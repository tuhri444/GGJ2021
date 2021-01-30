using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private Room room;
    [SerializeField]
    private int id = -1;
    [SerializeField]
    private bool hasAdjacentRoom = false;
    // Start is called before the first frame update
    public void Init()
    {
        room = transform.parent.GetComponent<Room>();
        hasAdjacentRoom = adjacentCheck();
    }

    void Update()
    {
        if (name.Equals("SouthTrigger"))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
        }
        else if (name.Equals("NorthTrigger"))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.green);
        }
        else if (name.Equals("EastTrigger"))
        {
            Debug.DrawRay(transform.position, transform.right, Color.blue);
        }
        else if (name.Equals("WestTrigger"))
        {
            Debug.DrawRay(transform.position, transform.right, Color.white);
        }

    }

    public bool adjacentCheck()
    {
        if (id == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            {
                Debug.Log("Name: " + name);
                Debug.Log("Hit Something");
                return true;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.right, out hit, 10))
            {
                Debug.Log("Name: " + name);
                Debug.Log("Hit Something");
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Triggered");
            if (room.IsMain)
            {
                Debug.Log("Leaving Room");
                room.IsMain = false;
            }
            else
            {
                Debug.Log("Entering Room");
                room.IsMain = true;
            }
        }
    }

    public bool HasAdjacentRoom
    {
        get { return hasAdjacentRoom; }
        set { hasAdjacentRoom = value; }
    }

}
