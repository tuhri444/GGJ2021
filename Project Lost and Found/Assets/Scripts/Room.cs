using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    ProceduralRoomGenerator procRoomGen;
    [SerializeField]
    private bool isMain = false;

    private List<RoomTrigger> doors;

    private bool northAdjacentRoom = false;
    private bool southAdjacentRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        doors = new List<RoomTrigger>();
        procRoomGen = FindObjectOfType<ProceduralRoomGenerator>();
        doors.Add(transform.Find("NorthTrigger").GetComponent<RoomTrigger>());
        doors.Add(transform.Find("SouthTrigger").GetComponent<RoomTrigger>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {

    }

    public bool CheckAdjacency()
    {
        int count = 0;
        for(int i = 0;i<doors.Count;i++)
        {
            if(doors[i].HasAdjacentRoom)
            {
                count++;
            }
        }

        if (count < doors.Count)
        {
            return true;
        }
        return false;
    }

    public bool IsMain
    {
        get { return isMain; }
        set { isMain = value; }
    }

    public bool NorthAdjacent
    {
        get { return northAdjacentRoom; }
        set { northAdjacentRoom = value; }
    }

    public bool SouthAdjacent
    {
        get { return southAdjacentRoom; }
        set { southAdjacentRoom = value; }
    }

}
