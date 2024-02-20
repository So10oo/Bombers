using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //[Header("Display")]
    //[SerializeField] DisplayEntity _displayCharacterTraits;

    //[Header("SpawnPlayer")]
    //[SerializeField] SpawnPlayer _spawnPlayer;

    [Header("Events")]
    [SerializeField] UnityEvent _afterEndGame;
    [SerializeField] float _timeAfterEndGame;

    [Header("End Game")]
    [SerializeField] float _timeStartEndGame;
    [SerializeField] float _timeSetBlock;
    [SerializeField] GameObject _block;
    [SerializeField] Transform _parentBlocks;

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
    Coroutine _coroutineGame;

    #region Public metod
    public void StartGame()
    {
         _coroutineGame = StartCoroutine(StartEndGame());
    }

    public void EndGame()
    {
        var tagPlayerInfo = PhotonNetwork.LocalPlayer.TagObject as TagPlayerInfo;
        if (tagPlayerInfo.PlayerGameObject is GameObject playerObject && playerObject != null)
        {
            playerObject.GetComponent<PlayerManager>().enabled = false;
        }

        if(_coroutineGame != null) 
            StopCoroutine(_coroutineGame);

        Invoke(nameof(AfterEndGame), _timeAfterEndGame < 0 ? 0 : _timeAfterEndGame);
    }

    #endregion

    #region MonoBehaviour

    #endregion

    #region Private metod
    void AfterEndGame()
    {
        _afterEndGame?.Invoke();
    }

    IEnumerator StartEndGame()
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

    #endregion
}
