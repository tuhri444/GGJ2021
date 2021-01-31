using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    ProceduralRoomGenerator procRoomGen;
    [SerializeField]
    private bool isMain = false;

    private Dictionary<string,RoomTrigger> doors;

    private List<string> keysToRemove = new List<string>();

    //[SerializeField]
    //private bool northAdjacentRoom = false;
    //[SerializeField]
    //private bool southAdjacentRoom = false;
    //[SerializeField]
    //private bool eastAdjacentRoom = false;
    //[SerializeField]
    //private bool westAdjacentRoom = false;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        doors = new Dictionary<string, RoomTrigger>();
        procRoomGen = FindObjectOfType<ProceduralRoomGenerator>();

        RoomTrigger trigger;
        if (transform.Find("NorthTrigger") != null)
        {
            doors.Add("NorthTrigger", transform.Find("NorthTrigger").GetComponent<RoomTrigger>());
        }
        if (transform.Find("SouthTrigger") != null)
        {
            doors.Add("SouthTrigger", transform.Find("SouthTrigger").GetComponent<RoomTrigger>());
        }
        if (transform.Find("EastTrigger") != null)
        {
            doors.Add("EastTrigger", transform.Find("EastTrigger").GetComponent<RoomTrigger>());
        }
        if (transform.Find("WestTrigger") != null)
        {
            doors.Add("WestTrigger", transform.Find("WestTrigger").GetComponent<RoomTrigger>());
        }
       
        foreach(RoomTrigger rt in doors.Values)
        {
            //Debug.Log("Init RoomTriggers");
            rt.Init();
        }
        //northAdjacentRoom = doors[0].HasAdjacentRoom;
        //southAdjacentRoom = doors[1].HasAdjacentRoom;
        //eastAdjacentRoom = doors[2].HasAdjacentRoom;
        //westAdjacentRoom = doors[3].HasAdjacentRoom;
    }

    public bool IsMain
    {
        get { return isMain; }
        set { isMain = value; }
    }

    //public bool NorthAdjacent
    //{
    //    get { return northAdjacentRoom; }
    //    set { northAdjacentRoom = value; }
    //}

    //public bool SouthAdjacent
    //{
    //    get { return southAdjacentRoom; }
    //    set { southAdjacentRoom = value; }
    //}

    //public bool EastAdjacent
    //{
    //    get { return eastAdjacentRoom; }
    //    set { eastAdjacentRoom = value; }
    //}

    //public bool WestAdjacent
    //{
    //    get { return westAdjacentRoom; }
    //    set { westAdjacentRoom = value; }
    //}

    public Dictionary<string, RoomTrigger> Doors
    {
        get { return doors; }
    }



}
