using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("PhotonNetwork")]
    [SerializeField] bool _isOffline;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

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

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        ChackEndGame();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        otherPlayer.SetCustomProperties(new Hashtable { { BombersGame.PLAYER_LIVES, 0 } });
    }

    #endregion

    private void ChackEndGame()
    {
        var livePlayers = (from p in PhotonNetwork.PlayerList
                 where p.CustomProperties.ContainsKey(BombersGame.PLAYER_LIVES) && (byte)p.CustomProperties[BombersGame.PLAYER_LIVES] > 0
                 select p).ToArray();
        if (livePlayers.Length == 1)
            EndGame(livePlayers.First());
    }

    private void EndGame(Player p)
    {
        textMeshProUGUI.text = "Игра закончена! Победил " + p.NickName;
    }
}
