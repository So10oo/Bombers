using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("PhotonNetwork")]
    [SerializeField] bool _isOffline;

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #region MonoBehaviour
    private void Awake()
    {
        if (_isOffline && !PhotonNetwork.InLobby)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }
    #endregion

    #region PunCallbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }


    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    PhotonNetwork.DestroyPlayerObjects(otherPlayer);
    //}
    #endregion
}
