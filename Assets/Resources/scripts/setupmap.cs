using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.Netcode;

public class setupmap : MonoBehaviour
{
    [SerializeField] private Button server;
    [SerializeField] private Button host;
    [SerializeField] private Button client;




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

    void setupbasebuilding(GameObject basebuilding, MeshCollider surfacemesh, Renderer surfacerenderer)
    {
        // adding basecontroller to basebuilding

        BaseController controllerscript = basebuilding.transform.AddComponent<BaseController>();
        controllerscript.meshcollider = surfacemesh;
        controllerscript.surfacerenderer = surfacerenderer;
        controllerscript.worldmap = this.gameObject;
        controllerscript.building = basebuilding.transform.gameObject;


        // adding spawnminion to basebuilding
        spawnminion spawnminionscript = basebuilding.transform.AddComponent<spawnminion>();
        spawnminionscript.spawntime = 5;
    }
}
