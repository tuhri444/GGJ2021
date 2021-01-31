using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
//using UnityEditor.AI;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    private float spawnRad = 20;

    [SerializeField]
    private GameObject NPCPref;

    [SerializeField]
    private List<GameObject> NPCS;

    [SerializeField]
    private float NPCCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        bool CreatedMom = false;
        //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        for (int i = 0; i < NPCCount; i++)
        {
            GameObject go = Instantiate(NPCPref);
            go.GetComponent<NavMeshAgent>().Warp(NavMeshUtil.GetRandomPoint(transform.localPosition, spawnRad,1));
            NPCS.Add(go);
        }
        GameObject randomNPC = NPCS[Random.Range(0, NPCS.Count)];
        randomNPC.GetComponentInChildren<MomData>().IsMom = true;
    }

    public bool ValidatePosition(int index,Vector3 position)
    {
        for(int i = 0;i<NPCS.Count;i++)
        {
            if (index != i)
            {
                if (Vector3.Distance(position, NPCS[i].transform.position) < .1f)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public Vector2 GetNewDestination()
    {

        return new Vector2(0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,spawnRad);
    }
}
