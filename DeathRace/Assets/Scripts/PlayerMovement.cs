using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    ScriptHandler scriptHandler;
    GameObject Gun;
	// Use this for initialization
	void Start () {
        scriptHandler = GameObject.Find("ScriptHandler").GetComponent<ScriptHandler>();
        Gun = GameObject.Find("Gun");

	}
	
	// Update is called once per frame
	void Update () {

        this.transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * scriptHandler.playerMoveSpeed * Time.deltaTime);
        this.transform.Translate(Vector3.left * Input.GetAxis("Vertical") * scriptHandler.playerMoveSpeed * Time.deltaTime);
        transform.Rotate(0, 10 * Input.GetAxis("Mouse X") * Time.deltaTime * scriptHandler.playerMoveSpeed, 0);
        //Gun.transform.Rotate(0, 0, scriptHandler.playerMoveSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime * 10);

        //if (Gun.transform.localEulerAngles.z <= 75)
        //    Gun.transform.localEulerAngles = new Vector3(0, 0, 75);
        //if (Gun.transform.localEulerAngles.z >= 105)
        //    Gun.transform.localEulerAngles = new Vector3(0, 0, 105);
	}
}