using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleBar : MonoBehaviour
{
    public Slider slider;
    public void SetAngle(float angle)
    {
        slider.value = angle;
    }

}
