using Assets.Scripts;
using Photon.Pun;
using System;
using System.Globalization;
using UnityEngine;

public class Bomb : MonoBehaviour, IPunInstantiateMagicCallback
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private float _delay;
    [SerializeField] private protected LayerMask StopLayer;

    [HideInInspector] public event Action OnBlow;

    private int _flameLength;

    void Start()
    {
        Invoke(nameof(Blow), _delay);
    }

    private void Blow()
    {
        OnBlow?.Invoke();

        var isOwner = _photonView.Owner == PhotonNetwork.LocalPlayer;
        if (!isOwner)
            return;

        PhotonNetwork.Destroy(gameObject);
        var pos = transform.position;
        PhotonNetwork.Instantiate(_fire.name, pos, Quaternion.identity);
        var (left, right, down, up) = (true, true, true, true);
        for (int i = 1; i <= _flameLength; i++)
        {
            if (left)
                left = InstantiateFire(pos, Vector3.left, i);
            if (right)
                right = InstantiateFire(pos, Vector3.right, i);
            if (down)
                down = InstantiateFire(pos, Vector3.down, i);
            if (up)
                up = InstantiateFire(pos, Vector3.up, i);
        }


    }

    private bool InstantiateFire(Vector3 pos, Vector3 direction, int i)
    {
        Vector3 _pos = pos + direction * i;
        var block = Physics2D.OverlapPoint(_pos, StopLayer);
        if (!block)
        {
            PhotonNetwork.Instantiate(_fire.name, _pos, Quaternion.identity);
            return true;
        }
        else
        {
            var attitudeToFire = block.GetComponent<AttitudeToFire>();
            if (attitudeToFire != null)
            {
                switch (attitudeToFire.StatusAttitudeToFire)
                {
                    case AttitudeToFire.AttitudeFire.Absorb:
                        PhotonNetwork.Instantiate(_fire.name, _pos, Quaternion.identity);
                        break;
                    case AttitudeToFire.AttitudeFire.Stops:
                        break;
                }
                return false;
            }
            else { return true; }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.GetComponent<MoveController>() is MoveController)
                return;
        }
        gameObject.layer = LayerMask.NameToLayer("Barriers");
    }

    PhotonView _photonView;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        _photonView = info.photonView;
        var data = _photonView.InstantiationData;
        _flameLength = (int)data[0];
    }
}

