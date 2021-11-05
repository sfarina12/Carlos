using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public TextMeshProUGUI speed;
    
    public void updateSpeedText(Vector3 velocity) 
    { 
        speed.text = velocity.magnitude.ToString("0.00");
    }
    
}
