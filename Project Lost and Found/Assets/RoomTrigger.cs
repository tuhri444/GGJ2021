using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    Room room;
    // Start is called before the first frame update
    void Start()
    {
        room = transform.parent.GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (room.IsMain)
        {
            Debug.Log("Leaving Room");
        }
        else
        {
            Debug.Log("Entering Room");
        }
    }

}
