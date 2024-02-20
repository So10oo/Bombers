using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class ButtonReady : MonoBehaviour
{
    TextMeshProUGUI _textButton;

    const string ready =  "Ожидание других игроков...";
    const string noready = "Готов?";

    private void Awake()
    {
        _textButton = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        bool isReady = IsReady(PhotonNetwork.LocalPlayer);
        _textButton.text = isReady ? ready : noready;
    }

    private void Start()
    {
        var props = new ExitGames.Client.Photon.Hashtable { { BombersGame.PLAYER_READY, false } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public void ChangeReadiness()
    {
        bool isReady = !IsReady(PhotonNetwork.LocalPlayer);
        _textButton.text = isReady ? ready : noready;

        var props = new ExitGames.Client.Photon.Hashtable { { BombersGame.PLAYER_READY, isReady } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public static bool IsReady(Player player)
    {
        if (player.CustomProperties.ContainsKey(BombersGame.PLAYER_READY))
        {
            return (bool)player.CustomProperties[BombersGame.PLAYER_READY];
        }
        else 
            return false;
    }
}
