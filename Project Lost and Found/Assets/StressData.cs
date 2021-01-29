using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressData : MonoBehaviour
{
    private float stressLevel = 0.0f;
    public void AddToStress(float addition) => stressLevel += addition;
    public void RemoveFromStress(float addition) => stressLevel -= addition;
    public float GetStress() => stressLevel;
}

