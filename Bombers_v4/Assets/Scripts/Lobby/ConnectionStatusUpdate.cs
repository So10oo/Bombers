using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ConnectionStatusUpdate : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    public TextMeshProUGUI ConnectionStatusText;
            
    [Header("Events")]
    [SerializeField] UnityEvent _onConnectedToMaster;

    #region UNITY

    public void Update()
    {
        ConnectionStatusText.text = PhotonNetwork.NetworkClientState.ToCastomString();
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
                return "Создание узла";
            case ClientState.Authenticating:
                return "Аутентификация"; 
            case ClientState.Authenticated:
                return "Проверка подлинности";
            case ClientState.JoiningLobby:
                return "Присоединение к лобби";
            case ClientState.JoinedLobby:
                return "Присоединён к лобби";
            case ClientState.DisconnectingFromMasterServer:
                return "Отключение от главного сервера";
            case ClientState.ConnectingToGameServer:
                return "Подключение к игровому серверу";
            case ClientState.ConnectedToGameServer:
                return "Подключен к игровому серверу";
            case ClientState.Joining:
                return "Присоединение";
            case ClientState.Joined:
                return "Присоединился";
            case ClientState.Leaving:
                return "Покидает";
            case ClientState.DisconnectingFromGameServer:
                return "";
            case ClientState.ConnectingToMasterServer:
                return "Отключение от главного сервера";
            case ClientState.Disconnecting:
                return "Отключение";
            case ClientState.Disconnected:
                return "Отключен";
            case ClientState.ConnectedToMasterServer:
                return "Подключен к главному серверу";
            case ClientState.ConnectingToNameServer:
                return "Подключение к серверу имен";
            case ClientState.ConnectedToNameServer:
                return "Подключен к серверу имен";
            case ClientState.DisconnectingFromNameServer:
                return "Отколючение от сервера имен";
            case ClientState.ConnectWithFallbackProtocol:
                return "Подключение по резервному протоколу";
            case ClientState.ConnectWithoutAuthOnceWss:
                return "Подключайтесь без авторизации AuthOnceWss";
            default:
                return "";
        }
    }
}