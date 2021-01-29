using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGrabbed : MonoBehaviour
{
    private Material self_mat;
    private Color selected_color = new Color(0, 0, 255);
    private Color normal_color;

    void Start()
    {
        self_mat = GetComponent<MeshRenderer>().material;
        normal_color = self_mat.color;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
            self_mat.color = selected_color;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
            self_mat.color = normal_color;
    }

}
