using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class networkmanagerui : MonoBehaviour
{
    [SerializeField] private Button server;
    [SerializeField] private Button host;
    [SerializeField] private Button client;

    // Update is called once per frame

    private void Awake()
    {
        server.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        host.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        client.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
