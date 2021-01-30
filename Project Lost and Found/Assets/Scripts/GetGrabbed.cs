using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetGrabbed : MonoBehaviour
{
    private StressData playerStress;
    private DialogueSystem ds;

    private Material self_mat;
    private Color selected_color = new Color(0, 0, 255);
    private Color normal_color;
    private NPC npc;

    [SerializeField] [Range(0.0f, 100.0f)] private float chanceOfBeingMom;

    private List<Dialogue> IsMomDialogues;
    private List<Dialogue> IsNotMomDialogues;

    void Start()
    {
        playerStress = FindObjectOfType<StressData>();
        self_mat = GetComponent<MeshRenderer>().material;
        normal_color = self_mat.color;
        npc = GetComponentInParent<NPC>();

        ds = FindObjectOfType<DialogueSystem>();

        IsMomDialogues = new List<Dialogue>();
        IsNotMomDialogues = new List<Dialogue>();

        Dialogue d = new Dialogue() { name = "Not Mom", dialogue = "Hi child, what is it you require of me?" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "You", dialogue = "Have you seen my mother?" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "Not Mom", dialogue = "Nope" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "", dialogue = "" };
        IsNotMomDialogues.Add(d);

        d = new Dialogue() { name = "Mom", dialogue = "Thank god i found you, i was worried sick" };
        IsMomDialogues.Add(d);
        d = new Dialogue() { name = "Mom", dialogue = "Let's go get some meatballs!" };
        IsMomDialogues.Add(d);
        d = new Dialogue() { name = "", dialogue = "" };
        IsMomDialogues.Add(d);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            self_mat.color = selected_color;
            float ranNumb = Random.Range(0.0f,100.01f);
            if (ranNumb <= chanceOfBeingMom)
            {
                ds.ClearQueue();
                DialogueSystem.AddDialogue(IsMomDialogues[0]);
                DialogueSystem.AddDialogue(IsMomDialogues[1]);
                ds.triggerAnimation = true;
                DialogueSystem.NextSentence();
            }
            else
            {
                ds.ClearQueue();
                DialogueSystem.AddDialogue(IsNotMomDialogues[0]);
                DialogueSystem.AddDialogue(IsNotMomDialogues[1]);
                DialogueSystem.AddDialogue(IsNotMomDialogues[2]);
                ds.triggerAnimation = true;
                DialogueSystem.NextSentence();
                playerStress.AddToStress(10);
            }

            if(npc != null)
                npc.ToggleWalk(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            self_mat.color = normal_color;
            if (npc != null)
                npc.ToggleWalk(false);
        }
    }

}
