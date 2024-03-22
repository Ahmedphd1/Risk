using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class networkhandler : NetworkBehaviour
{
    [SerializeField] private Transform crab;
    private NetworkVariable<int> netvar = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<customdata> netcustomvar = new NetworkVariable<customdata>(
        new customdata
        {
            spawns = 1,
            iswalkalbe = true,
            message = "hello"
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct customdata : INetworkSerializable
    {
        public int spawns;
        public bool iswalkalbe;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            serializer.SerializeValue(ref spawns);
            serializer.SerializeValue(ref iswalkalbe);
            serializer.SerializeValue(ref message);
        }
    }

    [ServerRpc]
    private void ServerRpc() // use this if you want to send data from client to server
    {
        Debug.Log("Recieved from client");
    }

    [ClientRpc]
    private void ClientRpc() // use this if you want to send data from server to client
    {
        Debug.Log("Sent to server");
    }

    public override void OnNetworkSpawn()
    {
    print("setting up events");
    netcustomvar.OnValueChanged += (customdata prevval, customdata newval) =>
    {
        Debug.Log(+ newval.spawns + " " + newval.iswalkalbe + " " + newval.message);
    };
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            ServerRpc();
            ClientRpc();
            Debug.Log(netvar.Value);
            netvar.Value += 1;
            netcustomvar.Value = new customdata { spawns = Random.RandomRange(0,20), iswalkalbe = true, message = "hello" };
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Transform crabobj = Instantiate(crab);
            crabobj.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
