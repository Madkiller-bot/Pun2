using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class PhotonRoom : MonoBehaviourPunCallbacks,IInRoomCallbacks
{

    public static PhotonRoom room;
    private PhotonView pv;

    

    public int _multiplayerScene; 
    public int CurrentScene;


    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this) 
            {

                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);
        pv = GetComponent<PhotonView>(); 
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneLoading;
    }

    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoading;
    }

    private void OnSceneLoading(Scene scene, LoadSceneMode arg1)
    {
        CurrentScene = scene.buildIndex;
        if (CurrentScene == _multiplayerScene)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefab", "PhotonPlayer"), transform.position, Quaternion.identity);     
       // Capturepoint= PhotonNetwork.Instantiate(Path.Combine("Prefab", "CapturePoint"), transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update


    public override void OnJoinedRoom()
    {
        Debug.Log("We are now in a Room");
        if (!PhotonNetwork.IsMasterClient)
            return;
       
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(_multiplayerScene);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("playerEntered");
    }

   

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(1);
    }

  

    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby_AV2");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
