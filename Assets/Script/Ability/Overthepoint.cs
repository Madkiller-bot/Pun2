using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Photon.Pun.Demo.Asteroids
{
    public class Overthepoint : MonoBehaviour
    {
        private PhotonView pv;
        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject EnteredObject = other.gameObject;
            PickUpPower(EnteredObject);
            //Destroy(gameObject);
        }

        private void PickUpPower(GameObject Player)
        {
            if (pv.IsMine)
            {
                //InstiantionOf particles;

                Player.gameObject.GetComponent<PhotonView>().RPC("GetToThePoint", RpcTarget.All, Player.GetComponent<PhotonView>().Owner.NickName, GameManager._gm._spawncppoint.transform.position);
                PowerDestroyed();
                GameManager._gm.Invoke("spawnPowerupA", 18f);
               
                

            }
        }

        private void PowerDestroyed()
        {
            PhotonNetwork.Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
