using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GrabHand : MonoBehaviour
{
    public delegate void Grab(Collider other);
    public static Grab OnGrab;
    public delegate void LetGo(Collider other);
    public static LetGo OnLetGo;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<GetGrabbed>() != null)
            OnGrab?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<GetGrabbed>() != null)
            OnLetGo?.Invoke(other);
    }

}
