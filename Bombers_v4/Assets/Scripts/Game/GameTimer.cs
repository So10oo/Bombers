using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [HideInInspector] public float Timer;

    [SerializeField] TextMeshProUGUI m_TextMeshPro;

    void Start()
    {
        Timer = 0;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        m_TextMeshPro.text = this.ToString();
    }


    public override string ToString()
    {
        int min = (int)Timer / 60;
        int sec = (int)Timer % 60;
        int micsec = (int)(Timer % 1 * 100);

        var minStr = min.ToString();
        var secStr = sec.ToString();
        var micsecStr = micsec.ToString();

        return minStr + ":" +
            (secStr.Length == 1 ? "0" : "") + secStr + ":" +
            (micsecStr.Length == 1 ? "0" : "") + micsecStr;
    }
}
