using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ObservableHealthPoint : HealthPoint, IPunObservable
{
    [SerializeField] Image imageHp;

    public override void StartMonoBehaviour()
    {
        base.StartMonoBehaviour();
        OnDamage += OnTakeDamage;
    }

    private void OnTakeDamage(float hp)
    {
        imageHp.fillAmount = hp;
        isInvulnerable = true;
        Invoke(nameof(ResetInvulnerable), 2f);
    }

    void ResetInvulnerable()
    {
        isInvulnerable = false;
    }

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.Hp);
        }
        else
        {
            this.Hp = (float)stream.ReceiveNext();
        }
    }
    #endregion
}
