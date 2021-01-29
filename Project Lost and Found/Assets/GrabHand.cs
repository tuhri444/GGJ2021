using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GrabHand : MonoBehaviour
{
    public delegate void Grab();
    public static Grab OnGrab;

    private bool grabbing = false;

    private Animator grabbing_animation;

    void Start()
    {
        grabbing_animation = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMouseInput();
        HandleGrabAnimation();
        OnGrab?.Invoke();
    }

    private void HandleGrabAnimation()
    {
        if(grabbing && !grabbing_animation.IsInTransition(0) && grabbing_animation.GetCurrentAnimatorClipInfo(0)[0].clip.name == "rest")
        {
            grabbing_animation.SetTrigger("Grab");
        }
        else if(!grabbing && grabbing_animation.GetCurrentAnimatorClipInfo(0)[0].clip.name == "grab")
        {
            grabbing_animation.SetTrigger("Pullback");
        }
    }

    private void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            grabbing = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            grabbing = false;
        }
    }
}
