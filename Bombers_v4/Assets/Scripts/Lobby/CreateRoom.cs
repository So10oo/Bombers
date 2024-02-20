using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField RoomNameInputField;

    public void OnCreateRoomButtonClicked()
    {
        string roomName = RoomNameInputField.text;
        roomName = (roomName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : roomName;
        RoomOptions options = BombersGame.GetRoomOptions();
        PhotonNetwork.CreateRoom(roomName, options, null);
    }
}
