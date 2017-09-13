using UnityEngine;
using System; 

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SurfaceCreator : MonoBehaviour {

	private Mesh mesh; 
	//static DataParser parser;

	private void OnEnable () {
		if (mesh == null) {
			mesh = new Mesh();
			mesh.name = "Surface Mesh";
			GetComponent<MeshFilter>().mesh = mesh;

		}
		testarw	 = parsing();
		Refresh();
		//generateMesh();
	}

	private int resolution = 60;
	public Vector3 rotation;
	[Range(0f, 3f)] public float strength = 1f;
	public bool coloringForStrength;
	public Gradient coloring;
	float[] testarw ;



//	public void generateMesh(){
//		vertices = new Vector3[(resolution + 1) * (resolution + 1)];
//		colors = new Color[vertices.Length];
//		normals = new Vector3[vertices.Length];
//		
//		Vector2[] uv = new Vector2[vertices.Length];
//		float stepSize = 1f / resolution;
//		float [] samples = {0,0,0,0,0,0,0,0,0,0,0,5,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
//
//		for (int v = 0, z = 0; z <= resolution; z++) {
//			for (int x = 0; x <= resolution; x++, v++) {
//				vertices[v] = new Vector3(x *stepSize - 0.5f, samples[v], z * stepSize - 0.5f);
//				colors[v] = Color.black;
//				normals[v] = Vector3.up;
//				uv[v] = new Vector2(x * stepSize, z * stepSize);
//			}
//		}
//			
//		mesh.vertices = vertices;
//		mesh.colors = colors;
//		mesh.normals = normals;
//		mesh.uv = uv;
//			
//		int[] triangles = new int[resolution * resolution * 6];
//		for (int t = 0, v = 0, y = 0; y < resolution; y++, v++) {
//			for (int x = 0; x < resolution; x++, v++, t += 6) {
//				triangles[t] = v;
//				triangles[t + 1] = v + resolution + 1;
//				triangles[t + 2] = v + 1;
//				triangles[t + 3] = v + 1;
//				triangles[t + 4] = v + resolution + 1;
//				triangles[t + 5] = v + resolution + 2;
//			}
//		}
//		mesh.triangles = triangles;
//	}

	public void Refresh () {
		CreateMesh();

		Quaternion quart = Quaternion.Euler(rotation);
		Quaternion currentquart = quart ;

		float amplitude  = 2* strength;
		//float [] samples = {0,0,0,0,0,0,0,0,0,0,0,5,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
		for (int v = 0, y = 0; y <= resolution; y++) {
			for (int x = 0; x <= resolution; x++, v++) {
				if (coloringForStrength) {
					colors[v] = coloring.Evaluate(testarw[v]* amplitude/ 300f);
				}
				else {
					colors[v] = coloring.Evaluate(testarw[v]/ 300f);
				}
			}
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.RecalculateNormals();
		normals = mesh.normals;

		transform.rotation = currentquart;


	}

	private Vector3[] vertices;
	private Vector3[] normals;
	private Color[] colors;

	private void CreateMesh () {
		
		mesh.Clear();
		vertices = new Vector3[(resolution + 1) * (resolution + 1)];
		colors = new Color[vertices.Length];
		normals = new Vector3[vertices.Length];

		Vector2[] uv = new Vector2[vertices.Length];
		float stepSize = 1f / resolution + 10;
		//float [] samples = {0,0,0,0,0,0,0,0,0,0,0,5,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
		for (int v = 0, z = 0; z <= resolution; z++) {
			for (int x = 0; x <= resolution; x++, v++) {
				vertices[v] = new Vector3(x *stepSize - 0.5f, stepSize * testarw[v] - 0.5f, z * stepSize - 0.5f);
				colors[v] = Color.black;
				normals[v] = Vector3.up;
				uv[v] = new Vector2(x * stepSize, z * stepSize);
			}
		}

		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.normals = normals;
		mesh.uv = uv;

		int[] triangles = new int[resolution * resolution * 6];
		for (int t = 0, v = 0, y = 0; y < resolution; y++, v++) {
			for (int x = 0; x < resolution; x++, v++, t += 6) {
				triangles[t] = v;
				triangles[t + 1] = v + resolution + 1;
				triangles[t + 2] = v + 1;
				triangles[t + 3] = v + 1;
				triangles[t + 4] = v + resolution + 1;
				triangles[t + 5] = v + resolution + 2;
			}
		}
		mesh.triangles = triangles;
	}

	public float[]  parsing () {
		TextAsset theSourceFile = Resources.Load ("volcan") as TextAsset ;
		string myText = theSourceFile.text;
		string[] tokens2 = myText.Split("\n"[0]);
		//Debug.Log (tokens2 [0]);
		myText = myText.Replace("\r", ",").Replace("\n", ",");
		string[] tokens = myText.Split(","[0]);

		float[] dataY =Array.ConvertAll<string, float>(tokens, float.Parse);

		return dataY;
	}

}