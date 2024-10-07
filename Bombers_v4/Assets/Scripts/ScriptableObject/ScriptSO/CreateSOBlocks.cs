using UnityEditor;
using UnityEngine;

public class CreateSOBlocks : MonoBehaviour
{
    [SerializeField] GameObject _wood;
    [SerializeField] GameObject _stone;

#if UNITY_EDITOR
    [ContextMenu("CreateSOMap")]
    void CreateSOMap()
    {
        var map = ScriptableObject.CreateInstance<Map>();
        var WoodBlocks = new BlocksPosition(); 
        WoodBlocks.Block = _wood;
        var StoneBlocks = new BlocksPosition();
        StoneBlocks.Block = _stone;
        map.Blocks.Add(WoodBlocks);
        map.Blocks.Add(StoneBlocks);
        foreach (Transform child in transform.GetChilds())
        {
            if (child.gameObject.GetComponent<Wood>() is Wood)
            {
                WoodBlocks.Positions.Add(child.position);
                continue;
            }
            if (child.gameObject.GetComponent<Stone>() is Stone)
            {
                StoneBlocks.Positions.Add(child.position);
                continue;
            }
        }
        AssetDatabase.CreateAsset(map, "Assets/" + gameObject.name + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = map;

    }
#endif
}

 