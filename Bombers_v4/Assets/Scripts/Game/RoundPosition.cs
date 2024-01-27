using UnityEngine;

public class RoundPosition : MonoBehaviour
{
    [ContextMenu("RoundPositionBlocks")]
    void RoundPositionBlocks()
    {
        var children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children) 
        {
            child.position = Vector3Int.RoundToInt(child.position);
        }
    }

}
