using Assets.Scripts;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MoveController
{   
    [SerializeField] GameObject _bomb;

    PhotonView _photonView;
    CharacterTraits _characterTraits = new();

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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            var posInt = Vector2Int.RoundToInt(transform.position);
            var bombs = Physics2D.OverlapPointAll(posInt);
            foreach (var bomb in bombs)
            {
                if (bomb?.gameObject.GetComponent<Bomb>() is not null)
                {
                    return;
                }
            }
            if (_currentBombQuantity < CharacterTraits.bombQuantity)
            {
                _photonView.RPC(nameof(SetBomb), RpcTarget.AllViaServer, new Vector3(posInt.x, posInt.y, 0), _characterTraits.flameLength);
                _currentBombQuantity++;              
            }
                 
        }
         
    }

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
        
        SetMove(GetInput(), _characterTraits.speed);
    }
    #endregion

    #region Pun
    [PunRPC]
    public void SetBomb(Vector3 pos,int fireLengrh, PhotonMessageInfo info)
    {
        //var posInt = Vector2Int.RoundToInt(pos);
        var bomb = Instantiate(_bomb, pos, Quaternion.identity);
        var bombScript = bomb.GetComponent<Bomb>();
        bombScript.FlameLength = fireLengrh;
        if(info.photonView == _photonView)
            bombScript.OnBlow += () => _currentBombQuantity--; 
    }
    #endregion

}

