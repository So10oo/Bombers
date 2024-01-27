using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static RaiseEventEnum;

public class Wood : HealthPoint
{
    public override void Die()
    {
        Destroy(gameObject);

        if (!PhotonNetwork.IsMasterClient) return;

        if (Random.Range(0f, 1f) > 0.5f)
        {
            object[] content = new object[] { Random.value , transform.position };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)RaiseEvent.WodToBonus, content, raiseEventOptions, SendOptions.SendReliable);
        }

    }

}
