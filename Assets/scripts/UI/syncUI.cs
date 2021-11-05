using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syncUI : MonoBehaviour
{
    public PhotonView view;
    public GameObject UI;

    void Update()
    {
        if (view.IsMine)
        {
            UI.active = true;
        }
    }
}
