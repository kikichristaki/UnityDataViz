using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh>().GetComponent<Renderer>().sortingOrder = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (itemTextControl.textStatus == "off")
		{
			Destroy(gameObject);
		}
	}
}
