using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestNetworking : MonoBehaviourPunCallbacks
{
     public string Name;
    void Start()
    {
        Debug.Log("Conneting To server");
        PhotonNetwork.NickName = Name;
        PhotonNetwork.GameVersion = "0.1";

        PhotonNetwork.ConnectUsingSettings();

    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        Debug.Log("conneted tO Server");
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconneted From Server" + cause.ToString());
    }

}
