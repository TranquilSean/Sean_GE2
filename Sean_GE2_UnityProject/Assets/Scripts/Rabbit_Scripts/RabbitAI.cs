using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    public float moveSpeed = 0f;
    public float searchRadius = 10f;
    public float jumpForce = 5f;

    private float hungry;
    private GameObject targetFood;
    private GameObject[] Food;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {

        // check for predators within a certain distance
        GameObject[] predators = GameObject.FindGameObjectsWithTag("Fox");
        foreach (GameObject predator in predators)
        {
            float distance = Vector3.Distance(predator.transform.position, transform.position);
            if (distance < 5)
            {
                // flee from the predator
                Flee();
                return;
            }
        }
        // if there's a targetFood, move towards it
        if (targetFood != null)
        {
            moveSpeed = 5;
            Vector3 direction = targetFood.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        // move forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // if there's no targetFood, find a new one
        if (targetFood == null)
        {
            Idle();
            FindFood();
        }
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            targetFood = null;
        }
    }

   

    public void FindFood()
    {
        Food = GameObject.FindGameObjectsWithTag("Food");

        GameObject closestFood = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Iterate through all food objects to find the closest one
        foreach (GameObject foodObject in Food)
        {
            float distance = Vector3.Distance(foodObject.transform.position, currentPosition);
            if (distance < closestDistance)
            {
                closestFood = foodObject;
                closestDistance = distance;
            }
        }

        targetFood = closestFood;
    }

    public void Flee()
    {
        moveSpeed = 5;
        GameObject[] predators = GameObject.FindGameObjectsWithTag("Fox");

        foreach (GameObject predator in predators)
        {
            // get the direction away from the predator
            Vector3 direction = transform.position - predator.transform.position;
            //lookaway 
            transform.LookAt(direction);
            direction.y = 0;
            direction.Normalize();

            
            // move away from the predator
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    public void Idle()
    {
        moveSpeed = 0;
    }
}
