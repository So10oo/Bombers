using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class CreateRoomMap : MonoBehaviourPunCallbacks
{
    [SerializeField] Map[] maps;

    static string MAP_ROOM = "MapRoom";

    private void Start()
    {
        ChoosingIndexMap();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(MAP_ROOM))
        {
            DelatyCurrentMap();
            int index = (int)propertiesThatChanged[MAP_ROOM];
            CreateMap(maps[index]);
        }
    }

    public void ChoosingIndexMap()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        var indexMap = Random.Range(0, maps.Length);
        var prop = new Hashtable() { { MAP_ROOM, indexMap } };
        PhotonNetwork.CurrentRoom.SetCustomProperties(prop);
    }

    void CreateMap(Map map)
    {
        foreach (var blocks in map.Blocks)
        {
            var block = blocks.Block;
            var isNetworkComponent = block.GetComponent<PhotonView>() is PhotonView;
            foreach (var pos in blocks.Positions)
            {
                GameObject go;
                if (isNetworkComponent && PhotonNetwork.IsMasterClient)
                    go = PhotonNetwork.InstantiateRoomObject(block.name, pos, Quaternion.identity);
                else
                    go = Instantiate(block, pos, Quaternion.identity);
                go.transform.SetParent(transform);
            }
        }
    }

    void DelatyCurrentMap()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();

        foreach (var item in transform.GetChilds())
        {
            var isNetworkComponent = item.GetComponent<PhotonView>() is PhotonView;
            if (isNetworkComponent && PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(item.gameObject);
            else
                Destroy(item.gameObject);
        }
    }
}
