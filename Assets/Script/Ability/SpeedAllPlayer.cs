using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpeedAllPlayer : MonoBehaviourPunCallbacks
{
    private PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        string EnteredName;
        GameObject EnteredObject = other.gameObject;
         EnteredName=EnteredObject.GetComponent<PhotonView>().Owner.NickName;
        
        PickUpPower(EnteredObject,EnteredName);
        //Destroy(gameObject);
    }

    private void PickUpPower(GameObject Player,string Name)
    {
        if (pv.IsMine)
        {
            //InstiantionOf particles;

            Player.gameObject.GetComponent<PhotonView>().RPC("SpeedAllPlayers", RpcTarget.All,Name);
            PowerDestroyed();
            GameManager._gm.Invoke("spawnPowerupB", 20f);
           

        }
    }

    private void PowerDestroyed()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
