using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color—ontainer : MonoBehaviourPunCallbacks
{
    static Dictionary<string,GameObject> buttonsColor = new();

    void Awake()
    {
        foreach (var tr in transform.GetChilds()) 
        { 
            string strColor = tr.gameObject.GetComponent<Image>().color.ToString();
            buttonsColor.Add(strColor, tr.gameObject);
        }

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        var propCurrentRoom = PhotonNetwork.CurrentRoom.CustomProperties;
        propCurrentRoom[otherPlayer.ActorNumber.ToString()] = string.Empty;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propCurrentRoom);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        OnChangedViewButtons(propertiesThatChanged);
    }

    private void OnChangedViewButtons(ExitGames.Client.Photon.Hashtable properties)
    {
        foreach (var buttonColor in buttonsColor)
        {
            if (properties.ContainsValue(buttonColor.Value.GetComponent<Image>().color.ToString()))
            {
                buttonColor.Value.SetActive(false);
            }
            else
            {
                buttonColor.Value.SetActive(true);
            }

        }
    }

    public static void GetFree—olor()
    {
        var prop = PhotonNetwork.CurrentRoom.CustomProperties;
        foreach (var buttonColor in buttonsColor)
        {
            if (prop.ContainsValue(buttonColor.Key))
            {
                continue;
            }
            else
            {
                prop[PhotonNetwork.LocalPlayer.ActorNumber.ToString()] = buttonColor.Key;
                PhotonNetwork.CurrentRoom.SetCustomProperties(prop);
                return;
            }
        }
    }

    public static Color GetColorPlayer(Player player)
    {
        Color color = Color.black;
        var prop = PhotonNetwork.CurrentRoom.CustomProperties;
        object keyColor;
        if (prop.TryGetValue(player.ActorNumber.ToString(), out keyColor) && buttonsColor.ContainsKey((string)keyColor))
        {
            color = buttonsColor[(string)keyColor].GetComponent<Image>().color;
        }
        return color;
    }

}


public static class TransformExtation
{
    public static Transform[] GetChilds(this Transform parent)
    {
        List<Transform> ret = new List<Transform>();
        foreach (Transform child in parent) ret.Add(child);
        return ret.ToArray();
    }
}