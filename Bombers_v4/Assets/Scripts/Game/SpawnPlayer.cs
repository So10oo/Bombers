using Photon.Pun;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [Header("Spawn Parametrs")]

    [SerializeField] GameObject _player;
    [SerializeField] Transform[] _points;

    //GameObject playerGO;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (_points == null && _points.Length != 4)
        {
            Debug.Log("ћассив поинтов имеет неверный формат");
            return;
        }

        for (int index = 0; index < PhotonNetwork.PlayerList.Length; index++)
        {
            if (PhotonNetwork.PlayerList[index] == PhotonNetwork.LocalPlayer)
            {
                PhotonNetwork.Instantiate(_player.name, _points[index].position, Quaternion.identity);
            }
         
        }
    }


}
