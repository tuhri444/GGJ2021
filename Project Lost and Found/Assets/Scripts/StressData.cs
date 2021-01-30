using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressData : MonoBehaviour
{
    [SerializeField] [Range(0.0f,100.0f)] private float stressLevel = 0.0f;
    public void AddToStress(float addition) => stressLevel += addition;
    public void RemoveFromStress(float addition) => stressLevel -= addition;
    public float GetStress() => stressLevel;
}

