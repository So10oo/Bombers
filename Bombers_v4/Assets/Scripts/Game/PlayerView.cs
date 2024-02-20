using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    PhotonView _photonView;
    [SerializeField] SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        //var sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //var i = Array.IndexOf(PhotonNetwork.PlayerList, _photonView.Owner);
        //sprite.color = BombersGame.GetColor(i);

        //var p = (from pl in PhotonNetwork.PlayerList
        //         where pl.ActorNumber == _photonView.CreatorActorNr
        //         select pl).First();

        var player = PhotonNetwork.CurrentRoom.Players.ContainsKey(_photonView.CreatorActorNr) ? PhotonNetwork.CurrentRoom.Players[_photonView.CreatorActorNr] : null;
        if (player == null) return;

        _spriteRenderer.color = Color—ontainer.GetColorPlayer(player);

        //int indexPlayer = Array.IndexOf(PhotonNetwork.PlayerList, player);
        //_spriteRenderer.sortingOrder
    }


}
