using System;using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkServer : IDisposable
{
    private NetworkManager _networkManager;

    private Dictionary<ulong, string> _clientIdToAuth = new();
    private Dictionary<string, UserData> _authIdToUserData = new();
    
    public NetworkServer(NetworkManager networkManager)
    {
        _networkManager = networkManager;
        _networkManager.ConnectionApprovalCallback += ApprovalCheck;
        _networkManager.OnServerStarted += OnNetworkReady;
    }
    
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        string payload = System.Text.Encoding.UTF8.GetString(request.Payload);
        UserData userData = JsonUtility.FromJson<UserData>(payload);

        _clientIdToAuth[request.ClientNetworkId] = userData.userAuthId;
        _authIdToUserData[userData.userAuthId] = userData;
        
        response.Approved = true;
        response.Position = SpawnPoint.GetRandomSpawnPos();
        response.Rotation = Quaternion.identity;
        response.CreatePlayerObject = true;
    }
    private void OnNetworkReady() => _networkManager.OnClientDisconnectCallback += OnClientDisconnect;
    

    private void OnClientDisconnect(ulong clientId)
    {
        if (!_clientIdToAuth.TryGetValue(clientId, out string authId)) return;
        _clientIdToAuth.Remove(clientId);
        _authIdToUserData.Remove(authId);
    }
    
    public UserData GetUserDataByClientId(ulong clientId)
    {
        if (!_clientIdToAuth.TryGetValue(clientId, out string authId)) return null;
        return _authIdToUserData.TryGetValue(authId, out UserData data) ? data : null;
    }

    public void Dispose()
    {
        if (_networkManager == null) return;
        _networkManager.ConnectionApprovalCallback -= ApprovalCheck;
        _networkManager.OnServerStarted -= OnNetworkReady;
        _networkManager.OnClientDisconnectCallback -= OnClientDisconnect;

        if (_networkManager.IsListening)
        {
            _networkManager.Shutdown();
        }
    }
}
