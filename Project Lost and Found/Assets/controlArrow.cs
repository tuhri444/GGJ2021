using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlArrow : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 1.0f)] private float activation_speed;
    [SerializeField][Range(0.0f,5.0f)] private float timerInSeconds;
    private bool arrowActive = false;
    private float currentTime;
    private MomData mom;
    private StressData player;

    private float timePassed = 0.0f;

    void Start()
    {
        player = FindObjectOfType<StressData>();
        
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void Update()
    {
        if(mom == null)
        {
            MomData[] potentialmoms = FindObjectsOfType<MomData>();
            for (int i = 0; i < potentialmoms.Length; i++)
            {
                if (potentialmoms[i].IsMom)
                {
                    Debug.Log("Found mom " + potentialmoms[i].name);
                    mom = potentialmoms[i];
                }
            }
        }
        HandleDirection();
        HandlePosition();
        if (arrowActive)
        {
            if (timePassed > timerInSeconds*100)
            {
                arrowActive = false;
                timePassed = 0.0f;
            }
            timePassed += Time.deltaTime;
            HandleAnimation();
        }
    }

    private void HandlePosition()
    {
        transform.position = player.transform.position + player.transform.forward * 2.0f;
    }
    private void HandleDirection()
    {
        transform.LookAt(mom.transform);
    }
    private void HandleAnimation()
    {
        if (arrowActive && currentTime <= activation_speed)
        {
            //Ease into existance
            float size = Easing.BackEaseOut(currentTime, 0.0f, 1.0f, activation_speed);
            transform.localScale = new Vector3(size, size, 1.0f);
            currentTime += Time.deltaTime;

        }
        else if (!arrowActive && currentTime >= 0)
        {
            //Ease out of existance
            float size = Easing.BackEaseOut(currentTime, 0.0f, 1.0f, activation_speed);
            transform.localScale = new Vector3(size, size, 1.0f);
            currentTime -= Time.deltaTime;
        }
    }
    public void activationTrigger()
    {
        arrowActive = true;
        currentTime = 0.0f;
    }
}
