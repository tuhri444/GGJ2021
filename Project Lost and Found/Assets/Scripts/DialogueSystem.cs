using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct Dialogue
{
    public string name;
    public string dialogue;
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    private RectTransform dialogueTrans;

    private GrabAnimationController animController;

    [SerializeField] private TextMeshProUGUI dialogueName;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] [Range(0.0f, 1.0f)] private float animationSpeed;
    [SerializeField] public bool triggerAnimation;
    private float currentTime = 0.0f;
    private bool dialogueActive;

    [SerializeField] private float dialogueSpeed;
    [SerializeField] private bool triggerText;
    private static Queue<Dialogue> dialogueQueue;

    private static bool startReading = false;
    private bool doneReadingCurrent = true;

    private bool checkForMouseInput = false;

    public delegate void StopTalking();
    public static StopTalking OnStopTalking;
    
    private GrabHand hand;



    void Start()
    {
        dialogueTrans = dialogueBox.GetComponent<RectTransform>();
        dialogueTrans.localScale = new Vector3(0, 0, 0);
        dialogueBox.SetActive(false);
        GrabHand.OnGrab += OnGrab;
        dialogueQueue = new Queue<Dialogue>();
        animController = FindObjectOfType<GrabAnimationController>();
        hand = FindObjectOfType<GrabHand>();
    }
    void Update()
    {
        activationTrigger();
        activationTriggerText();
        if(checkForMouseInput)
        {
            if(Input.GetMouseButtonDown(0))
            {
                NextSentence();
            }
        }
        if (dialogueQueue != null && dialogueQueue.Count == 0 && checkForMouseInput)
        {
            NextSentence();
            NextSentence();
            NextSentence();
            checkForMouseInput = false;
            animController.StopGrabbing();
            animController.Pullback();
            OnStopTalking?.Invoke();
            GrabHand.OnLetGo?.Invoke(new Collider());
        }
        HandleBoxAnimation();
        DisplayText();
    }
    private void DisplayText()
    {
        if (dialogueQueue != null && currentTime >= animationSpeed)
        {
            if(startReading && doneReadingCurrent)
            {
                if (dialogueQueue.Count > 0)
                {
                    startReading = false;
                    StartCoroutine(DisplayCharacter(dialogueQueue.Peek()));
                }
                else
                {
                    triggerAnimation = true;
                }
            }
        }
    }
    IEnumerator DisplayCharacter(Dialogue d)
    {
        doneReadingCurrent = false;
        //Reset boxes
        ClearDialogueBox();
        //Set name
        dialogueName.text = dialogueQueue.Peek().name;
        //Get speed
        float spd = dialogueSpeed;
        foreach(char character in d.dialogue)
        {
            if (startReading)
            {
                spd = 0.0f;
            }
            dialogueText.text += character;
            yield return new WaitForSeconds(spd);
        }
        dialogueQueue.Dequeue();
        doneReadingCurrent = true;
    }

    public void ClearQueue()
    {
        dialogueQueue.Clear();
    }
    private void ClearDialogueBox()
    {
        dialogueText.text = "";
        dialogueName.text = "";
    }
    private void HandleBoxAnimation()
    {
        if(dialogueActive && currentTime <= animationSpeed)
        {
            //Ease into existance
            float size = Easing.BackEaseOut(currentTime, 0.0f, 1.0f, animationSpeed);
            //Debug.Log("Appear: " + size);
            //Debug.Log("currentTime: " + currentTime);
            dialogueTrans.localScale = new Vector3(size,size, 1.0f);
            currentTime += Time.deltaTime;
            ClearDialogueBox();
        }
        else if(!dialogueActive && currentTime >= 0)
        {
            //Ease out of existance
            float size = Easing.BackEaseOut(currentTime, 0.0f, 1.0f, animationSpeed);
            //Debug.Log("dissapear: " + size);
            //Debug.Log("currentTime: " + currentTime);
            dialogueTrans.localScale = new Vector3(size, size, 1.0f);
            currentTime -= Time.deltaTime;
        }

        //end of dissapearing
        if (!dialogueActive && currentTime <= 0 && dialogueBox.activeSelf)
        {
            ClearDialogueBox();
            dialogueBox.SetActive(false);
        }
    }
    public void activationTrigger()
    {
        if(triggerAnimation)
        {
            dialogueActive = !dialogueActive;
            if (dialogueActive)
            {
                currentTime = 0.0f;
                dialogueBox.SetActive(true);
            }
            else
                currentTime = animationSpeed;
            triggerAnimation = false;
        }
    }
    private void activationTriggerText()
    {
        if (triggerText)
        {
            NextSentence();
            triggerText = false;
        }
    }
    public static void AddDialogue(Dialogue d)
    {
        dialogueQueue.Enqueue(d);
    }
    public static void NextSentence()
    {
        startReading = !startReading;
    }

    void OnGrab(Collider other)
    {
        checkForMouseInput = true;
    }

    private void OnDestroy()
    {
        GrabHand.OnGrab -= OnGrab;
    }

}
