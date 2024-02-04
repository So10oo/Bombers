using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Yandex", menuName = "Yandex")]
public class Yandex : ScriptableObject
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();


    public void ShowAdvertisement()
    {
#if UNITY_EDITOR
        Debug.Log("Unity Editor");
#elif UNITY_WEBGL
    ShowAdv();
#endif
    }


}
