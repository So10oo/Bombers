using System.IO;
using UnityEngine;

public class Vector2IntExtension 
{
    public static object Deserialize(byte[] data)
    {
        var result = new Vector2Int();
         
        return result;
    }

    public static byte[] Serialize(object customType)
    {
        var c = (Vector2Int)customType;

        return new byte[] {  };
    }
}
