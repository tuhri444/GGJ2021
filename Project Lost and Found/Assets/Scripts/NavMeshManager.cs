using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    private GameObject NPCPref;

    [SerializeField]
    private List<GameObject> NPCs;

    [SerializeField]
    private float NPCCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i= 0;i<NPCCount;i++)
        {
            GameObject go = Instantiate(NPCPref);
            go.transform.position = NavMeshUtil.GetRandomPoint(new Vector3(-1.3f, 0, -11.6f), 20);
            Debug.Log(go.transform.position);
            NPCs.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
