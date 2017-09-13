using UnityEngine;
using System.Collections;

public class Grapher2 : MonoBehaviour {

	[Range (10, 100)] public int resolution = 10; 
	private int currentResolution;
	private ParticleSystem.Particle[] points; 

	public enum funtionOption{
		Linear,
		Exponential,
		Parabola,
		Sine,
		Ripple
	}

	public funtionOption function;

	void Start () {
		createPoints ();
	}

	private void createPoints(){
		
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution * resolution];
		float increment = 1f / (resolution - 1);
		int i = 0;
		for (int x = 0 ; x < resolution ; x++){
			for (int z = 0 ; z < resolution; z++){
				Vector3 p = new Vector3 (x * increment, 0f, z * increment);
				points [i].position = p;
				points [i].startColor = new Color (p.x, 0f, p.z);
				points [i++].size = 0.07f;
			}
		}

	}

	private delegate float FunctionDelegate (Vector3 p, float t); 
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine,
		Ripple
	};

	void Update () {
		if (currentResolution != resolution || points == null){
			createPoints ();
		}
		FunctionDelegate f = functionDelegates [(int)function]; 
		float t = Time.timeSinceLevelLoad;
		for (int i = 0; i < points.Length; i++) {
			Vector3 p = points [i].position;
			p.y = f (p , t);
			points [i].position = p;
			Color c = points [i].startColor;
			c.g = p.y;
			points [i].startColor = c;

		}
		GetComponent<ParticleSystem> ().SetParticles (points, points.Length);
	}

	private static float Linear(Vector3 p, float t){
		return p.x;
	}
	private static float Exponential(Vector3 p, float t){
		return p.x * p.x;
	}
	private static float Parabola(Vector3 p, float t){
		p.x += p.x - 1f;
		p.z += p.z - 1f;
		return 1f- p.x * p.x * p.z * p.z;
	}
	private static float Sine(Vector3 p, float t){
		return 0.5f +
		0.25f * Mathf.Sin (4f * Mathf.PI * p.x + 4f * t) * Mathf.Sin (2f * Mathf.PI * p.z + t) +
		0.10f * Mathf.Cos (3f * Mathf.PI * p.x + 5f * t) * Mathf.Cos (5f * Mathf.PI * p.z + 3f * t) +
		0.15f * Mathf.Sin (Mathf.PI * p.x + 0.6f * t);



	}

	private static float Ripple (Vector3 p, float t){
		p.x -= 0.5f;
		p.z -= 0.5f;
		float SquareRadius = p.x * p.x + p.z * p.z;
		return 0.5f + Mathf.Sin (15f * Mathf.PI * SquareRadius - 2f * t) / (2f + 100f * SquareRadius);
	}
}
