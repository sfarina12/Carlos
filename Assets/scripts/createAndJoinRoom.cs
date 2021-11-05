using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class createAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField address;
    public TMP_InputField nickname;
    public TextMeshProUGUI connecting;
    public popupMessage mshHandler;
    public string errorJoin;
    public string errorCreate;

    void Start() { PhotonNetwork.ConnectUsingSettings(); }
    private void Awake() { PhotonNetwork.AutomaticallySyncScene = true; }

    //--------------------------------------------------------------------------

    public override void OnConnectedToMaster() { PhotonNetwork.JoinLobby(); }

    public void createRoom() 
    { 
        string name = address.text;
        if (name.Equals(""))
            emptyField();
        else
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = false;
            roomOptions.PublishUserId = true;
            PhotonNetwork.CreateRoom(name, roomOptions); 
        }
    }
    public void JoinRoom() 
    {
        string name = address.text;
        if (name.Equals(""))
            emptyField();
        else
        {
            PhotonNetwork.LocalPlayer.NickName = nickname.text.Equals("") ? "random player" : nickname.text;
            PhotonNetwork.JoinRoom(name);
        }
    }

    //--------------------------------------------------------------------------
    public override void OnJoinRoomFailed(short returnCode, string message) { mshHandler.playMessage(errorJoin); }
    private void emptyField() { mshHandler.playMessage(errorCreate); }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = nickname.text.Equals("") ? "random player" : nickname.text;
        PhotonNetwork.LoadLevel("detroit");
    }
    public override void OnJoinedLobby() { connecting.text = "Connected"; }
}
