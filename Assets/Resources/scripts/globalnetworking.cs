using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class globalnetworking : MonoBehaviour
{
    // notes for networking



    [ServerRpc]
    private void ServerRpc()
    {
        // sends data from client to server
    }

    [ClientRpc]
    private void ClientRpc()
    {
        // sends data from server to all clients
    }

    [ServerRpc]
    private void ServerRpc(int number)
    {
        // sends data from client to server (with parameters)
        print(number);
    }

    [ClientRpc]
    private void ClientRpc(int number)
    {
        // sends data from server to all clients (with parameters)
        print(number);
    }

    [ServerRpc]
    private void ServerRpc(ServerRpcParams param = default)
    {
        // sends data from client to server (with sender client id)
        ulong senderid = param.Receive.SenderClientId;
        print(senderid);
    }

    [ClientRpc]
    private void ClientRpc(ServerRpcParams param = default)
    {
        // sends data from server to specific client

        ClientRpcParams clientRpcParams = new ClientRpcParams();

        clientRpcParams.Send.TargetClientIds = new List<ulong>()
        {
            param.Receive.SenderClientId
        };
    }
}
