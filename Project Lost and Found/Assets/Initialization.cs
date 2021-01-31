using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Initialization : MonoBehaviour
{
    [SerializeField]
    private GameObject loadScreen;

    private float time;
    private float duration = 7f;
    GameObject go;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.worldCamera = Camera.main;
        loadScreen.transform.localScale = new Vector3(2, 2, 2);
        go = Instantiate(loadScreen, new Vector3(960,560,0), Quaternion.identity, canvas.transform); 
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > duration)
        {
            Destroy(go);
            Destroy(this.gameObject);
        }
    }
}
