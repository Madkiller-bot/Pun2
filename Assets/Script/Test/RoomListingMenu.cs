using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private RoomListingInfo _roomListingPrefab;

    private List<RoomListingInfo> listingInfos = new List<RoomListingInfo>();

    // Start is called before the first frame update
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       foreach(RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                int index = listingInfos.FindIndex(x => x.Roominfo.Name == roomInfo.Name);
                if (index != -1)
                {
                    Destroy(listingInfos[index].gameObject);
                    listingInfos.RemoveAt(index);
                    Debug.Log("RoomMemoved" + roomInfo.Name);
                }
            }
            else
            {
                RoomListingInfo roomListing = Instantiate(_roomListingPrefab, _content);
                if (roomListing != null)
                {
                    roomListing.SetRoomInfo(roomInfo);
                    listingInfos.Add(roomListing);
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
