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

    //test
    [SerializeField] GameObject c_fire;
    [SerializeField] GameObject l_fire;
    [SerializeField] GameObject k_fire;


    private int _flameLength;

    private float _radius;
    private void Awake()
    {
        _radius = GetComponent<CircleCollider2D>().radius - 0.01f;//без комментариев 
    }

    void Start()
    {
        Invoke(nameof(Blow_v2), _delay);
    }

    //private void Blow()
    //{
    //    OnBlow?.Invoke();

    //    var isOwner = _photonView.Owner == PhotonNetwork.LocalPlayer;
    //    if (!isOwner)
    //        return;

    //    PhotonNetwork.Destroy(gameObject);
    //    var pos = transform.position;
    //    PhotonNetwork.Instantiate(_fire.name, pos, Quaternion.identity);
    //    var (left, right, down, up) = (true, true, true, true);
    //    for (int i = 1; i <= _flameLength; i++)
    //    {
    //        if (left)
    //            left = InstantiateFire(pos, Vector3.left, i);
    //        if (right)
    //            right = InstantiateFire(pos, Vector3.right, i);
    //        if (down)
    //            down = InstantiateFire(pos, Vector3.down, i);
    //        if (up)
    //            up = InstantiateFire(pos, Vector3.up, i);
    //    }


    //}

    //private bool InstantiateFire(Vector3 pos, Vector3 direction, int i)
    //{
    //    Vector3 _pos = pos + direction * i; 
    //    var block = Physics2D.OverlapPoint(_pos, StopLayer);
    //    if (!block)
    //    {
    //        PhotonNetwork.Instantiate(_fire.name, _pos, Quaternion.identity);
    //        return true;
    //    }
    //    else
    //    {
    //        var attitudeToFire = block.GetComponent<AttitudeToFire>();
    //        if (attitudeToFire != null)
    //        {
    //            switch (attitudeToFire.StatusAttitudeToFire)
    //            {
    //                case AttitudeToFire.AttitudeFire.Absorb:
    //                    PhotonNetwork.Instantiate(_fire.name, _pos, Quaternion.identity);
    //                    break;
    //                case AttitudeToFire.AttitudeFire.Stops:
    //                    break;
    //            }
    //            return false;
    //        }
    //        else
    //        {
    //            PhotonNetwork.Instantiate(_fire.name, _pos, Quaternion.identity);
    //            return true;
    //        }
    //    }
    //}



    private void Blow_v2()
    {
        OnBlow?.Invoke();

        var isOwner = _photonView.Owner == PhotonNetwork.LocalPlayer;
        if (!isOwner)
            return;

        PhotonNetwork.Destroy(gameObject);
        var pos = transform.position;
        PhotonNetwork.Instantiate(c_fire.name, pos, Quaternion.identity);
        var (left, right, down, up) = (true, true, true, true);
        for (int i = 1; i <= _flameLength; i++)
        {
            if (left)
                left = InstantiateFire_v2(pos, Vector3.left, i, _flameLength);
            if (right)
                right = InstantiateFire_v2(pos, Vector3.right, i, _flameLength);
            if (down)
                down = InstantiateFire_v2(pos, Vector3.down, i, _flameLength);
            if (up)
                up = InstantiateFire_v2(pos, Vector3.up, i, _flameLength);
        }


    }

    private bool InstantiateFire_v2(Vector3 pos, Vector3 direction, int i, int imax)
    {
        Vector3 _pos = pos + direction * i;
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.right, direction);
        var canSetCurrentBlock = CanAttitudeFireBlock(_pos);
        //if (canSetCurrentBlock == AttitudeToFire.AttitudeFire.Absorb)
        //{
        //    if (i == imax)
        //    {
        //        PhotonNetwork.Instantiate(k_fire.name, _pos, quaternion);
        //        return false;
        //    }
        //    Vector3 _posNext = pos + direction * (i + 1);
        //    var canSetNextBlock = CanAttitudeFireBlock(_posNext);
        //    if (canSetNextBlock)
        //    {
        //        PhotonNetwork.Instantiate(l_fire.name, _pos, quaternion);
        //        return true;
        //    }
        //    else
        //    {
        //        PhotonNetwork.Instantiate(k_fire.name, _pos, quaternion);
        //        return false;
        //    }
        //}
        //else
        //{
        //    return false;
        //}
        if (canSetCurrentBlock == AttitudeToFire.AttitudeFire.Skips)
        {
            if (i == imax)
            {
                PhotonNetwork.Instantiate(k_fire.name, _pos, quaternion);
                return false;
            }
            else
            {
                Vector3 _posNext = pos + direction * (i + 1);
                var canSetNextBlock = CanAttitudeFireBlock(_posNext);
                if (canSetNextBlock == AttitudeToFire.AttitudeFire.Stops)
                {
                    PhotonNetwork.Instantiate(k_fire.name, _pos, quaternion);
                    return false;
                }
                else
                {
                    PhotonNetwork.Instantiate(l_fire.name, _pos, quaternion);
                    return true;
                }
            }
        }
        else if (canSetCurrentBlock == AttitudeToFire.AttitudeFire.Absorb)
        {
            PhotonNetwork.Instantiate(k_fire.name, _pos, quaternion);
            return false;
        }
        else
        {
            return false;
        }


    }

    AttitudeToFire.AttitudeFire CanAttitudeFireBlock(Vector3 _pos)
    {
        var block = Physics2D.OverlapPoint(_pos, StopLayer);
        if (!block)
        {
            return AttitudeToFire.AttitudeFire.Skips;
        }
        else
        {
            var attitudeToFire = block.GetComponent<AttitudeToFire>();
            if (attitudeToFire != null)
            {
                //switch (attitudeToFire.StatusAttitudeToFire)
                //{
                //    case AttitudeToFire.AttitudeFire.Absorb:
                //        return true;
                //    case AttitudeToFire.AttitudeFire.Stops:
                //        return false;
                //}
                return attitudeToFire.StatusAttitudeToFire;
            }
            else
            {
                return AttitudeToFire.AttitudeFire.Skips; 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _radius);
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

