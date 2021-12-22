using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingInfo : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;

    public RoomInfo Roominfo { get; private set; }
    public void SetRoomInfo(RoomInfo info)
    {
        Roominfo = info;
        _text.text = info.MaxPlayers + "," + info.Name;
    }

   
}
