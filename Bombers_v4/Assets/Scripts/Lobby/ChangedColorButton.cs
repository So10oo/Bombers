using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ChangedColorButton : MonoBehaviour
{
    [SerializeField] Image _image;
    Color _color;
    Button _button;

    public Color Color { get => _color; }

    private void Awake()
    {
        _color = _image.color;
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(Click);
    }

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
            var prop = new ExitGames.Client.Photon.Hashtable() { { PhotonNetwork.LocalPlayer.ActorNumber.ToString() , strColor } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(prop);

        }
    }

}
