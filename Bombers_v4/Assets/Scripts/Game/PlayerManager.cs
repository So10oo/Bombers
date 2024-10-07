using Assets.Scripts;
using Photon.Pun;
using System;
using UnityEngine;

public class PlayerManager : MoveController, IPunInstantiateMagicCallback
{   
    [SerializeField] GameObject _bomb;
    readonly CharacterTraits _characterTraits = new();
    public CharacterTraits CharacterTraits => _characterTraits;


    private int _currentBombQuantity = 0;
    private void SetBomb(Vector3 pos, int fireLengrh)
    {
        object[] objects = { fireLengrh };
        var bomb = PhotonNetwork.Instantiate(_bomb.name, pos, Quaternion.identity, data: objects);
        bomb.GetComponent<Bomb>().OnBlow += () => _currentBombQuantity--;
    }

    float _timeDelaySetBomb = 0.1f;
    bool _buttonBomb = true;
    void RestetButtonBomb() => _buttonBomb = true;
    private void BombManager()
    {
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
                SetBomb(new Vector3(posInt.x, posInt.y, 0), _characterTraits.FlameLength);
                _currentBombQuantity++;
                _buttonBomb = false;
                Invoke(nameof(RestetButtonBomb), _timeDelaySetBomb);
            }

        }
    }

    #region MonoBehaviour

    private void Update()
    {
        BombManager();
        SetMove(GetInput(), _characterTraits.Speed);
    }

    /*
     * если я двигаюсь влево(вверх вниз вправо (не огриничивая общности)) есть 3 варианта:
     * 1.я двигаюсь влево
     *  --когда пути хватает до округления.
     * 2. я двигаюсь вверх
     *  -- когда не хватает пути до округления и ближайшее округление выше по координате
     * 3. я двигаюсь вниз
     *  -- когда не хватает пути до округления и ближайшее округление ниже по координате 
     */
    #endregion

    #region Pun

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (!info.photonView.IsMine)
        {    
            this.enabled = false;
            var healthPoint = GetComponent<PlayerHealthPoint>();
            healthPoint.enabled = false; 
        }
    }

    #endregion

}

