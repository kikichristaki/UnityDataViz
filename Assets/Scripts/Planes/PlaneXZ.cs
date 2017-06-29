using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaneXZ  {
	
	public static GameObject CreatePlane(float width, float height, Material mat)
	{
		GameObject go = new GameObject("PlaneXZ");
		MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
		MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		Mesh m = new Mesh(); 
		m.vertices = new Vector3[]
		{
			new Vector3(0,0,0),
			new Vector3(width,0,0),
			new Vector3(width,0,height),
			new Vector3(0,0,height)
		};

		m.uv = new Vector2[]
		{
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};

		m.triangles = new int[]{0,2,1,0,3,2};

		mr.material = mat;
		mf.mesh = m;
		m.RecalculateNormals();

		return go; 
	}
	
}
