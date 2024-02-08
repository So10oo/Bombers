using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInRoomList : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _playerUIView;
    [SerializeField] Transform[] _parents;

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Color—ontainer.GetFree—olor();
        }
    }

    public override void OnJoinedRoom()
    {
        Color—ontainer.GetFree—olor();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        ListUpdate();
    }


    //List<GameObject> _playersUIView = new();

    void ListUpdate()
    {
        //_playersUIView.ForEach(p => Destroy(p));
        //_playersUIView.Clear();
        //foreach (var player in PhotonNetwork.PlayerList)
        //{
        //    var UIcomponent = Instantiate(_playerUIView, _parents.position, Quaternion.identity);
        //    UIcomponent.transform.SetParent(_parents);
        //    UIcomponent.transform.localScale = Vector3.one;
        //    UIcomponent.GetComponent<PlayerUIView>().Initialization(player, Color—ontainer.GetColorPlayer(player));
        //    _playersUIView.Add(UIcomponent);
        //}

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            var player = PhotonNetwork.PlayerList[i];   
            _parents[i].gameObject.GetComponent<PlayerUIView>().Initialization(player, Color—ontainer.GetColorPlayer(player));
        }


    }
}
