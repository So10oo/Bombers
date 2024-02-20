using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("PhotonNetwork")]
    [SerializeField] bool _isOffline;

    [Header("GameEvents")]
    [SerializeField] UnityEvent _startTimer;
    [SerializeField] UnityEvent _startGame;
    [SerializeField] UnityEvent _endGame;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    //bool wasConnected = true;
    bool _isGameRunning = false;

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

    //public void Reconnect()
    //{
    //    if (!PhotonNetwork.IsConnected && wasConnected)
    //    {
    //        PhotonNetwork.ReconnectAndRejoin();
    //    }
    //    else
    //    {
    //        PhotonNetwork.ConnectUsingSettings();
    //    }
    //}

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        if (_isOffline && !PhotonNetwork.InLobby)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        if (PhotonNetwork.InRoom)
        {
            SetStartPlayerInRoomCustomProperties();
        }

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
    //    var tagPlayerInfo = (TagPlayerInfo)PhotonNetwork.LocalPlayer.TagObject;
    //    if (tagPlayerInfo.PlayerGameObject != null)
    //        PhotonNetwork.Destroy(tagPlayerInfo.PlayerGameObject);
        SceneManager.LoadScene("Lobby");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(BombersGame.PLAYER_LIVES))
        {
            ChackEndGame();
        }
        if (changedProps.ContainsKey(BombersGame.PLAYER_READY))
        {
            //if (!PhotonNetwork.IsMasterClient) return;

            //int startTimestamp;
            //bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);
            if (CheckAllPlayerLoadedLevel())
            {
                //if (!startTimeIsSet)
                //{
                _startTimer?.Invoke();
                _isGameRunning = true;
                if (!PhotonNetwork.IsMasterClient) return;

                CountdownTimer.SetStartTime();
                //}
            }
        }

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //Debug.Log("OnPlayerLeftRoom1");
        if (!PhotonNetwork.IsMasterClient) return;
        //Debug.Log("OnPlayerLeftRoom2");

        otherPlayer.SetCustomProperties(new Hashtable { { BombersGame.PLAYER_LIVES, (byte)0 } });

        PhotonNetwork.DestroyPlayerObjects(otherPlayer);

        var propCurrentRoom = new ExitGames.Client.Photon.Hashtable();
        propCurrentRoom[otherPlayer.ActorNumber.ToString()] = string.Empty;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propCurrentRoom);
    }

    public override void OnJoinedRoom()
    {
        SetStartPlayerInRoomCustomProperties();
    }
    #endregion

    #region private metod

    private void ChackEndGame()
    {
        //Debug.Log("ChackEndGameStart");

        if (!_isGameRunning) return;

        //foreach (var p in PhotonNetwork.PlayerList)
        //{
        //    if (p.CustomProperties.ContainsKey(BombersGame.PLAYER_LIVES))
        //    {
        //        Debug.Log($"player-{p} ---- LIVES {p.CustomProperties[BombersGame.PLAYER_LIVES]}");
        //    }
        //    else
        //    {
        //        Debug.Log("No key BombersGame.PLAYER_LIVES");
        //    }
        //}

        var livePlayers = (from p in PhotonNetwork.PlayerList
                           where p.CustomProperties.ContainsKey(BombersGame.PLAYER_LIVES) && (((byte)p.CustomProperties[BombersGame.PLAYER_LIVES]) > 0)
                           select p).ToArray();

        //List<Player> list = new List<Player>();
        //foreach (var p in PhotonNetwork.PlayerList)
        //{
        //    object lives;
        //    if (p?.CustomProperties is not null && p.CustomProperties.TryGetValue(BombersGame.PLAYER_LIVES, out lives))
        //    {
        //        Debug.Log("Lives " + lives.ToString());
        //        //var l = (int)lives;
        //        //if (l > 0)
        //        //    list.Add(p);
        //    }
        //}
        //var livePlayers = list.ToArray();

        //Debug.Log("ChackEndGame " + livePlayers);

        if (livePlayers.Length == 1)
        {
            EndGame(livePlayers.First());
            _endGame?.Invoke();
        }
    }

    private void EndGame(Player winner)
    {
        textMeshProUGUI.text = "Игра закончена! Победил " + winner.NickName;
        _isGameRunning = false;
        SetStartPlayerInRoomCustomProperties();
        _endGame?.Invoke();
    }

    private bool CheckAllPlayerLoadedLevel()
    {
#if UNITY_EDITOR

#else
        if (PhotonNetwork.PlayerList.Length <= 1) return false;
#endif

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;
            if (p.CustomProperties.TryGetValue(BombersGame.PLAYER_READY, out playerLoadedLevel))
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
        _startGame?.Invoke();
    }

    private void SetStartPlayerInRoomCustomProperties()
    {
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable { { BombersGame.PLAYER_READY, false }, { BombersGame.PLAYER_LIVES, BombersGame.PLAYER_MAX_LIVES } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    #endregion
}
