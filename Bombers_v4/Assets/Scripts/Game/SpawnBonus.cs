using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using static RaiseEventEnum;


public class SpawnBonus : MonoBehaviour, IOnEventCallback
{

    [SerializeField] List<GameObject> _bonuses;

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == (byte)RaiseEvent.WodToBonus)
        {
            object[] data = (object[])photonEvent.CustomData;
            var index = Mathf.FloorToInt((float)data[0] * _bonuses.Count);
            if (index >= _bonuses.Count) index = _bonuses.Count - 1;

            Vector3 position = (Vector3)data[1];
            Instantiate(_bonuses[index], position, Quaternion.identity);
        }
    }
}
