using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nickName;
    [SerializeField] Image _imageBackgraund;
    [SerializeField] Image _imageSkin;
    [SerializeField] Image _imageReady;

    public void Initialization(Player player,Color color , bool isReady = false)
    {
        _nickName.text = player.NickName;
        _imageBackgraund.color = color;
        _imageSkin.color = color;
        _imageReady.color = isReady ? Color.green : Color.red;
    }

   
}
