using UnityEngine;
using System.Collections;

public class ServerButtonScript : MonoBehaviour {

    HostData DataHost;
    public void setHostData(HostData hData)
    {
        DataHost = hData;
    }
    void OnClick()
    {
        Network.Connect(DataHost); 
    }
    
}
