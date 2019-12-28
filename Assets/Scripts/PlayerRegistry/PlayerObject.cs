using UnityEngine;

public class PlayerObject
{
    public BoltEntity character;
    public BoltConnection connection;

    public bool IsServer
    {
        get { return connection == null; }
    }

    public bool IsClient
    {
        get { return connection != null; }
    }

    public void Spawn()
    {
        if (!character)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));

            character = BoltNetwork.Instantiate(BoltPrefabs.PlayerPrefab, spawnPosition, Quaternion.identity);

            if (IsServer)
            {
                character.TakeControl();
            }
            else
            {
                character.AssignControl(connection);
            }
        }
    }
}