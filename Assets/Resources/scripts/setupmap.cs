using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections.Generic;
using System.Linq;
using System;

public class setupmap : NetworkBehaviour
{
    [SerializeField] private Button server;
    [SerializeField] private Button host;
    [SerializeField] private Button client;
    [SerializeField] private Button start;
    List<string> bases = new List<string>()
    {
        "denmark",
        "norway"
    };



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

        start.onClick.AddListener(() =>
        {
            if (IsServer)
            {
                Dictionary<GameObject, ulong> bases = getbases();

                if (bases != null)
                {
                    // assign bases ownership

                    if (assignbases(bases))
                    {
                        // request start to client
                        RequestInitClientRpc();
                    }
                }
            }
        });
    }

    [ClientRpc]
    private void RequestInitClientRpc()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<BaseController>().init = true;
        }
    }

    bool assignbases(Dictionary<GameObject, ulong> bases)
    {
        try
        {

            foreach (var kvp in bases)
            {
                if (kvp.Value != 0)
                {
                    GameObject basesurface = kvp.Key;
                    GameObject basebuilding = basesurface.transform.GetChild(0).gameObject;
                    GameObject basecurcle = basebuilding.transform.GetChild(0).gameObject;

                    basesurface.GetComponent<NetworkObject>().ChangeOwnership(kvp.Value);
                    basebuilding.GetComponent<NetworkObject>().ChangeOwnership(kvp.Value);
                    basecurcle.GetComponent<NetworkObject>().ChangeOwnership(kvp.Value);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            print("Cannot assign bases. something went wrong : ->");
            print(ex.Message);
            Application.Quit();
            return false;
        }
    }

    Dictionary<GameObject, ulong> getbases()
    {
        try
        {
            IReadOnlyList<ulong> clientids = NetworkManager.Singleton.ConnectedClientsIds;

            Dictionary<GameObject, ulong> zipped = bases.Zip(clientids.Concat(Enumerable.Repeat(playertags.serverid, bases.Count - clientids.Count)), (baseName, clientId) => new { BaseName = baseName, ClientId = clientId }).ToDictionary(obj => GameObject.Find(obj.BaseName).gameObject, obj => obj.ClientId);
            return zipped;
        } catch (Exception ex)
        {
            print("Cannot get bases. something went wrong : ->");
            print(ex.Message);
            Application.Quit();
            return new Dictionary<GameObject, ulong>();
        }
    }
}
