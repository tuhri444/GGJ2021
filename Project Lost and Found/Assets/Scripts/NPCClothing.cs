using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClothing : MonoBehaviour
{
    [SerializeField]
    private List<Material> hairMaterials;

    [SerializeField]
    private List<Material> topMaterials;

    [SerializeField]
    private List<Material> bottomMaterials;

    [SerializeField]
    private List<Material> skinMaterials;


    private List<Transform> fBottoms;
    private List<Transform> mBottoms;
    private List<Transform> fTops;
    private List<Transform> mTops;
    private List<Transform> hairs;
    private List<Transform> genders;

    // Start is called before the first frame update
    void Start()
    {
        fBottoms = new List<Transform>();
        mBottoms = new List<Transform>();
        fTops = new List<Transform>();
        mTops = new List<Transform>();
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
                if (ContainsName("Male", transform.GetChild(i)))
                {
                    mTops.Add(transform.GetChild(i));
                    transform.GetChild(i).gameObject.SetActive(false);
                    continue;
                }
                else if (ContainsName("Female", transform.GetChild(i)))
                {
                    fTops.Add(transform.GetChild(i));
                    transform.GetChild(i).gameObject.SetActive(false);
                    continue;
                }
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
        int fTop = Random.Range(0, fTops.Count);
        int mTop = Random.Range(0, mTops.Count);
        int hair = Random.Range(0, hairs.Count);
        int mat;

        mat = Random.Range(0, skinMaterials.Count);
        genders[gender].gameObject.SetActive(true);
        genders[gender].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[mat];
        if (gender == 1)
        {
            mat = Random.Range(0, bottomMaterials.Count);
            mBottoms[mBot].gameObject.SetActive(true);
            mBottoms[mBot].GetComponent<SkinnedMeshRenderer>().material = bottomMaterials[mat];

            mat = Random.Range(0, topMaterials.Count);
            mTops[mTop].gameObject.SetActive(true);
            mTops[mTop].GetComponent<SkinnedMeshRenderer>().material = topMaterials[mat];
        }
        else
        {
            mat = Random.Range(0, bottomMaterials.Count);
            fBottoms[fBot].gameObject.SetActive(true);
            fBottoms[fBot].GetComponent<SkinnedMeshRenderer>().material = bottomMaterials[mat];

            mat = Random.Range(0, topMaterials.Count);
            fTops[fTop].gameObject.SetActive(true);
            fTops[fTop].GetComponent<SkinnedMeshRenderer>().material = topMaterials[mat];
        }


        mat = Random.Range(0, hairMaterials.Count);
        hairs[hair].gameObject.SetActive(true);
        hairs[hair].GetComponent<SkinnedMeshRenderer>().material = hairMaterials[mat];
    }

    public bool ContainsName(string target,Transform transf)
    {
        return transf.name.Contains(target);
    }
}
