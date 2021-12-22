using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

namespace Photon.Pun.Demo.Asteroids {
    public class PlayerControl : MonoBehaviourPunCallbacks
    {
        public bool isFlat = true;

        [SerializeField]
        public string Name;

        private Rigidbody rb;
        private PhotonView Pv;
        private new Renderer renderer;
        [SerializeField]
        public int Score;
        [SerializeField]
        private float Speed;
        Vector3 tilted;
        bool CanMove = true;
        GameManager GameManager;
        [SerializeField]
        bool ChangeControl = false;
        

        
        // Start is called before the first frame update

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Pv = GetComponent<PhotonView>();
            renderer = GetComponent<Renderer>();
            


        }
        void Start()
        {
            GameManager = FindObjectOfType<GameManager>();
            foreach (Renderer r in GetComponents<Renderer>())
            {
                r.material.color = AsteroidsGame.GetColor(photonView.Owner.GetPlayerNumber());
            }
            Name = PhotonNetwork.LocalPlayer.NickName;
            Debug.Log("PlayerName Is " + Name);
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (!Pv.IsMine)
            {              
                return;
            }

            if (CanMove)
            {
                     tilted = Input.acceleration * Speed;
                if (isFlat)
                    tilted = Quaternion.Euler(90, 0, 0) * tilted;
                if (ChangeControl) 
                {
                    rb.AddForce(tilted.x*(-1), 0, tilted.z*(-1));
                }
                else
                {
                    rb.AddForce(tilted.x, 0, tilted.z);                 
                }

            }
        }

        [PunRPC]
        public void OnPickUpOverSize(string pid)
        {
            gameObject.transform.localScale *= 1.5f;
            gameObject.GetComponent<Rigidbody>().mass = 10;
            GameManager._gm.Info.text =pid+ "Picked OverSize";
            Invoke("BackToNorMal", 10f);
            
        }

        public void BackToNorMal()
        {
            ChangeControl = false;
            gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
            rb.mass = 1f;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(tilted.x, 0, tilted.y));
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            print("BacktoNormal");
            GameManager._gm.Info.text = "BacktoNormal";


        }

        [PunRPC]
        public void GetToThePoint(string pID, Vector3 circleTransform)
        {
            if (PhotonNetwork.LocalPlayer.NickName == pID)
            {

                print("POintMovedtoThepoint");
                CanMove = false;
                gameObject.transform.position = circleTransform;
                CanMove = true;
                GameManager._gm.Info.text = pID + "TelePorted to The Point";
            }
                     
        }
        [PunRPC]
        public void SpeedAllPlayers(string pid)
        {
            Debug.LogError(PhotonNetwork.LocalPlayer.NickName + pid);

            if (PhotonNetwork.LocalPlayer.NickName == pid)
            {
                ChangeControl = false;
                print("Owner");
                GameManager._gm.Info.text = pid + "Changed Everones Control";
                
            }
            else if (PhotonNetwork.LocalPlayer.NickName != pid)
            {
                ChangeControl = true;
                print("COntrolChanged");
                Invoke("BackToNorMal", 10f);
            }
        }


    }
}
