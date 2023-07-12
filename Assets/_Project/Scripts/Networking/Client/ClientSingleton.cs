using System;
using System.Threading.Tasks;
using UnityEngine;

public class ClientSingleton : MonoBehaviour
{
    private static ClientSingleton _instance;
    public ClientGameManager GameManager { get; private set; }


    private void OnTriggerStay(Collider other)
    {
        int myInt = 10;
        string myString = "Test";
        
        float number = 1.954f;
        Console.WriteLine("Formatted number: {0:2}", number);

    }

    public static ClientSingleton Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<ClientSingleton>();
            if (_instance == null)
            {
                Debug.LogError("No ClientSingleton in the scene!");
                return null;
            }
            return _instance;
        }
        
    }
    private void Start() => DontDestroyOnLoad(gameObject);

    public async Task<bool> CreateClient()
    {
        GameManager = new ClientGameManager();
        return await GameManager.InitAsync();
    }

    private void OnDestroy() => GameManager?.Dispose();
}
