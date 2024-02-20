using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class Color—ontainer : MonoBehaviourPunCallbacks
{
    static Dictionary<string, ChangedColorButton> buttonsColor;

    static Color—ontainer instante;
    //private void OnEnable()
    //{
    //    base.OnEnable();
    //    buttonsColor.Clear();
    //}

    public override void OnDisable()
    {
        base.OnDisable();
        //buttonsColor.Clear();
    }

    private void Awake()
    {
        instante = this;
    }

    void Start()
    {
        Refresh();
    }

    static void Refresh()
    {
        buttonsColor = new();
        foreach (var tr in instante.gameObject.transform.GetChilds())
        {
            var changedColorButton = tr.gameObject.GetComponent<ChangedColorButton>();
            string strColor = changedColorButton.Color.ToString();
            buttonsColor.Add(strColor, changedColorButton);
        }
    }

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    if (!PhotonNetwork.IsMasterClient) return;

    //    Debug.Log("OnPlayerLeftRoom" + PhotonNetwork.CurrentRoom.ToStringFull());

    //    var propCurrentRoom = new ExitGames.Client.Photon.Hashtable();
    //    propCurrentRoom[otherPlayer.ActorNumber.ToString()] = string.Empty;
    //    PhotonNetwork.CurrentRoom.SetCustomProperties(propCurrentRoom);
    //}

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        OnChangedViewButtons(PhotonNetwork.CurrentRoom.CustomProperties);
    }

    private void OnChangedViewButtons(ExitGames.Client.Photon.Hashtable properties)
    {
        if(buttonsColor == null)
            Refresh();

        foreach (var buttonColor in buttonsColor)
        {
            var str = buttonColor.Value.Color.ToString();
            if (properties.ContainsValue(str))
            {
                buttonColor.Value.gameObject.SetActive(false);
            }
            else
            {
                buttonColor.Value.gameObject.SetActive(true);
            }
            
        }
    }

    public static void GetFree—olor()
    {
        if (buttonsColor == null)
            Refresh();

        var prop = PhotonNetwork.CurrentRoom.CustomProperties;

        foreach (var buttonColor in buttonsColor)
        {
            if (prop.ContainsValue(buttonColor.Key))
            {
                continue;
            }
            else
            {
                var a = new ExitGames.Client.Photon.Hashtable() { { PhotonNetwork.LocalPlayer.ActorNumber.ToString(), buttonColor.Key } };
                PhotonNetwork.CurrentRoom.SetCustomProperties(a);
                return;
            }
        }
    }

    public static Color GetColorPlayer(Player player)
    {
        if (buttonsColor == null)
            Refresh();

        Color color = Color.black;
        var prop = PhotonNetwork.CurrentRoom.CustomProperties;

        //Debug.Log("GetColorPlayer PhotonNetwork.CurrentRoom.CustomProperties " + PhotonNetwork.CurrentRoom.CustomProperties.ToStringFull());
        object keyColor;
        if (prop.TryGetValue(player.ActorNumber.ToString(), out keyColor) && buttonsColor.ContainsKey((string)keyColor))
        {
            color = buttonsColor[(string)keyColor].Color;
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


