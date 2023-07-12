using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbiesList : MonoBehaviour
{
    [SerializeField] private Transform lobbyItemParent;
    [SerializeField] private LobbyItem lobbyItemPrefab;
    [SerializeField] private Button refreshButton;
    
    
    private bool _isJoining;
    private bool _isRefreshing;

    private void OnEnable() => RefreshList();

    private void Start() => refreshButton.onClick.AddListener(RefreshList);
    private void OnDestroy() => refreshButton.onClick.RemoveListener(RefreshList);

    private async void RefreshList()
    {
        if (_isRefreshing) return;
        _isRefreshing = true;

        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;

            options.Filters = new List<QueryFilter>()
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0"),
                new QueryFilter(
                    field: QueryFilter.FieldOptions.IsLocked,
                    op: QueryFilter.OpOptions.EQ,
                    value: "0")
            };

           QueryResponse lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);
           
           foreach (Transform child in lobbyItemParent)
           {
               Destroy(child.gameObject);
           }
           
           foreach (Lobby lobby in lobbies.Results)
           {
              LobbyItem lobbyItem = Instantiate(lobbyItemPrefab, lobbyItemParent);
              lobbyItem.Initialize(this, lobby);
           }

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
        
        
        _isRefreshing = false;
    }
    
    public async void JoinAsync(Lobby lobby)
    {
        if (_isJoining) return;
        _isJoining = true;
        try
        {
           var joiningLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
           string joinCode = joiningLobby.Data["JoinCode"].Value;

           await ClientSingleton.Instance.GameManager.StartClientAsync(joinCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

        _isJoining = false;
    }
}
