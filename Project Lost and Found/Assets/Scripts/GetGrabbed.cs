using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GetGrabbed : MonoBehaviour
{
    private StressData playerStress;
    private DialogueSystem ds;
    private MomData momd;

    private Material self_mat;
    private Color selected_color = new Color(0, 0, 255);
    private Color normal_color;
    private NPC npc;

    [SerializeField] private Animator anim;

    [SerializeField] [Range(0.0f, 100.0f)] private float chanceOfBeingMom;

    private List<Dialogue> IsMomDialogues;
    private List<Dialogue> IsNotMomDialogues;

    private controlArrow arrow;


    void Start()
    {
        momd = GetComponentInParent<MomData>();
        playerStress = FindObjectOfType<StressData>();
        self_mat = GetComponent<MeshRenderer>().material;
        normal_color = self_mat.color;
        npc = GetComponentInParent<NPC>();

        DialogueSystem.OnStopTalking += OnStopTalking;

        ds = FindObjectOfType<DialogueSystem>();

        IsMomDialogues = new List<Dialogue>();
        IsNotMomDialogues = new List<Dialogue>();

        Dialogue d = new Dialogue() { name = "Not Mom", dialogue = "Hi there, what's wrong little guy?" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "You", dialogue = "Have you seen my mommy?" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "Not Mom", dialogue = "Nope" };
        IsNotMomDialogues.Add(d);

        d = new Dialogue() { name = "Not Mom", dialogue = "Hi there, what's wrong little guy?" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "You", dialogue = "Have you seen my mommy?" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "Not Mom", dialogue = "I think I might have seen someone in that direction" };
        IsNotMomDialogues.Add(d);
        d = new Dialogue() { name = "", dialogue = "" };
        IsNotMomDialogues.Add(d);

        d = new Dialogue() { name = "Mom", dialogue = "Thank god I found you, I was worried sick" };
        IsMomDialogues.Add(d);
        d = new Dialogue() { name = "Mom", dialogue = "Let's go get some meatballs!" };
        IsMomDialogues.Add(d);

        arrow = FindObjectOfType<controlArrow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GrabHand gh = other.GetComponent<GrabHand>();
        if (gh != null && !gh.IsGrabbing)
        {
            if (other.gameObject.layer == 8)
            {
                anim.SetTrigger("StartResting");
                self_mat.color = selected_color;
                if (momd.IsMom)
                {
                    ds.ClearQueue();
                    DialogueSystem.AddDialogue(IsMomDialogues[0]);
                    DialogueSystem.AddDialogue(IsMomDialogues[1]);
                    ds.triggerAnimation = true;
                    DialogueSystem.NextSentence();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    ds.ClearQueue();
                    anim.SetTrigger("StartResting");

                    MomData mom = FindObjectOfType<MomData>();

                    if (mom == null)
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

                    if (Vector3.Distance(mom.transform.position, transform.position) < 10)
                    {
                        arrow.activationTrigger();
                        DialogueSystem.AddDialogue(IsNotMomDialogues[3]);
                        DialogueSystem.AddDialogue(IsNotMomDialogues[4]);
                        DialogueSystem.AddDialogue(IsNotMomDialogues[5]);
                        DialogueSystem.AddDialogue(IsNotMomDialogues[6]);
                        playerStress.RemoveFromStress(10);
                        anim.SetTrigger("PointDirection");
                    }
                    else
                    {
                        DialogueSystem.AddDialogue(IsNotMomDialogues[0]);
                        DialogueSystem.AddDialogue(IsNotMomDialogues[1]);
                        DialogueSystem.AddDialogue(IsNotMomDialogues[2]);
                        playerStress.AddToStress(20);
                    }

                    ds.triggerAnimation = true;
                    DialogueSystem.NextSentence();
                }
                if (npc != null)
                    npc.ToggleWalk(true);
            }
        }

    }

    private void OnStopTalking()
    {
        self_mat.color = normal_color;
        DialogueSystem.NextSentence();
        anim.SetTrigger("StartWalking");
        ds.triggerAnimation = true;
        if (npc != null)
            npc.ToggleWalk(false);
    }
    public static Vector2 rotate(Vector3 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.z * Mathf.Sin(delta),
            v.z * Mathf.Sin(delta) + v.z * Mathf.Cos(delta)
        );
    }
    private void OnTriggerExit(Collider other)
    {
        //GrabHand gh = other.GetComponent<GrabHand>();
        //if (gh != null && !gh.IsGrabbing)
        //{
        //    if (other.gameObject.layer == 8)
        //    {
        //        self_mat.color = normal_color;
        //        if (npc != null)
        //            npc.ToggleWalk(false);
        //    }
        //}
    }

}
