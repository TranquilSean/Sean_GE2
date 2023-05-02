using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAI : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float detectionRadius = 10f;
    public float huntRadius = 10f;
    private float areaMinMax = 20f;

    private GameObject[] rabbits;
    public GameObject target;

    public float hungry;
    private float timeSinceLastDecrease = 0f;

    void Start()
    {
        hungry = 0f;
        rabbits = GameObject.FindGameObjectsWithTag("Rabbit");
        LookForRabbit();
    }

    void Update()
    {
        timeSinceLastDecrease += Time.deltaTime;
        if (timeSinceLastDecrease >= 2)
        {
            hungry -= .2f;
            if (hungry < 0)
            {
                hungry = 0;
            }
            timeSinceLastDecrease = 0;
        }

        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            // if a rabbit is close enough, hunt it
            if (distance < huntRadius && hungry <=.8f)
            {
                Hunt(target);
            }
            
        }
        // otherwise, wander around
        else
        {
            LookForRabbit();
            Wander();
        }

    }

    public void LookForRabbit()
    {

        rabbits = GameObject.FindGameObjectsWithTag("Rabbit");

        GameObject closestRabbit = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        foreach (GameObject rabbit in rabbits)
        {
            float distance = Vector3.Distance(rabbit.transform.position, currentPosition);
            if (distance < detectionRadius)
            {
                // check if rabbit is closest one yet
                if (distance < closestDistance)
                {
                    closestRabbit = rabbit;
                    closestDistance = distance;
                }
            }
        }
        target = closestRabbit;
    }

    void Hunt(GameObject rabbit)
    {
        // move towards rabbit and face it
        Vector3 direction = rabbit.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        // move forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private Vector3 destination;
    void Wander()
    {
        float rotationSpeed = 10;
        // If the fox has reached its current destination
        if (Vector3.Distance(transform.position, destination) < 1f)
        {
            // Pick a new random position within the wander area
            float randomX = Random.Range(-areaMinMax, areaMinMax);
            float randomZ = Random.Range(-areaMinMax, areaMinMax);
            destination = new Vector3(randomX, transform.position.y, randomZ);
        }

        // Move towards the destination
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate towards the direction of movement
        if (direction.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rabbit"))
        {
            Destroy(other.gameObject);
            hungry += .6f;
            target = null;
        }
    }
}
