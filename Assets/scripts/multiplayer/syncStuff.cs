using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syncStuff : MonoBehaviour
{
    public PhotonView view;
    string mineId;
    private void Awake()
    {
        if (view.IsMine)
        { PlayerPrefs.SetString("playerID", PhotonNetwork.LocalPlayer.UserId); mineId = PhotonNetwork.LocalPlayer.UserId; }
        
    }

    private void Update()
    {
        if (!view.IsMine)
            Debug.Log("my id: "+ mineId + "other player id: "+PlayerPrefs.GetString("playerID"));
    }
}
