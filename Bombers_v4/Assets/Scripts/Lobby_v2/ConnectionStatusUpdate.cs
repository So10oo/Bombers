using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ConnectionStatusUpdate : MonoBehaviourPunCallbacks
{
    private readonly string connectionStatusMessage = "   Connection Status: ";

    [Header("UI References")]
    public TextMeshProUGUI ConnectionStatusText;

    [Header("Events")]
    [SerializeField] UnityEvent _onConnectedToMaster;

    #region UNITY

    public void Update()
    {
        ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState.ToString(/*new CultureInfo("ru-RU")*/);
    }

    #endregion

     
    public override void OnConnectedToMaster()
    {
        _onConnectedToMaster?.Invoke();
    }
}


public static class ClientStateExtension
{
    public static string ToCastomString(this ClientState str)
    {

        switch (str)
        {
            case ClientState.PeerCreated:
                return "";
            case ClientState.Authenticating:
                return ""; 
            case ClientState.Authenticated:
                return "";
            case ClientState.JoiningLobby:
                return "";
            case ClientState.JoinedLobby:
                return "";
            case ClientState.DisconnectingFromMasterServer:
                return "";
            case ClientState.ConnectingToGameServer:
                return "";
            case ClientState.ConnectedToGameServer:
                return "";
            case ClientState.Joining:
                return "";
            case ClientState.Joined:
                return "";
            case ClientState.Leaving:
                return "";
            case ClientState.DisconnectingFromGameServer:
                return "";
            case ClientState.ConnectingToMasterServer:
                return "";
            case ClientState.Disconnecting:
                return "";
            case ClientState.Disconnected:
                return "";
            case ClientState.ConnectedToMasterServer:
                return "";
            case ClientState.ConnectingToNameServer:
                return "";
            case ClientState.ConnectedToNameServer:
                return "";
            case ClientState.DisconnectingFromNameServer:
                return "";
            case ClientState.ConnectWithFallbackProtocol:
                return "";
            case ClientState.ConnectWithoutAuthOnceWss:
                return "";
            default:
                return "";
        }
    }
}