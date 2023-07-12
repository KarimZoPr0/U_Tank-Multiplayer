using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TMP_InputField joinCodeField;
    

    private void Start()
    {
        hostButton.onClick.AddListener(StartHost);
        clientButton.onClick.AddListener(StartClient);
    }

    private void OnDestroy()
    {
        hostButton.onClick.RemoveListener(StartHost);
        clientButton.onClick.RemoveListener(StartClient);
    }

    private async void StartHost() => await HostSingleton.Instance.GameManager.StartHostAsync();
    private async void StartClient() => await ClientSingleton.Instance.GameManager.StartClientAsync(joinCodeField.text);
}
