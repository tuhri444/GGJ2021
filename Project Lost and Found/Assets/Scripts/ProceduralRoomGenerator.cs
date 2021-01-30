using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRoomGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Room> rooms = new List<Room>();

    private List<Room> toBeAdded;

    [SerializeField]
    private GameObject roomPref;
    // Start is called before the first frame update
    void Start()
    {
        toBeAdded = new List<Room>();
        foreach(Room room in rooms)
        {
            room.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i<toBeAdded.Count;i++)
        {
            rooms.Add(toBeAdded[i]);
        }
        toBeAdded.Clear();
        foreach (Room room in rooms)
        {
            //Debug.Log("Spam");
            if (room.IsMain)
            {
                //Debug.Log(room.SouthAdjacent);
                if (!room.SouthAdjacent)
                {
                    SpawnRoom(null, room.transform.position + new Vector3(-4.4f, 0f, -24));
                    room.SouthAdjacent = true;
                }

                //Debug.Log(room.NorthAdjacent);
                if (!room.NorthAdjacent)
                {
                    SpawnRoom(null, room.transform.position + new Vector3(4.4f, 0f, 24));
                    room.NorthAdjacent = true;
                }

                //Debug.Log(room.EastAdjacent);
                if (!room.EastAdjacent)
                {
                    SpawnRoom(null, room.transform.position + new Vector3(23.5f, 0f, 0));
                    room.EastAdjacent = true;
                }

                //Debug.Log(room.WestAdjacent);
                if (!room.WestAdjacent)
                {
                    SpawnRoom(null, room.transform.position + new Vector3(-23.5f, 0f, 0f));
                    room.WestAdjacent = true;
                }
            }
        }
    }


    public void SpawnRoom(Transform parent, Vector3 position)
    {
        GameObject go = Instantiate(roomPref);
        go.transform.parent = parent;
        go.transform.localPosition = position;
        toBeAdded.Add(go.GetComponent<Room>());
        Debug.Log("SPAWNED");
    }
}
