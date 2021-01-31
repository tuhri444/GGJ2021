using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroTimer : MonoBehaviour
{
    private float time;
    private float duration = 15f;
    private bool act2 = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > duration && !act2)
        {
            GameObject.Find("TextPanel").transform.GetChild(0).GetComponent<TMP_Text>().text = "When we got to the Ikea I got really bored. But then I saw this really cool bed and I ran to jump on it! It was soo fun! Wait?! Wheres mommy?!!......";
            duration = 8f;
            time = 0;
            act2 = true;
        }

        if(time > duration && act2)
        {
            SceneManager.LoadScene(2);
        }
        if(Input.GetMouseButton(0)&& !act2)
        {
            time = 15.1f;
            act2 = true;
        }
        if(Input.GetMouseButton(0) && act2)
        {
            time = 8.1f;
        }
    }
}
