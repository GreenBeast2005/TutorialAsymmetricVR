using Unity.Netcode;
using UnityEngine;

public class RpcTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer && IsOwner) //Only send an RPC to the server from the client that owns the NetworkObject of this NetworkBehaviour instance
        {
            ServerOnlyServerRpc(0, NetworkObjectId);
        }
    }

    [ClientRpc]
    void ClientAndHostClientRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        if (IsOwner) //Only send an RPC to the owner of the NetworkObject
        {
            ServerOnlyServerRpc(value + 1, sourceNetworkObjectId);
        }
    }

    [ServerRpc]
    void ServerOnlyServerRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        ClientAndHostClientRpc(value, sourceNetworkObjectId);
    }
}