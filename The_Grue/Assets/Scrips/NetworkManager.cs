using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    private const string typeName = "UniqueGameName";
    private const string gameName = "RoomName";
    private HostData[] hostList;

    public GameObject player;
    public GameObject torch;

    private void StartServer()
    {
        
        Network.InitializeServer(2,25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
        SpawnPlayer();
        SpawnTorch();
    }

    private void SpawnTorch()
    {
        Network.Instantiate(torch, new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if(hostList != null)
            {
                for(int i = 0 ; 1 < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);

                }
            }
        }
    }

    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }

    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Network.Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity, 0);

    }

}
