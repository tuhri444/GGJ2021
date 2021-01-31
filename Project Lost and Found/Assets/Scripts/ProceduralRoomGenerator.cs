using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProceduralRoomGenerator : MonoBehaviour
{
    private List<Room> rooms = new List<Room>();

    private List<Room> toBeAdded;

    [SerializeField]
    private List<GameObject> roomPrefs;

    // Start is called before the first frame update
    void Start()
    {
        rooms = FindObjectsOfType<Room>().ToList();
        toBeAdded = new List<Room>();
        foreach (Room room in rooms)
        {
            room.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < toBeAdded.Count; i++)
        {
            rooms.Add(toBeAdded[i]);
        }
        toBeAdded.Clear();
        foreach (Room room in rooms)
        {
            //Debug.Log("Spam");
            if (room.IsMain)
            {
                //Debug.Log(room.NorthAdjacent);
                if (room.Doors.ContainsKey("NorthTrigger"))
                {
                    if (!room.Doors["NorthTrigger"].HasAdjacentRoom)
                    {
                        int num = Random.Range(0, roomPrefs.Count);
                        SpawnRoom(roomPrefs[num],null, room.transform.position + new Vector3(4.4f, 0f, 24));
                        room.Doors["NorthTrigger"].HasAdjacentRoom = true;
                    }
                }

                //Debug.Log(room.SouthAdjacent);
                if (room.Doors.ContainsKey("SouthTrigger"))
                {
                    if (!room.Doors["SouthTrigger"].HasAdjacentRoom)
                    {
                        int num = Random.Range(0, roomPrefs.Count);
                        SpawnRoom(roomPrefs[num],null, room.transform.position + new Vector3(-4.4f, 0f, -24),new Vector3(0,180,0));
                        room.Doors["SouthTrigger"].HasAdjacentRoom = true;
                    }
                }

                //Debug.Log(room.EastAdjacent);
                if (room.Doors.ContainsKey("EastTrigger"))
                {
                    if (!room.Doors["EastTrigger"].HasAdjacentRoom)
                    {
                        int num = Random.Range(0, roomPrefs.Count);
                        SpawnRoom(roomPrefs[num], null, room.transform.position + new Vector3(23.5f, 0f, 0));
                        room.Doors["EastTrigger"].HasAdjacentRoom = true;
                    }
                }

                //Debug.Log(room.WestAdjacent);
                if (room.Doors.ContainsKey("WestTrigger"))
                {
                    if (!room.Doors["WestTrigger"].HasAdjacentRoom)
                    {
                        int num = Random.Range(0, roomPrefs.Count);
                        SpawnRoom(roomPrefs[num], null, room.transform.position + new Vector3(-23.5f, 0f, 0f));
                        room.Doors["WestTrigger"].HasAdjacentRoom = true;
                    }
                }
            }
        }
    }

    public void SpawnRoom(GameObject roomPref, Transform parent, Vector3 position,Vector3 rotation)
    {
        GameObject go = Instantiate(roomPref);
        go.transform.parent = parent;
        go.transform.localPosition = position;
        go.transform.rotation.SetEulerAngles(rotation);
        toBeAdded.Add(go.GetComponent<Room>());
        Debug.Log("SPAWNED");
    }

    public void SpawnRoom(GameObject roomPref,Transform parent, Vector3 position)
    {
        GameObject go = Instantiate(roomPref);
        go.transform.parent = parent;
        go.transform.localPosition = position;
        toBeAdded.Add(go.GetComponent<Room>());
        Debug.Log("SPAWNED");
    }
}
