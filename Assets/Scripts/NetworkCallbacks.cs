using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{

    public static List<IPvpPlayerState> playerStates = new List<IPvpPlayerState>();

    public override void SceneLoadLocalDone(string map)
    {
        // randomize a position
        var spawnPosition = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));

        // instantiate cube
        BoltEntity playerPrefab = BoltNetwork.Instantiate(BoltPrefabs.PlayerPrefab, spawnPosition, Quaternion.identity);
        IPvpPlayerState playerState = playerPrefab.GetComponent<ChrControllerBolt>().state;
        playerStates.Add(playerState);
    }
}