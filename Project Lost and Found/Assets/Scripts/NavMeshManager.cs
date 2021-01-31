using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
//using UnityEditor.AI;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    private GameObject NPCPref;

    [SerializeField]
    private List<GameObject> NPCS;

    [SerializeField]
    private float NPCCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        for (int i = 0; i < NPCCount; i++)
        {
            GameObject go = Instantiate(NPCPref);
            Vector3 pos = new Vector3(transform.position.x,0,transform.position.z);
            go.transform.position = NavMeshUtil.GetRandomPoint(transform.position, 2,-1);
            NPCS.Add(go);
        }
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
}
