using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetworkBehaViour : MonoBehaviourPunCallbacks
{
       public string PlayerName
    {
        get;
        set;
    }

    [SerializeField]
    public GameObject cancelbuttoon;

    public GameObject BattelButton;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Conneting To server");
        PhotonNetwork.NickName = PlayerName;
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("conneted tO Server");
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

     public void OnClickBattelButton()
    {
        BattelButton.SetActive(false);
        cancelbuttoon.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("There is no open Room Avaliable");
        CreateroomNew();
        

    }

    private void CreateroomNew()
    {
        RoomOptions roomops = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Room" + PlayerName, roomops);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("New Rooom Creattion Failed");
        CreateroomNew();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconneted From Server" + cause.ToString());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We are in a room");
    }

    public void OnCancelButton()
    {
        cancelbuttoon.SetActive(false);
        BattelButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    // Update is called once per frame
   
}
