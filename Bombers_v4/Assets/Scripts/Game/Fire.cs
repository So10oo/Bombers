using Photon.Pun;
using UnityEngine;

public class Fire : Damager, IPunInstantiateMagicCallback
{
    [SerializeField] private float _delay;

    private void Start()
    {
        Invoke(nameof(DestroyGameObject), _delay);
    }

    void DestroyGameObject()  
    {
        var isOwner = _photonView.Owner == PhotonNetwork.LocalPlayer;
        if (!isOwner)
            return;
        PhotonNetwork.Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameobject = collision.gameObject.GetComponent<HealthPoint>();
        if (gameobject != null)
            gameobject.TakeDamage(Damage);
    }

    PhotonView _photonView;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        _photonView = info.photonView;
    }
}
