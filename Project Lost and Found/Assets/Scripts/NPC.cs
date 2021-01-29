using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    NavMeshAgent navAgent;

    private bool stopWalking = false;

    private float time = 0.0f;
    private float waitTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoRepath = true;
        navAgent.SetDestination(NavMeshUtil.GetRandomPoint(navAgent.transform.position,20));
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if(!navAgent.hasPath && !navAgent.isStopped)
        {
            if(time >= waitTime)
            {
                navAgent.SetDestination(NavMeshUtil.GetRandomPoint(navAgent.transform.position, 20));//just a magic number for now
                time = 0;
                waitTime = Random.Range(1,5);
            }
            else
            {
                time += Time.deltaTime;
            }
        }
    }


    public void ToggleWalk()
    {
        navAgent.isStopped = !navAgent.isStopped;
    }

    public void ToggleWalk(bool toggle)
    {
        navAgent.isStopped = toggle;
    }
}
