using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClothing : MonoBehaviour
{
    private List<Transform> fBottoms;
    private List<Transform> mBottoms;
    private List<Transform> tops;
    private List<Transform> hairs;
    private List<Transform> genders;
    // Start is called before the first frame update
    void Start()
    {
        fBottoms = new List<Transform>();
        mBottoms = new List<Transform>();
        tops = new List<Transform>();
        hairs = new List<Transform>();
        genders = new List<Transform>();

        for (int i = 0;i<transform.childCount;i++)
        {
            if (ContainsName("Bot",transform.GetChild(i)))
            {
                if (ContainsName("Male",transform.GetChild(i)))
                {
                    mBottoms.Add(transform.GetChild(i));
                    transform.GetChild(i).gameObject.SetActive(false);
                    continue;
                } 
                else if (ContainsName("Female", transform.GetChild(i)))
                {
                   fBottoms.Add(transform.GetChild(i));
                    transform.GetChild(i).gameObject.SetActive(false);
                    continue;
                }
            }
            if (ContainsName("Top", transform.GetChild(i)))
            {
                tops.Add(transform.GetChild(i));
                transform.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            if (ContainsName("Hair", transform.GetChild(i)))
            {
                hairs.Add(transform.GetChild(i));
                transform.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            if (ContainsName("Base", transform.GetChild(i)))
            {
                genders.Add(transform.GetChild(i));
                transform.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }

        int gender = Random.Range(0, genders.Count);
        int mBot = Random.Range(0, mBottoms.Count);
        int fBot = Random.Range(0, fBottoms.Count);
        int top = Random.Range(0, tops.Count);
        int hair = Random.Range(0, hairs.Count);

        genders[gender].gameObject.SetActive(true);
        if (gender == 1)
        {
            mBottoms[mBot].gameObject.SetActive(true);
        }
        else
        {
            fBottoms[fBot].gameObject.SetActive(true);
        }
        tops[top].gameObject.SetActive(true);
        hairs[hair].gameObject.SetActive(true);
    }

    public bool ContainsName(string target,Transform transf)
    {
        return transf.name.Contains(target);
    }
}
