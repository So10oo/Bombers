using Assets.Scripts;
using Photon.Pun;
using System;
using UnityEngine;

public class PlayerManager : MoveController
{   
    [SerializeField] GameObject _bomb;
    PhotonView _photonView;
    readonly CharacterTraits _characterTraits = new();

    public CharacterTraits CharacterTraits => _characterTraits;

    private int _currentBombQuantity = 0;

    #region MonoBehaviour
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }



    private void Update()
    {
        if (!_photonView.IsMine) return;

        if (Input.GetKey(KeyCode.Space) && _buttonBomb)
        {
            var posInt = Vector2Int.RoundToInt(transform.position);
            var bombs = Physics2D.OverlapPointAll(posInt);
            foreach (var bomb in bombs)
            {
                if (bomb.gameObject.GetComponent<Bomb>() is not null)
                {
                    return;
                }
            }
            if (_currentBombQuantity < CharacterTraits.BombQuantity)
            {
                //_photonView.RPC(nameof(SetBomb), RpcTarget.AllViaServer, new Vector3(posInt.x, posInt.y, 0), _characterTraits.flameLength);
                SetBomb(new Vector3(posInt.x, posInt.y, 0), _characterTraits.FlameLength);
                _currentBombQuantity++;
                _buttonBomb = false;
                Invoke(nameof(RestetButtonBomb), _timeDelaySetBomb);
            }
                 
        }
         
    }

    const float _timeDelaySetBomb = 0.1f;
    bool _buttonBomb = true;
    void RestetButtonBomb() => _buttonBomb = true;

    /*
     * если я двигаюсь влево(вверх вниз вправо (не огриничивая общности да)) есть 3 варианта:
     * 1.я двигаюсь влево
     *  --когда пути хватает до округления.
     * 2. я двигаюсь вверх
     *  -- когда не хватает пути до округления и ближайшее округление выше по координате
     * 3. я двигаюсь вниз
     *  -- когда не хватает пути до округления и ближайшее округление ниже по координате 
     */
    private void FixedUpdate()
    {
        if (!_photonView.IsMine) return;
        
        SetMove(GetInput(), _characterTraits.Speed);
    }
    #endregion

    #region Pun

    public void SetBomb(Vector3 pos, int fireLengrh)
    {      
        object[] objects = { fireLengrh };
        var bomb = PhotonNetwork.Instantiate(_bomb.name, pos, Quaternion.identity, data: objects);
        bomb.GetComponent<Bomb>().OnBlow += () => _currentBombQuantity--;
    }

    #endregion

}

