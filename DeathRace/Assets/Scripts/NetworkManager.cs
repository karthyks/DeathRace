using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

    public string regGameName;
    string GameType = "FFA";

    HostData[] hostData;
    float refreshRequestLength = 3.0f;

    int PlayerID = 0;

    ScriptHandler scriptHandler;
    UIHandler uiHandler;

    GameObject InputName;

    void Awake()
    {
        MasterServer.ipAddress = "127.0.0.1";
        MasterServer.port = 23466;
        scriptHandler = GameObject.Find("ScriptHandler").GetComponent<ScriptHandler>();
        uiHandler = GameObject.Find("UIHandler").GetComponent<UIHandler>();
        InputName = (GameObject)Resources.Load("Prefabs/Input");
    }

    void StartServer()
    {
        GameObject input = (GameObject)Instantiate(InputName, Vector3.zero, Quaternion.identity);
        input.name = "Input";
        input.transform.parent = GameObject.Find("ServerList").transform;
        input.transform.localScale = new Vector3(1, 1, 1);
        input.transform.localPosition = new Vector3(-300, 130, 0);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Hosted");
        MasterServer.RegisterHost(GameType, regGameName, "TestServer");
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
            Debug.Log("Server registered");
    }
    void OnConnectedToServer()
    {
        if (PlayerID > 7)
            PlayerID = 0;
        Application.LoadLevel(1);
        SpawnPlayer();
        PlayerID++;
    }

    void SpawnPlayer()
    {
        //GameObject Player = (GameObject)Network.Instantiate(Resources.Load("Prefab/Tank"), 3 * Vector3.up, Quaternion.identity, 0);
        Debug.Log("Player Spawn Code");
    }

    void OnPlayerDisconnected(NetworkPlayer Player)
    {
        Debug.Log("Player disconnected from :" + Player.ipAddress + "Port:" + Player.port);
        Network.RemoveRPCs(Player);
        Network.DestroyPlayerObjects(Player);
    }

    void OnApplicationQuit()
    {
        if (Network.isServer)
        {
            Network.Disconnect(200);
            MasterServer.UnregisterHost();
        }
        if (Network.isClient)
        {
            Network.Disconnect(200);
        }
    }

    public IEnumerator RefreshHostList()
    {
        MasterServer.RequestHostList(GameType);
        float timeStarted = Time.time;
        float timeEnd = timeStarted + refreshRequestLength;
        while (Time.time < timeEnd)
        {
            hostData = MasterServer.PollHostList();
            uiHandler.BroadcastMessage("Progress", SendMessageOptions.DontRequireReceiver);
            yield return new WaitForEndOfFrame();
        }


        if (hostData == null || hostData.Length == 0)
        {
            Debug.Log("No Active Servers Found");
        }
        else
        {
            Debug.Log(hostData.Length + " " + "Server(s) Found");
            uiHandler.BroadcastMessage("ServerList",hostData, SendMessageOptions.DontRequireReceiver);
        }
    }
}
