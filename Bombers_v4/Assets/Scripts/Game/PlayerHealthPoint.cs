
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPoint : HealthPoint//, IPunObservable
{
    [SerializeField] Image imageHp;

    bool isInvulnerable = false;

    PhotonView _photonView;
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    protected override void OnHpChandged(byte value)
    {
        if (_hp > value && !isInvulnerable)
        {
            _hp = value;
            Synchronization(_hp);
            if (_hp <= 0 && !isDead)
            {
                Die();
            }
            isInvulnerable = true;
            Invoke(nameof(ResetInvulnerable), 2f);
        }
        else if(_hp < value)
        {
            _hp = MaxHp < value ? MaxHp : value;
            Synchronization(_hp);
        }
        imageHp.fillAmount = _hp / (float)MaxHp;

    }

    private void Synchronization(byte hp)
    {
        if (!_photonView.IsMine) return;

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { BombersGame.PLAYER_LIVES, hp } });
    }

    void ResetInvulnerable() => isInvulnerable = false;
    
    public override void Die()
    {
        if (!_photonView.IsMine)
            return;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { BombersGame.PLAYER_LIVES, (byte)0 } });
        PhotonNetwork.Destroy(_photonView.gameObject);
        isDead = false;
    }
}