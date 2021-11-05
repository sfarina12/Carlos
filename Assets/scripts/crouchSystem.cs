using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchSystem : MonoBehaviour
{
    public Transform player;
    float startingHeight;
    float crouchHeight;

    void Start()
    {
        startingHeight = player.localScale.y;
        crouchHeight = startingHeight / 2;
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            player.localScale = new Vector3(player.localScale.x, crouchHeight, player.localScale.z);
        else
            player.localScale = new Vector3(player.localScale.x, startingHeight, player.localScale.z);
    }
}
