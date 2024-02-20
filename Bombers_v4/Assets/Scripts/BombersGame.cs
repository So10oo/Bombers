using Photon.Realtime;

public static class BombersGame
{
    public const byte MAX_PLAYERS_IN_ROOM = 4;//maximum number of players in a room

    public const byte PLAYER_MAX_LIVES = 3;

    public const string PLAYER_LIVES = "PlayerLives";

    public const string PLAYER_READY = "IsPlayerReady";

    public const string PLAYER = "Player";

    public const string PlayerColor = "PlayerColor";

    //public static Color GetColor(int colorChoice)
    //{
    //    switch (colorChoice)
    //    {
    //        case 0: return Color.red;
    //        case 1: return Color.green;
    //        case 2: return Color.blue;
    //        case 3: return Color.yellow;
    //        case 4: return Color.cyan;
    //        case 5: return Color.grey;
    //        case 6: return Color.magenta;
    //        case 7: return Color.white;
    //    }
    //    Debug.Log("Цвет под номером " + colorChoice + "\n");
    //    return Color.black;
    //}


    public static RoomOptions GetRoomOptions()
    {
        //return new RoomOptions { MaxPlayers = 4, PlayerTtl = 30_000, CleanupCacheOnLeave = false };
        return new RoomOptions { MaxPlayers = MAX_PLAYERS_IN_ROOM, PlayerTtl = 100, CleanupCacheOnLeave = false };
    }
}

