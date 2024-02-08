using ExitGames.Client.Photon;
using System.IO;

public class ColorInfoServer
{
    public int ActorNumber { get; set; }
    //public string StringColor { get; set; }

    static ColorInfoServer()
    {
        PhotonPeer.RegisterType(typeof(ColorInfoServer), 255, ColorInfoServer.Serialize, ColorInfoServer.Deserialize);
    }

    public static bool operator !(ColorInfoServer value)
    {
        return value.ActorNumber >= 1;
    }

    public static bool operator true(ColorInfoServer value)
    {
        return value.ActorNumber < 1;
    }
    public static bool operator false(ColorInfoServer value)
    {
        return value.ActorNumber >= 1;
    }

    public static byte[] Serialize(object data)
    {
        var plauyeInfoServer = (ColorInfoServer)data;
        using (MemoryStream m = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(m))
            {
                writer.Write(plauyeInfoServer.ActorNumber);
               // writer.Write(plauyeInfoServer.StringColor);
            }
            return m.ToArray();
        }
    }

    public static object Deserialize(byte[] data)
    {
        ColorInfoServer result = new ColorInfoServer();
        using (MemoryStream m = new MemoryStream(data))
        {
            using (BinaryReader reader = new BinaryReader(m))
            {
                result.ActorNumber = reader.ReadInt32();
                //result.StringColor = reader.ReadString();
            }
        }
        return result;
    }
}
