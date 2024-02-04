using Photon.Pun;
using UnityEngine;

public abstract class Bonus: MonoBehaviour, IPunInstantiateMagicCallback
{
    PhotonView _photonView;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        _photonView = info.photonView;   
    }

    public virtual void UpBonus(PlayerManager playerManager)
    { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>() is PlayerManager player)
        {
            UpBonus(player);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            if (!PhotonNetwork.IsMasterClient) return;
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
