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

            HostData[] data = MasterServer.PollHostList();

            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if(hostList != null)
            {
                
                foreach (var element in data)
                {                            
                    GUILayout.BeginArea(new Rect(400,100,300,50));
                    GUILayout.BeginHorizontal();
                    var name = element.gameName + " " + element.connectedPlayers + " / " + element.playerLimit;
                    GUILayout.Label(name);
                    GUILayout.Space(5);
                    string hostInfo;
                    hostInfo = "[";
                    foreach (var host in element.ip)
                        hostInfo = hostInfo + host + ":" + element.port + " ";
                    hostInfo = hostInfo + "]";
                    GUILayout.Label(hostInfo);
                    GUILayout.Space(5);
                    GUILayout.Label(element.comment);
                    GUILayout.EndHorizontal();
                    if (GUILayout.Button("Connect"))
                    {
                        // Connect to HostData struct, internally the correct method is used (GUID when using NAT).
                        Network.Connect(element);
                    }
                    GUILayout.EndArea();
                }
            }
            
            // Go through all the hosts in the host list
            
        }
    }

    private void RefreshHostList()
    {
        Debug.Log("Retreving Host List");
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
