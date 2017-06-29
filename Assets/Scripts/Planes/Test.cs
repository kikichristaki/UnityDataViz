using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour {

	public Material planeMat;

	void Start () {
		PlaneXY.CreatePlane(50, 50, planeMat);
		PlaneYZ.CreatePlane(50, 50, planeMat);
		PlaneXZ.CreatePlane(50, 50, planeMat);


	}
	
}
