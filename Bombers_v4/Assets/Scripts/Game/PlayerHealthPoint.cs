
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPoint : HealthPoint
{
    [SerializeField] Animator _animator;
    [SerializeField] Image _healsBar;

    bool _isInvulnerable = false;
    bool IsInvulnerable
    {
        get
        {
            return _isInvulnerable;
        }
        set
        {
            _isInvulnerable = value;
            _animator?.SetBool("Invulnerable", value);
            if (_photonView.IsMine)
                _photonView.RPC(nameof(SynchronizationInvulnerable_PunRPC), RpcTarget.Others, value);
        }
    }

    PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    const float _timeInvulnerable = 3f;

    public override void TakeDamage(byte damage)
    {
        if (IsInvulnerable || !_photonView.IsMine) return;

        var pastHp = Hp;
        base.TakeDamage(damage);

        IsInvulnerable = true;
        Invoke(nameof(ResetInvulnerable), _timeInvulnerable);

        _photonView.RPC(nameof(SynchronizationHeal_PunRPC), RpcTarget.All, pastHp, Hp);
    }

    public override void Healing(byte healing)
    {
        if (!_photonView.IsMine) return;

        var pastHp = Hp;
        base.Healing(healing);

        _photonView.RPC(nameof(SynchronizationHeal_PunRPC), RpcTarget.All, pastHp, Hp);
    }

    //private void SynchronizationHp(byte Hp/*, byte presentHp*/)
    //{
    //    PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { BombersGame.PLAYER_LIVES, presentHp } });
    //    //_photonView.RPC(nameof(SynchronizationHeal_PunRPC), RpcTarget.All, pastHp, presentHp);
    //}

    void ResetInvulnerable() => IsInvulnerable = false;

    public override void Die()
    {
        if (!_photonView.IsMine) return;
        Debug.Log($"{PhotonNetwork.LocalPlayer} dead");
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { BombersGame.PLAYER_LIVES, (byte)0 } });
        PhotonNetwork.Destroy(_photonView.gameObject);
        isDead = false;
    }

    [PunRPC]
    void SynchronizationHeal_PunRPC(byte pastHp/*прошлое*/, byte presentHp/*настоящее*/, PhotonMessageInfo info)
    {
        Debug.Log($"pastHp: {pastHp.ToString()}, presentHp: {presentHp.ToString()}");
        if (_photonView == info.photonView)
        {
            _healsBar.fillAmount = (float)presentHp / (float)MaxHp;
            if (presentHp > pastHp)//хил
            {
                _animator?.SetTrigger("Heal");
            }
        }
    }


    [PunRPC]
    void SynchronizationInvulnerable_PunRPC(bool isInvulnerable, PhotonMessageInfo info)
    {
        Debug.Log($"_photonView: {_photonView.ToString()}, info.photonView: {info.photonView.ToString()}");
        if (_photonView == info.photonView)
        {
            IsInvulnerable = isInvulnerable;
        }
    }


}
