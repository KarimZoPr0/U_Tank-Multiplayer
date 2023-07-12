using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyItem : MonoBehaviour
{
    [SerializeField] private Button joinButton;
    
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private TMP_Text lobbyPlayersText;

    private LobbiesList _lobbiesList;
    private Lobby _lobby;
    
    public void Initialize(LobbiesList lobbiesList, Lobby lobby)
    {
        _lobbiesList = lobbiesList;
        _lobby = lobby;
        
        lobbyNameText.text = lobby.Name;
        lobbyPlayersText.text = $"{lobby.Players.Count}/{lobby.MaxPlayers}";
    }

    private void Start() => joinButton.onClick.AddListener(Join);
    private void OnDestroy() => joinButton.onClick.RemoveListener(Join);

    private void Join() => _lobbiesList.JoinAsync(_lobby);
}
