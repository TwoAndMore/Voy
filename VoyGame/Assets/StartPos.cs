using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class StartPos : MonoBehaviourPunCallbacks
{
    public Transform position1;
    
    private void Start()
    {
        //StartPose(position1);
    }

    [PunRPC]
    private void StartPose(Transform _itemsPosition)
    {
        transform.parent = _itemsPosition;
        transform.localPosition = new Vector3(0f, 0.03f, 0.15f);
        transform.localRotation = Quaternion.Euler(-90, 0, 30);
    }
}
