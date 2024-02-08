using Photon.Pun;
using TMPro;
using UnityEngine;

public class testInfo : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
    //    text.text = PhotonNetwork.CurrentLobby.ToString()+"\n" +
    //        PhotonNetwork.PlayerList.ToString();
    }
    private void Update()
    {
        string a = string.Empty;
        a += "InLobby: " + PhotonNetwork.InLobby.ToString() + "\n";
        a += "InRoom: " + PhotonNetwork.InRoom.ToString() + "\n";
        a += "IsMasterClient: " + PhotonNetwork.IsMasterClient.ToString() + "\n";
        a += PhotonNetwork.CurrentRoom?.ToString() + "\n";
        foreach (var item in PhotonNetwork.PlayerList)
        {
            a += item.ToString() + " " + "ActorNumber" + item.ActorNumber + " " + "UserId " + item.UserId + " " + "HasRejoined " + item.HasRejoined + "\n";
        }
        text.text = a;
    }


}
