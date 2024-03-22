using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.Netcode;

public class setupmap : MonoBehaviour
{
    private GameObject map;
    [SerializeField] private Button server;
    [SerializeField] private Button host;
    [SerializeField] private Button client;




    private void Awake()
    {
        server.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            startmap();
        });

        host.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            startmap();
        });

        client.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }

    private void startmap()
    {
        map = this.transform.gameObject;
        map.layer = LayerMask.NameToLayer(layers.floor);
        setupbasesurface(map);
        setupnavmesh(map);
    }

    bool setupbasesurface(GameObject map)
    {
        if (map != null)
        {
            map.transform.GetChild(0).transform.GetChild(0).gameObject.tag = playertags.player1;
            map.transform.GetChild(1).transform.GetChild(0).gameObject.tag = playertags.player2;
            map.transform.GetChild(2).transform.GetChild(0).gameObject.tag = playertags.neutral;
            // setting up box collider for detection of minions
            for (int i = 0; i <= map.transform.childCount - 1; i++)
            {
                GameObject basesurface = map.transform.GetChild(i).gameObject;

                if (basesurface != null)
                {
                    basesurface.layer = LayerMask.NameToLayer(layers.floor);
                    basesurface.AddComponent<MeshCollider>();
                    GameObject baseobject = basesurface.transform.GetChild(0).gameObject;
                    setupbasebuilding(baseobject, basesurface.GetComponent<MeshCollider>(), basesurface.GetComponent<Renderer>());
                }
                else
                {
                }
            }
        } else
        {
            return false;
        }

        return true;
    }

    bool setupnavmesh(GameObject map)
    {
        // adding navigation components
        map.AddComponent<MeshCollider>();
        NavMeshSurface navmesh = map.AddComponent<NavMeshSurface>();
        navmesh.layerMask = LayerMask.GetMask(layers.floor);
        navmesh.BuildNavMesh();
        return true;
    }

    bool setupbasebuilding(GameObject basebuilding, MeshCollider surfacemesh, Renderer surfacerenderer)
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

        // setup navmesh obstacle
        NavMeshObstacle navmeshobstacle = basebuilding.AddComponent<NavMeshObstacle>();
        navmeshobstacle.shape = NavMeshObstacleShape.Capsule;
        navmeshobstacle.radius *= 1.5f;
        return true;
    }


}
