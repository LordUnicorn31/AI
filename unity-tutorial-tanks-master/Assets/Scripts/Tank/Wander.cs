using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    float freq = 0f;
    public float wanderFreq = 1f;
    public float wanderRadius = 3.5f;
    public float wanderOffset = 6f;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Engine audio (I need to know if im gona move next frame)
        
        freq += Time.deltaTime;
        if (freq > wanderFreq)
        {
            freq -= wanderFreq;
            DoWander();
        }
    }

    void Seek(Vector3 vec)
    {
        agent.SetDestination(vec);
    }

    void DoWander()
    {
        Vector3 localTarget = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        localTarget.Normalize();
        localTarget *= wanderRadius;
        localTarget += new Vector3(0, 0, wanderOffset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
        Seek(worldTarget);
    }
}
