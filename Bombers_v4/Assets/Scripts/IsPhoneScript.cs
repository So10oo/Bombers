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
        Hello();
        var a = IsPhone();
        Debug.Log(a);
        gameObject.SetActive(a);
        //gameObject.SetActive(IsPhone());
    }
}
