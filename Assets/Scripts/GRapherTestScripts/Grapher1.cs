using UnityEngine;
using System.Collections;

public class Grapher1 : MonoBehaviour {

	[Range (10, 100)] public int resolution = 10; 
	private int currentResolution;
	private ParticleSystem.Particle[] points; 

	public enum funtionOption{
		Linear,
		Exponential,
		Parabola,
		Sine
	}

	public funtionOption function;

	void Start () {
		createPoints ();
	}

	private void createPoints(){
		

		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution];
		float increment = 1f / (resolution - 1);

		for (int i = 0 ; i < resolution ; i++){
			float x = i * increment;
			points [i].position = new Vector3 (x, 0f, 0f);
			points [i].startColor = new Color (x, 0f, 0f);
			points [i].size = 0.1f; 	
		}

	}

	private delegate float FunctionDelegate (float x); 
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine
	};

	void Update () {
		if (currentResolution != resolution || points == null){
			createPoints ();
		}
		FunctionDelegate f = functionDelegates [(int)function]; 
		for (int i = 0; i < resolution; i++) {
			Vector3 p = points [i].position;
			p.y = f (p.x);
			points [i].position = p;
			Color c = points [i].startColor;
			c.g = p.y;
			points [i].startColor = c;

		}
		GetComponent<ParticleSystem> ().SetParticles (points, points.Length);
	}

	private static float Linear(float x){
		return x;
	}
	private static float Exponential(float x){
		return x * x;
	}
	private static float Parabola(float x){
		x = 2f * x - 1f;
		return x * x;
	}
	private static float Sine(float x){
		return 0.5f * 0.5f * Mathf.Sin (2 * Mathf.PI * x + Time.timeSinceLevelLoad);
	}
}
