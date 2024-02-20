using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPlayer : MonoBehaviour
{
    [Header("Spawn Parametrs")]

    [SerializeField] GameObject _player;
    [SerializeField] Transform[] _points;

    [Header("Events")]
    [SerializeField] UnityEvent _onSpawn;

    public void Spawn()
    {
        if (_points == null && _points.Length != 4)
        {
            Debug.LogError("ћассив поинтов имеет неверный формат");
            return ;
        }

        for (int index = 0; index < PhotonNetwork.PlayerList.Length; index++)
        {
            if (PhotonNetwork.PlayerList[index].IsLocal)/*.LocalPlayer.IsLocal*/
            {
                PhotonNetwork.LocalPlayer.TagObject ??= new TagPlayerInfo();
                var tagPlayerInfo = PhotonNetwork.LocalPlayer.TagObject as TagPlayerInfo;
                if (tagPlayerInfo.PlayerGameObject != null)
                    PhotonNetwork.Destroy(tagPlayerInfo.PlayerGameObject);

                var player = PhotonNetwork.Instantiate(_player.name, _points[index].position, Quaternion.identity);

                tagPlayerInfo.PlayerGameObject = player;
                _onSpawn?.Invoke();
            }
        }
       
    }
     

}
