using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GrabAnimationController : MonoBehaviour
{
    private bool grabbing = false;
    private bool grabbingHand = false; 

    private Animator grabbing_animation;

    void Start()
    {
        grabbing_animation = GetComponent<Animator>();
        GrabHand.OnGrab += OnGrab;
    }

    void Update()
    {
        HandleMouseInput();
        HandleGrabAnimation();
    }

    private void HandleGrabAnimation()
    {
        if(grabbing && !grabbing_animation.GetNextAnimatorStateInfo(0).IsName("Grab") && grabbing_animation.GetCurrentAnimatorClipInfo(0)[0].clip.name == "rest")
        {
            grabbing_animation.SetTrigger("Grab");
        }
        else if(!grabbing && grabbing_animation.GetCurrentAnimatorClipInfo(0)[0].clip.name == "grab" && !grabbing_animation.IsInTransition(0))
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
        if(!grabbingHand && Input.GetMouseButtonUp(0))
        {
            grabbing = false;
        }
    }
    public void StopGrabbing()
    {
        grabbingHand = false;
        grabbing = false;
    }

    void OnGrab(Collider other)
    {
        grabbingHand = true;
    }

    private void OnDestroy()
    {
        GrabHand.OnGrab -= OnGrab;
    }
}
