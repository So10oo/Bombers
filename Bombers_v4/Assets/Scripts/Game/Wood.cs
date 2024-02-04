using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Wood : HealthPoint
{
    [SerializeField] List<GameObject> _bonuses;

    public override void Die()
    {
        gameObject.SetActive(false);
        if (!PhotonNetwork.IsMasterClient) return;

        PhotonNetwork.Destroy(this.gameObject);

        if (Random.Range(0f, 1f) > 0.5f)
        {
            var bonus = _bonuses[Random.Range(0,_bonuses.Count)];
            PhotonNetwork.InstantiateRoomObject(bonus.name, transform.position, Quaternion.identity);
            //object[] content = new object[] { Random.value, transform.position };
            //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            //PhotonNetwork.RaiseEvent((byte)RaiseEvent.WodToBonus, content, raiseEventOptions, SendOptions.SendReliable);

        }

    }

}
