using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class CastomNetworManager : NetworkManager
{
    public List<Racket> Players = new List<Racket>();

    #region SingleTon
    public static CastomNetworManager Instance { get; private set; }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void InstantiateBall() 
    {
        GameObject ball = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(ball);
    }

    [System.Obsolete]
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerid) 
    {

        var currentPlayerCount = NetworkServer.connections.Count;
        if (currentPlayerCount <= startPositions.Count)
        {
            if (currentPlayerCount == 2)
                InstantiateBall();
            GameObject player = Instantiate(playerPrefab, startPositions[currentPlayerCount - 1].position, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerid);
            Players.Add(player.GetComponent<Racket>());
        }
        else 
        {
            conn.Disconnect();
        }
    }
}
 