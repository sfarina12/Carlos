using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class carUI : MonoBehaviour
{
    public Slider carSlider;
    public Rigidbody car;
    public TextMeshProUGUI speedText;

    private void LateUpdate()
    {
        int speed = Int32.Parse(car.velocity.magnitude.ToString("F0"));

        if (speed <= 50)
        { 
            carSlider.value = speed;
            speedText.text = speed.ToString();
        }

    }
}
