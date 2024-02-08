using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("GameManager")]
    [SerializeField] GameManager _gameManager;

    [Header("PhotonNetwork")]
    [SerializeField] bool _isOffline;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    bool wasConnected = true;

    #region public metod
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void Disconnect()
    {
        Debug.Log("Disconnect");
        PhotonNetwork.Disconnect();
    }

    public void Reconnect()
    {
        if (!PhotonNetwork.IsConnected && wasConnected)
        {
            PhotonNetwork.ReconnectAndRejoin();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        if (_isOffline && !PhotonNetwork.InLobby)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }

    private void Start()
    {
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            {BombersGame.PLAYER_LOADED_LEVEL, true}
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    }
    #endregion

    #region PunCallbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(BombersGame.PLAYER_LIVES))
        {
            ChackEndGame();
        }
        if (changedProps.ContainsKey(BombersGame.PLAYER_LOADED_LEVEL))
        {
            int startTimestamp;
            bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);

            if (changedProps.ContainsKey(BombersGame.PLAYER_LOADED_LEVEL))
            {
                if (CheckAllPlayerLoadedLevel())
                {
                    if (!startTimeIsSet)
                    {
                        CountdownTimer.SetStartTime();
                    }
                }
            }
        }

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
       // PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        if (!PhotonNetwork.IsMasterClient) return;
        otherPlayer.SetCustomProperties(new Hashtable { { BombersGame.PLAYER_LIVES, (byte)0 } });
    }
    #endregion

    #region private metod

    private void ChackEndGame()
    {
        //if (!PhotonNetwork.IsMasterClient) return;
        var livePlayers = (from p in PhotonNetwork.PlayerList
                           where p.CustomProperties.ContainsKey(BombersGame.PLAYER_LIVES) && (((byte)p.CustomProperties[BombersGame.PLAYER_LIVES]) > 0)
                           select p).ToArray();

        //List<Player> list = new List<Player>();
        //foreach (var p in PhotonNetwork.PlayerList)
        //{
        //    object lives;
        //    if (p?.CustomProperties is not null && p.CustomProperties.TryGetValue(BombersGame.PLAYER_LIVES, out lives))
        //    {
        //        var l = (byte)lives;
        //        if (l > 0)
        //            list.Add(p);
        //    }

        //}
        //var livePlayers = list.ToArray();

        if (livePlayers.Length == 1)
            EndGame(livePlayers.First());
    }

    private void EndGame(Player p)
    {
        textMeshProUGUI.text = "Игра закончена! Победил " + p.NickName;
       
        _gameManager.EndGame();
    }

    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(BombersGame.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }
            return false;
        }
        return true;
    }

    private void OnCountdownTimerIsExpired()
    {
        _gameManager.Initialization();
        _gameManager.StartGame();
        //StartCoroutine(_gameManager.StartEndGame());//запускаем таймер конца игры
        //_spawnPlayer.SetActive(true);//Спавним игроков 
    }
    #endregion
}
