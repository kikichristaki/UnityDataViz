using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTextControl : MonoBehaviour {

	public Transform popupText;
	public static string textStatus = "off";
	Camera cam;
	 //public Transform target;

	void Start()
	{
		//pointClass pc = GetComponent<pointClass> ();
	}

	void OnMouseEnter()
	{
		if (textStatus == "off")
		{
			pointClass pc = GetComponent<pointClass> ();
			string tooltipText = "(" + pc.xPosition.ToString () +
			                 " , " + pc.yPosition.ToString () +
			                 " , " + pc.zPosition.ToString () + ")";
				//GetComponentInParent<pointClass> ();
			Vector3 screenPoint = new Vector3 (250, 250, 20);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint ( screenPoint );
			//popupText.GetComponent<TextMesh>().text = "works somehow";
			//name = graphMarker.FindObjectOfType<pointClass>.;
			popupText.GetComponent<TextMesh> ().text = tooltipText;

			textStatus = "on";
			//Instantiate(popupText, worldPos , popupText.rotation);
			Transform tooltip =  Instantiate(popupText, worldPos , Quaternion.identity);

			tooltip.transform.LookAt(Camera.main.transform);

			tooltip.transform.Rotate (0, 180, 0);


		}
	}

	void OnMouseExit()
	{
		if (textStatus =="on")
		{
			textStatus = "off";
		}
	}
}
