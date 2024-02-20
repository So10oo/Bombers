using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class JoinRandomRoom : MonoBehaviour
{
    public void OnJoinRandomRoomButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
    }
}
