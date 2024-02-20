using Photon.Pun;
using TMPro;
using UnityEngine;

public class DisplayEntity : MonoBehaviour
{
    private TextMeshProUGUI _display;

    private void Awake()
    {
        _display = GetComponent<TextMeshProUGUI>();
    }

    private void Display<T>(T num)
    {
        _display.text = num.ToString();
    }

    public void DisplayPlayerInfo()
    {
        var tagPlayerInfo =  PhotonNetwork.LocalPlayer.TagObject as TagPlayerInfo;
        if (tagPlayerInfo == null) return;

        var playerManager = (tagPlayerInfo.PlayerGameObject).GetComponent<PlayerManager>();
        playerManager.CharacterTraits.OnCharacterTraitsChanged += this.Display;
        this.Display(playerManager.CharacterTraits);
    }
}
