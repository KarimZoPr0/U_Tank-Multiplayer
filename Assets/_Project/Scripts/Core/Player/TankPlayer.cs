using System;
using Cinemachine;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
public class TankPlayer : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [field: SerializeField] public Health Health { get; private set; }
    

    [Header("Settings")]
    [SerializeField] private int ownerPriority = 15;
    

    public NetworkVariable<FixedString32Bytes> playerName = new();

    public static event Action<TankPlayer> OnPLayerSpawned;
    public static event Action<TankPlayer> OnPlayerDespawned;
    
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
           UserData userData = HostSingleton. Instance.GameManager.NetworkServer.GetUserDataByClientId(OwnerClientId);
           playerName.Value = userData.username;
           
           OnPLayerSpawned?.Invoke(this);
        }
        if (IsOwner)
        {
            virtualCamera.Priority = ownerPriority;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            OnPlayerDespawned?.Invoke(this);
        }
    }
}
