using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionButtons : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;

    private void Start()
    {
        hostButton.onClick.AddListener(OnHost);
        joinButton.onClick.AddListener(OnJoin);
    }

    private void OnDestroy()
    {
        hostButton.onClick.RemoveListener(OnHost);
        joinButton.onClick.RemoveListener(OnJoin);
    }

    private void OnHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    private void OnJoin()
    {
        NetworkManager.Singleton.StartClient();
    }
}
