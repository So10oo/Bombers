using TMPro;
using UnityEngine;

public class DisplayEntity : MonoBehaviour
{  
    private TextMeshProUGUI _display;

    private void Awake()
    {
        _display = GetComponent<TextMeshProUGUI>();
    }

    public void Display<T>(T num)
    {
        _display.text = num.ToString();
    }
}
