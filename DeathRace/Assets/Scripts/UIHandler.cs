using UnityEngine;
using System.Collections;

public class UIHandler : MonoBehaviour {

    NetworkManager network;
    GameObject listBg;
    GameObject serverButton;
    GameObject ProgressBar;
    UISlider slider;

    void Start()
    {
        network = GameObject.Find("NetworkHandler").GetComponent<NetworkManager>();
        listBg = (GameObject)Resources.Load("Prefabs/ServerList/ListBg",typeof(GameObject));
        serverButton = (GameObject)Resources.Load("Prefabs/ServerList/ServerName", typeof(GameObject));
        ProgressBar = (GameObject)Resources.Load("Prefabs/ServerList/Progress Bar", typeof(GameObject));
    }

    void StartServer()
    {
        network.gameObject.BroadcastMessage("StartServer", SendMessageOptions.DontRequireReceiver);
    }

    void FindServer()
    {
        GameObject pBar = (GameObject)Instantiate(ProgressBar, Vector3.zero, Quaternion.identity);
        pBar.name = "ProgressBar";
        UISlider pBarScript = pBar.GetComponent<UISlider>();
        pBar.transform.parent = GameObject.Find("ServerList").transform;
        pBar.transform.localScale = new Vector3(1, 1, 1);
        pBar.transform.localPosition = new Vector3(-250, 80, 0);
        slider = pBarScript;
        slider.sliderValue = 0;
        StartCoroutine(network.RefreshHostList());
    }

    IEnumerator Progress()
    {
        slider.sliderValue += 0.0055f;
        yield return new WaitForSeconds(3.0f);
        Destroy(GameObject.Find("ProgressBar"));
    }
    void Quit()
    {
        Application.Quit();
    }

    void ServerList(HostData[] hData)
    {
        GameObject newServerListBg = (GameObject)Instantiate(listBg, Vector3.zero, Quaternion.identity);
        newServerListBg.transform.parent = GameObject.Find("ServerList").transform;
        newServerListBg.transform.localScale = new Vector3(588, 500, 1);
        newServerListBg.transform.localPosition = new Vector3(-107, 15, 0);

        for (int i = 0; i < hData.Length; i++)
        {
            GameObject listName = (GameObject)Instantiate(serverButton, Vector3.zero, Quaternion.identity);
            UILabel button = listName.GetComponentInChildren<UILabel>();
            button.text = hData[i].gameName;
            listName.name = "Server" + i;
            listName.transform.parent = GameObject.Find("ServerList").transform;
            listName.transform.localScale = new Vector3(1, 1, 1);
            listName.transform.localPosition = new Vector3(-310, 200-(i*50), 0);
            ServerButtonScript buttonScript = listName.GetComponent<ServerButtonScript>();
            buttonScript.setHostData(hData[i]);
        }
    }
}
