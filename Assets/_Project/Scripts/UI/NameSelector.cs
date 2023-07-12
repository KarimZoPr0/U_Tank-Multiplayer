using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameSelector : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private Button connectButton;
    [SerializeField] private int minNameLength = 1;
    [SerializeField] private int maxNameLength = 12;

    public const string PlayerNameKey = "PlayerName";
    
    private void Start()
    {
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        
        nameField.text = PlayerPrefs.GetString(PlayerNameKey, string.Empty);
        HandleNameChanged(nameField.text);
        
        nameField.onValueChanged.AddListener(HandleNameChanged);
        connectButton.onClick.AddListener(Connect);
    }

    private void OnDestroy()
    {
        nameField.onValueChanged.RemoveListener(HandleNameChanged);
        connectButton.onClick.RemoveListener(Connect);
    }

    private void HandleNameChanged(string nameText) => connectButton.interactable = nameText.Length >= minNameLength && nameText.Length <= maxNameLength;

    private void Connect()
    {
        PlayerPrefs.SetString(PlayerNameKey, nameField.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
