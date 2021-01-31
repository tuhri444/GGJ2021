using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEditor.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    NavMeshManager agentManager;
    NavMeshAgent navAgent;

    private float time = 0.0f;
    private float waitTime = 0.0f;
    private int id;
    // Start is called before the first frame update
    void Start()
    {
        agentManager = GameObject.Find("NavMeshManager").GetComponent<NavMeshManager>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoRepath = true;
        navAgent.SetDestination(NavMeshUtil.GetRandomPoint(navAgent.transform.position, 20, -1));
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.enabled)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Idle", false);
            if (!navAgent.hasPath && !navAgent.isStopped)
            {

                if (agentManager.ValidatePosition(id, transform.position))
                {
                    if (time >= waitTime)
                    {
                        navAgent.SetDestination(NavMeshUtil.GetRandomPoint(transform.position, 5, -1));//just a magic number for now
                        time = 0;
                        waitTime = Random.Range(1, 5);
                    }
                    else
                    {
                        time += Time.deltaTime;
                    }
                }
                else
                {

                    navAgent.SetDestination(NavMeshUtil.GetRandomPoint(transform.position, 5, -1));//just a magic number for now
                    time = 0;
                    waitTime = Random.Range(1, 5);

                }
            }
            else
            {
                transform.GetChild(0).GetComponent<Animator>().SetBool("Idle",true);
            }
        }
    }


    public void ToggleWalk()
    {
        navAgent.isStopped = !navAgent.isStopped;
    }
    public void ToggleWalk(bool toggle)
    {
        if (toggle)
            navAgent.enabled = false;
        else
            navAgent.enabled = true;
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }
}
