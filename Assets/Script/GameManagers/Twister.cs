using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Twister : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime*Speed,Space.Self);
    }
}
