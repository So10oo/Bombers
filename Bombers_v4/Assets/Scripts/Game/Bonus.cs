using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour, IPunInstantiateMagicCallback
{
    PhotonView _photonView;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        _photonView = info.photonView;   
    }

    public virtual void UpBonus(CharacterTraits characterTraits)
    { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>() is PlayerManager player)
        {
            UpBonus(player.CharacterTraits);
            Destroy(gameObject);
        }
    }
}
