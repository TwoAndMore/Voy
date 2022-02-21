using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayerListCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] _players;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
        }
    }
}
