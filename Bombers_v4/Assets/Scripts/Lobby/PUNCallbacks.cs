using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PUNCallbacks : MonoBehaviourPunCallbacks
{
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Комната " + Random.Range(1000, 10000);

        RoomOptions options = BombersGame.GetRoomOptions();

        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("IsMasterClient" + PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }


}
