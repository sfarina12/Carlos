using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpScript : MonoBehaviour
{
    public List<GameObject> card;
    public GameObject cardHand;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < card.Count; i++)
        {
            card[i].GetComponent<Renderer>().enabled = false;
        }
        cardHand.GetComponent<Renderer>().enabled = true;
    }

    void Update()
    {
        
    }
}
