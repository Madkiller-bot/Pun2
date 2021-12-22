using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PhotonViewForPlayer : MonoBehaviour
{
    private PhotonView _pv;
    [SerializeField]
    private GameObject _myAvator;
    // Start is called before the first frame update
    void Start()
    {
        _pv = GetComponent<PhotonView>();
        int spwanpoint = Random.Range(0, GameManager._gm.SpawnPoints.Length);
        if (_pv.IsMine)
        {
          _myAvator= PhotonNetwork.Instantiate(Path.Combine("Prefab", "PlayerBallAvatar"), GameManager._gm.SpawnPoints[spwanpoint].position, GameManager._gm.SpawnPoints[spwanpoint].rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
