using Photon.Pun;
using UnityEngine;

public class InitializationPhotonNetwork : MonoBehaviour
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LocalPlayer.NickName = "Player " + Random.Range(1_000, 10_000).ToString();
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.LocalPlayer.TagObject = new TagPlayerInfo();
    }

    //public override void OnConnectedToMaster()
    //{
    //    if (!PhotonNetwork.InLobby)
    //    {
    //        PhotonNetwork.JoinLobby();
    //    }
    //}

}
