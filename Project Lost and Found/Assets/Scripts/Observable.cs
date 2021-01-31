using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observable : MonoBehaviour
{

    private GameObject observableSpot;
    // Start is called before the first frame update
    void Start()
    {
        observableSpot = transform.Find("ObserveSpot").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject ObservableSpot
    {
        get { return observableSpot; }
        set { observableSpot = value; }
    }

    public Vector3 position
    {
        get { return observableSpot.transform.localPosition; }
        set { observableSpot.transform.localPosition = value; }
    }

}
