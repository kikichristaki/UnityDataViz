using UnityEngine;
using System.Collections;

public class ZoomCam : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () 
	{
		if (Input.GetAxis("Mouse ScrollWheel") >0 )
		{
			//GetComponent<Camera>().fieldOfView --; 
			GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, transform.position.z -0.3f) ;    
		}
		if (Input.GetAxis("Mouse ScrollWheel") <0 )
		{
			//GetComponent<Camera>().fieldOfView ++;
			GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, transform.position.z +0.3f) ;  

		}
	}
}
