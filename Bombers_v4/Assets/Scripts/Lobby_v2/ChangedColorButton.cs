using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ChangedColorButton : MonoBehaviour
{
    Color _color;
    Button _button;

    private void Awake()
    {
        _color = GetComponent<Image>().color;
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(Click);
    }

    //private void action()
    //{
    //    var probCurrentRoom = PhotonNetwork.CurrentRoom.CustomProperties;
    //    string strColor = _color.ToString();
    //    if (probCurrentRoom.ContainsKey(strColor)) 
    //    {
    //        //доступен ли цвет? 
    //        if ((bool)probCurrentRoom[strColor])
    //        {
    //            //освобождаем свой цвет 
    //            var probLocalPlayer = PhotonNetwork.LocalPlayer.CustomProperties;
    //            object localColor;
    //            if (probLocalPlayer.TryGetValue(BombersGame.PlayerColor,out localColor))
    //            {
    //                var keyColor = (string)localColor;
    //                probCurrentRoom[keyColor] = true;
    //            } 

    //            probCurrentRoom[strColor] = false;
    //            probLocalPlayer[BombersGame.PlayerColor] = strColor;

    //            PhotonNetwork.CurrentRoom.SetCustomProperties(probCurrentRoom);
    //            PhotonNetwork.LocalPlayer.SetCustomProperties(probLocalPlayer);
    //        }
    //    }

    //}

    private void Click()
    {
        var probCurrentRoom = PhotonNetwork.CurrentRoom.CustomProperties;
        string strColor = _color.ToString();

        if (probCurrentRoom.ContainsValue(strColor))
        {
            //цвет занят
        }
        else 
        {
            probCurrentRoom[PhotonNetwork.LocalPlayer.ActorNumber.ToString()] = strColor;
            PhotonNetwork.CurrentRoom.SetCustomProperties(probCurrentRoom);
        }

    }




}
