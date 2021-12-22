using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Text __roomlisting;
  

    public void OnclickRoomCreate()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(__roomlisting.text, roomOptions, TypedLobby.Default);

    }

    public override void OnCreatedRoom()
    {
        Debug.Log("RoomSuccefullyCreated");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Creating Failed" + message);
    }
    
}
