using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengeState : RabbitStateBase
{

    private GameObject Target;
    private List<GameObject> Food = new List<GameObject>();


    public override void EnterState(RabbitStateManager rabbit)
    {
        Food.AddRange(GameObject.FindGameObjectsWithTag("Food"));

        float dist = 0;
        float lowdist = 10000;
        for (int i = 0; i < Food.Count; i++)
        {
            dist = Vector3.Distance(rabbit.transform.position, Food[i].transform.position);

            if (dist < lowdist)
            {
                lowdist = dist;
                Target = Food[i];

            }

        }

        Food.Clear();
    }

    public override void UpdateState(RabbitStateManager rabbit)
    {
        if (Target == null)
        {
            rabbit.ChangeState(rabbit.ScavengeState);
            return;
        }

        if (Vector3.Distance(rabbit.transform.position, Target.transform.position) > 3)
        {
            //move to food         
        }
        else
        {
            Target.transform.position = rabbit.transform.position;
            rabbit.ChangeState(rabbit.IdleState);
        }
    }

    public override void ExitState(RabbitStateManager rabbit)
    {

    }
}
