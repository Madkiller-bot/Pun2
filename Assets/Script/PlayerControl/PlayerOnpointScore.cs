using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Photon.Pun.Demo.Asteroids
{
    public class PlayerOnpointScore : MonoBehaviourPunCallbacks
    {
        float FillAmount;
        // Start is called before the first frame update
        public GameObject PlayerOverviewEntryPrefab;

        GameManager gameManager;

        public Dictionary<int, GameObject> playerListEntries;

        public void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            playerListEntries = new Dictionary<int, GameObject>();
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                GameObject entry = Instantiate(PlayerOverviewEntryPrefab);
                entry.transform.SetParent(gameObject.transform);
                entry.transform.localScale = Vector3.one;
                entry.GetComponent<Image>().color = AsteroidsGame.GetColor(p.GetPlayerNumber());
                entry.GetComponentInChildren<Text>().text = p.NickName;
                entry.GetComponentInChildren<Text>().color = AsteroidsGame.GetColor(p.GetPlayerNumber());
                // entry.GetComponent<Text>().text = string.Format("{0}\nScore: {1}\nLives: {2}", p.NickName, p.GetScore(), AsteroidsGame.PLAYER_MAX_LIVES);
                playerListEntries.Add(p.ActorNumber, entry);
            }


        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            GameObject entry;
            if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
            {
                float FillValue;
                FillAmount = targetPlayer.GetScore();
                Debug.Log(FillAmount);
                if (FillAmount == 0)
                {
                    FillValue = FillAmount;
                }
                FillValue = FillAmount/250;
                entry.GetComponent<Image>().fillAmount = FillValue;
                if (FillValue >= 1f)
                {                        
                    gameManager.ChecktheWinner(targetPlayer);
                }
                
                // entry.GetComponentInChildren<Text>().text = targetPlayer.GetScore().ToString() ;
            }
        }

    }
}
