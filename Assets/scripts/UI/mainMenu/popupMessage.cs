using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class popupMessage : MonoBehaviour
{
    Animator anim;
 
    public string animationName;
    [Space]
    public TextMeshProUGUI text;


    void Start() { anim = transform.GetComponent<Animator>(); }

    public void playMessage(string msgText){ text.text = msgText; anim.Play(animationName); }
}
