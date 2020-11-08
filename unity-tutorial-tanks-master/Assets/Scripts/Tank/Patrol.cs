using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] Points;
    int patrolWP = 0;

    NavMeshAgent agent;


    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.9f) 
            DoPatrol();
    }

    void Seek(Vector3 vec)
    {
        agent.SetDestination(vec);
    }

    void DoPatrol()
    {
        patrolWP = (patrolWP + 1) % Points.Length;
        Seek(Points[patrolWP].transform.position);
    }
}
