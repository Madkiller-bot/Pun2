using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
namespace Photon.Pun.Demo.Asteroids
{
    public class Capturepoint : MonoBehaviourPunCallbacks
    {
        GameManager _gameManager;

        PlayerOnpointScore playerOverview;

        PhotonView pv;

        float timeProgress = 0;

        Player p;

        [SerializeField]
        private Sprite White;

        [SerializeField]
        private Sprite Blue;

        [SerializeField]
        private Sprite Red;

        [SerializeField]
        private int NumofPlyInCircle = 0;
        SpriteRenderer spriteRenderer;

        float timer;
        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (NumofPlyInCircle == 0)
            {
                
                if (NumofPlyInCircle == 0)
                {
                    _gameManager.NameOFCapture.text = "Point Free To Capture";
                    _gameManager.NameOFCapture.color = Color.black;
                    spriteRenderer.color = Color.black;
                }
            }

            
        }
        private void OnTriggerEnter(Collider other)
        {

            NumofPlyInCircle++;
            GameObject EnteredObject = other.gameObject;

            pv = EnteredObject.GetComponent<PhotonView>();
            Debug.Log(pv.Owner.NickName);


            if (NumofPlyInCircle == 1)
            {
                // spriteRenderer.sprite = Red;
                spriteRenderer.color = AsteroidsGame.GetColor(pv.Owner.GetPlayerNumber());
            }

            else
            {
                print(NumofPlyInCircle + "Players Are Contesting");
                _gameManager.NameOFCapture.text = NumofPlyInCircle + "Players Are Contesting";
                _gameManager.NameOFCapture.color = Color.black;
                spriteRenderer.color = Color.white;
            }

        }

        private void OnTriggerStay(Collider other)
        {
            GameObject Enterobject = other.gameObject;
            // PlayerControl Name = Enterobject.GetComponent<PlayerControl>();
            pv = Enterobject.GetComponent<PhotonView>();
            Debug.Log(pv.Owner.NickName + "IsStaying");
            if (NumofPlyInCircle == 1)
            {
                timeProgress = 1f;
                
                pv.Owner.AddScore(Mathf.FloorToInt(timeProgress));              
                _gameManager.NameOFCapture.text = pv.Owner.NickName + "Is Capturing";
                _gameManager.NameOFCapture.color = AsteroidsGame.GetColor(pv.Owner.GetPlayerNumber());
                spriteRenderer.color = AsteroidsGame.GetColor(pv.Owner.GetPlayerNumber());
                Debug.Log(AsteroidsGame.GetColor(pv.Owner.GetPlayerNumber()));

            }
            if (NumofPlyInCircle > 1)
            {
                print("PlayerContestingForthepoint");
                timeProgress = 0f;
                spriteRenderer.color = Color.white;
                //pv.Owner.ResentingScore(0);
            }
            else if(NumofPlyInCircle==0)
            {
               
                timer += Time.deltaTime;
                if (timer >= 10)
                {
                    pv.Owner.ResentingScore(0);
                    timer = 0f;
                }
                print("No One is Staying");
            }

        }


        private void OnTriggerExit(Collider other)
        {
            NumofPlyInCircle--;
            GameObject Enterobject = other.gameObject;
            pv = Enterobject.GetComponent<PhotonView>();
            Debug.Log(pv.Owner.NickName + "Exited");
            //pv.Owner.ResentingScore(0);
            if (NumofPlyInCircle >= 0)
            {
                spriteRenderer.color = Color.white;
            }

        }
    }
}
