using UnityEngine;

public class RoundPosition : MonoBehaviour
{
#if UNITY_EDITOR
    [ContextMenu("RoundPositionBlocks")]
    void RoundPositionBlocks()
    {
        foreach (Transform child in transform.GetChilds()) 
        {
            child.position = Vector3Int.RoundToInt(child.position);
        }
    }
#endif
}
