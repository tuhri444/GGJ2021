using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRoomGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Room> rooms;

    [SerializeField]
    private GameObject roomPref;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Room room in rooms)
        {
            room.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Room room in rooms)
        {
            if (room.IsMain && room.CheckAdjacency())
            {
                if (!room.SouthAdjacent)
                {
                    SpawnRoom(null, room.transform.position + new Vector3(-4.4f, 0f, -24));
                    room.SouthAdjacent = true;
                }
                else if (!room.NorthAdjacent)
                {
                    SpawnRoom(null, room.transform.position + new Vector3(4.4f, 0f, 24));
                    room.NorthAdjacent = true;
                }
            }
        }
    }


    public void SpawnRoom(Transform parent, Vector3 position)
    {
        GameObject go = Instantiate(roomPref);
        go.transform.parent = parent;
        go.transform.localPosition = position;
        Debug.Log("SPAWN");
    }
}
