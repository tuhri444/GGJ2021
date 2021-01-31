using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class ControlCollider : MonoBehaviour
{
    private CapsuleCollider cc;

    void Start()
    {
        GrabHand.OnGrab += OnGrab;
        GrabHand.OnLetGo += OnLetGo;
        cc = GetComponent<CapsuleCollider>();
    }

    private void OnGrab(Collider other)
    {
        cc.enabled = false;
    }

    private void OnLetGo(Collider other)
    {
        cc.enabled = true;
    }

    private void OnDestroy()
    {
        GrabHand.OnGrab -= OnGrab;
        GrabHand.OnLetGo -= OnLetGo;
    }
}
