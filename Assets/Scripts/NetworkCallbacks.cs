using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class NetworkCallbacks : Bolt.GlobalEventListener
{    
    public static List<IPvpPlayerState> playerStates = new List<IPvpPlayerState>();

    void Awake()
    {
        PlayerObjectRegistry.CreateServerPlayer();
    }
    
    public override void Connected(BoltConnection connection)
    {      
        PlayerObjectRegistry.CreateClientPlayer(connection);  
        // Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));

        // BoltEntity playerPrefab = BoltNetwork.Instantiate(BoltPrefabs.PlayerPrefab, spawnPosition, Quaternion.identity);
        // playerPrefab.AssignControl(connection);
        // IPvpPlayerState playerState = playerPrefab.GetComponent<ChrControllerBolt>().state;
        // playerStates.Add(playerState);
    }

    public override void SceneLoadLocalDone(string map)
    {
        PlayerObjectRegistry.ServerPlayer.Spawn();
    }
    
    // public override void SceneLoadLocalDone(string map)
    // {
    //     Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));

    //     BoltEntity playerPrefab = BoltNetwork.Instantiate(BoltPrefabs.PlayerPrefab, spawnPosition, Quaternion.identity);
    //     playerPrefab.TakeControl();
    //     IPvpPlayerState playerState = playerPrefab.GetComponent<ChrControllerBolt>().state;
    //     playerStates.Add(playerState);
    // }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        PlayerObjectRegistry.GetPlayer(connection).Spawn();
    }
}