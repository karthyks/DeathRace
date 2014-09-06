using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.left, out hit))
        {
            Debug.DrawLine(transform.position, hit.transform.position);
        }
	}
}
