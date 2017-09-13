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

	[Range(1, 60)] public int resolution = 60;
	private int currentResolution;
	public Vector3 offset;
	public Vector3 rotation;
	[Range(0f, 3f)] public float strength = 1f;
	public bool coloringForStrength;
	public float frequency = 1f;
	[Range(1, 8)] public int octaves = 1;
	[Range(1f, 4f)] public float lacunarity = 2f;
	[Range(0f, 1f)] public float persistence = 0.5f;
	[Range(1, 3)] public int dimensions = 3;
	//public NoiseMethodType type;
	public Gradient coloring;
	public bool damping;
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
		
		CreateGrid();

		if (resolution != currentResolution) {
			CreateGrid();
		}
//		Quaternion q = Quaternion.Euler(rotation);
//		Vector3 point00 = q * new Vector3(-0.5f, -0.5f) + offset;
//		Vector3 point10 = q * new Vector3( 0.5f, -0.5f) + offset;
//		Vector3 point01 = q * new Vector3(-0.5f, 0.5f) + offset;
//		Vector3 point11 = q * new Vector3( 0.5f, 0.5f) + offset;
//
//		float stepSize = 1f / resolution;
		float amplitude  = 1;
//		//float[] samples = {1, 2, 1, 2, 5, 0 ,6 ,7 ,7,3,4,5,6,7,8,9,0,3,1,4,6,8,3,5,7,8,45,3,3,3,2,};
//		//float[] samples = {0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,};
		float [] samples = {0,0,0,0,0,0,0,0,0,0,0,5,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
		for (int v = 0, y = 0; y <= resolution; y++) {
			//Vector3 point0 = Vector3.Lerp(point00, point01, y * stepSize);
			//Vector3 point1 = Vector3.Lerp(point10, point11, y * stepSize);
			for (int x = 0; x <= resolution; x++, v++) {
				//Vector3 point = Vector3.Lerp(point0, point1, x * stepSize);
				//float sample = samples[v];
				//float sample = Noise.Sum(method, point, frequency, octaves, lacunarity, persistence);
				//sample = type == NoiseMethodType.Value ? (sample - 0.5f) : (sample * 0.5f);
				if (coloringForStrength) {
					colors[v] = coloring.Evaluate(testarw[v]/ 300f);
					//sample *= amplitude;
				}
				else {
					//sample *= amplitude;
					colors[v] = coloring.Evaluate(testarw[v]/ 300f);
				}
				//vertices[v].y = sample;
			}
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.RecalculateNormals();
		normals = mesh.normals;


	}

	private Vector3[] vertices;
	private Vector3[] normals;
	private Color[] colors;

	private void CreateGrid () {
		//testarw	 = parsing();
		//Debug.Log (testarw.Length);
		currentResolution = resolution;
		mesh.Clear();
		vertices = new Vector3[(resolution + 1) * (resolution + 1)];
		colors = new Color[vertices.Length];
		normals = new Vector3[vertices.Length];

		Vector2[] uv = new Vector2[vertices.Length];
		float stepSize = 1f / resolution + 10;
		//float[] samples = {1, 2, 1, 2, 5, 0 ,6 ,7 ,7,3,4,5,6,7,8,9,0,3,1,4,6,8,3,5,7,8,45,3,3,3,2,1, 2, 3, 4, 5, 0 ,6 ,7 ,7,3,4,5,6,7,8,9,0,3,1,4,6,8,3,5,7,8,45,3,3,3,2,1, 2, 3, 4, 5, 0 ,6 ,7 ,7,3,4,5,6,7,8,9,0,3,1,4,6,8,3,5,7,8,45,3,3,3,2};
		//float[] samples = {0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,};
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
		Debug.Log (tokens2 [0]);
		myText = myText.Replace("\r", ",").Replace("\n", ",");
		string[] tokens = myText.Split(","[0]);

		float[] dataY =Array.ConvertAll<string, float>(tokens, float.Parse);


		Debug.Log (dataY[0]);
		Debug.Log (dataY[59]);
		Debug.Log (dataY[60]);


		return dataY;
	}

}