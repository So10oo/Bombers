using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nickName;
    [SerializeField] Image _imageBackgraund;
    [SerializeField] Image _imageSkin;

    public void Initialization(Player player,Color color)
    {
        _nickName.text = player.NickName;
        _imageBackgraund.color = color;
        _imageSkin.color = color;
    }

   
}
