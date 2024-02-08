using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class JoinRandomRoom : MonoBehaviourPunCallbacks
{
    #region PUN CALLBACKS
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = GetRoomOptions();

        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("IsMasterClient" + PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game_v2");
        }
    }
    #endregion

    public void OnJoinRandomRoomButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private static RoomOptions GetRoomOptions()
    {
        //return new RoomOptions { MaxPlayers = 4, PlayerTtl = 30_000, CleanupCacheOnLeave = false };
        return new RoomOptions { MaxPlayers = 4, PlayerTtl = 100, CleanupCacheOnLeave = false };
    }
}
