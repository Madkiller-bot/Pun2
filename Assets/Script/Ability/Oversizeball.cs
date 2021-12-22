using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
namespace Photon.Pun.Demo.Asteroids
{

    public class Oversizeball : MonoBehaviour
    {
        private PhotonView pv;

        private void Start()
        {
            pv = GetComponent<PhotonView>();
        }
        private void OnTriggerEnter(Collider other)
        {
            GameObject EnteredObject = other.gameObject;
            PickUpPower(EnteredObject);
           // Destroy(gameObject);
        }

        private void PickUpPower(GameObject Player)
        {
            if (pv.IsMine)
            {
                //InstiantionOf particles;
               // Player.gameObject.GetComponent<PhotonView>().RPC("OnPickUpOverSize", RpcTarget.All);
                Player.gameObject.GetComponent<PhotonView>().RPC("OnPickUpOverSize", RpcTarget.All, Player.GetComponent<PhotonView>().Owner.NickName);
                PowerDestroyed();
                GameManager._gm.Invoke("SpawnPowerups", 10f);
                
                

            }        
        }

        private void PowerDestroyed()
        {
            PhotonNetwork.Destroy(gameObject);

        }
    }
}
