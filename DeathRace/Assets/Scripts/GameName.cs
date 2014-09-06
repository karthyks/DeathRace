using UnityEngine;
using System.Collections;

public class GameName : MonoBehaviour {

    NetworkManager network;
    void Start()
    {
        network = GameObject.Find("NetworkHandler").GetComponent<NetworkManager>();
    }
    void OnSubmit()
    {
        network.regGameName = UIInput.current.text;
        Network.InitializeServer(16, 23467, false);
        Application.LoadLevel(1);
    }
}
