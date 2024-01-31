using System.Runtime.InteropServices;
using UnityEngine;

public class IsPhoneScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsPhone();

    [DllImport("__Internal")]
    private static extern void Hello();

    private void Start()
    {
        var isPhone = IsPhone();
        gameObject.SetActive(isPhone);
    }
}
