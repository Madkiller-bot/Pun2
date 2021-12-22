using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager _gm;
    public Transform[] SpawnPoints;

    private void Awake()
    {
        if (GameManager._gm == null)
        {
            GameManager._gm = this;
        }
        else
        {
            if (GameManager._gm != this)
            {

                Destroy(GameManager._gm.gameObject);
                GameManager._gm = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);

    }


    [SerializeField]
    public GameObject _CapturepointPrebaf;

    [SerializeField]
    public Transform Capturepoint;

    [SerializeField]
    public Transform[] MoveTransform;

    [SerializeField]
    public Transform[] PowerPoints;

    [SerializeField]
    public Transform[] speedUpTransform;

    [SerializeField]
    private bool Spawned;

    [SerializeField]
    public Text Info;

    
    public Text time;

    public Text NameOFCapture;

    [SerializeField]
    private GameObject WinnerPannel;

    private int MovingPoint = 0;
    public GameObject _spawncppoint;
    GameObject _spawnPowerups;
    GameObject _spawnPowerupB;

    void Start()
    {
        InvokeRepeating("MoveTheCapturePoint", 15f, 15f);
        Invoke("SpawnPowerups", 10f);
        Invoke("spawnPowerupA", 12f);
        Invoke("spawnPowerupB", 15f);
    }


    void Update()
    {
        if (!Spawned)
        {
            //_spawncppoint = Instantiate(_CapturepointPrebaf, Capturepoint.transform.position, _CapturepointPrebaf.transform.rotation);
            _spawncppoint = PhotonNetwork.InstantiateRoomObject(Path.Combine("Prefab", "CapturePoint"), Capturepoint.transform.position, Capturepoint.transform.rotation);
            Spawned = true;
            Debug.Log("CapturePointSpawned");
        }
    }
    
    public void ChecktheWinner(Player player)
    {
        WinnerPannel.SetActive(true);
        WinnerPannel.GetComponentInChildren<Text>().text = player.NickName + "Winner Of The Game";
        Debug.Log(player.NickName + "WinnerOf the Game");
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();

    }
    [PunRPC]
    public void MoveTheCapturePoint()
    {
        MovingPoint = Random.Range(0, MoveTransform.Length);
        Debug.Log("Point Moved to " + MovingPoint);
        _spawncppoint.transform.position = MoveTransform[MovingPoint].transform.position;

    }
    public void SpawnPowerups()
    {
        int random = Random.Range(0, PowerPoints.Length);
        _spawnPowerups = PhotonNetwork.InstantiateRoomObject(Path.Combine("Prefab", "PowerUp1"), PowerPoints[random].transform.position,Quaternion.identity);
    }

    public void spawnPowerupA()
    {
        //int random = Random.Range(0, 4);
        _spawnPowerups = PhotonNetwork.InstantiateRoomObject(Path.Combine("Prefab", "PowerUp2"), PowerPoints[3].transform.position, Quaternion.identity);
    }

    public void spawnPowerupB()
    {
        int random = Random.Range(0, speedUpTransform.Length);
        _spawnPowerupB = PhotonNetwork.InstantiateRoomObject(Path.Combine("Prefab", "PowerUp3"), speedUpTransform[random].transform.position, Quaternion.identity);
    }

    public void New()
    {
        

    }

    
}
