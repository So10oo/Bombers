using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("SpawnPlayer")]
    [SerializeField] GameObject _spawnPlayer;

    [Header("End Game")]
    [SerializeField] float _timeStartEndGame;
    [SerializeField] float _timeSetBlock;
    [SerializeField] GameObject _block;
    [SerializeField] Transform _parentBlocks;
    //[SerializeField] bool _isEndGame;

    float TimeStartEndGame
    {
        get => _timeStartEndGame < 1 ? 1 : _timeStartEndGame;
        set => _timeStartEndGame = value;
    }
    float TimeSetBlock
    {
        get => _timeSetBlock < 1 ? 1 : _timeSetBlock;
        set => _timeSetBlock = value;
    }

    #region MonoBehaviour

    public void Start()
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

    #region Private metod

    IEnumerator StartGame()
    {

        var traversal = ClockwiseTraversal(-8, -5, 8, 5);
        yield return new WaitForSeconds(TimeStartEndGame);
        int index = 0;
        while (true)
        {
            if (index < traversal.Count)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    var block = PhotonNetwork.InstantiateRoomObject(_block.name, (Vector2)traversal[index], Quaternion.identity);
                    //block.transform.SetParent(_parentBlocks);
                }
                index++;
                if (index != traversal.Count)
                    yield return new WaitForSeconds(TimeSetBlock);
                else
                    break;
            }
        }
    }

    private void OnCountdownTimerIsExpired()
    {     
        StartCoroutine(StartGame());//запускаем таймер конца игры
        _spawnPlayer.SetActive(true);//Спавним игроков 
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
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


    List<Vector2Int> ClockwiseTraversal(int x_min, int y_min, int x_max, int y_max)
    {
        HashSet<Vector2Int> traversal = new();
        int x = x_min, y = y_min;
        while (true)
        {
            while (y != y_max && traversal.Add(new Vector2Int(x, y)))
                y++;

            while (x != x_max && traversal.Add(new Vector2Int(x, y)))
                x++;

            while (y != y_min && traversal.Add(new Vector2Int(x, y)))
                y--;

            while (x != x_min && traversal.Add(new Vector2Int(x, y)))
                x--;

            if (x_min == x_max || y_max == y_min)
                break;

            x_max--;
            y_max--;
            x = ++x_min;
            y = ++y_min;
        }
        return traversal.ToList();
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

    #endregion
}
