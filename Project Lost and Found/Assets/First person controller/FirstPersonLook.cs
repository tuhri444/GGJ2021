﻿using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;

    private bool canMove = true;

    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GrabHand.OnGrab += OnGrab;
        GrabHand.OnLetGo += OnLetGo;
    }

    void Update()
    {
        if (canMove)
        {
            // Get smooth mouse look.
            Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
            appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
            currentMouseLook += appliedMouseDelta;
            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

            // Rotate camera and controller.
            transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
        }
    }
    void OnGrab(Collider other)
    {
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnLetGo(Collider other)
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        GrabHand.OnGrab -= OnGrab;
        GrabHand.OnLetGo -= OnLetGo;
    }
}
