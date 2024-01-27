using Photon.Pun;
using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        var sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        var i = Array.IndexOf(PhotonNetwork.PlayerList, _photonView.Owner);
        sprite.color = BombersGame.GetColor(i);
    }


}
