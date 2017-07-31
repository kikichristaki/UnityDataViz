using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour {

	public Material planeMat;

	void Start () {
		PlaneXY.CreatePlane(100, 100, planeMat);
		PlaneYZ.CreatePlane(100, 100, planeMat);
		PlaneXZ.CreatePlane(100, 100, planeMat);


	}
	
}
