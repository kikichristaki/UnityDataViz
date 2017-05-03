using UnityEngine;
using System.Collections;

public class Grapher4 : MonoBehaviour {

	[Range (10, 30)] public int resolution = 10; 
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
	public bool absolute;
	public float threshold = 0.5f;

	void Start () {
		createPoints2 ();
	}

	private void createPoints(){
		
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution * resolution * resolution];
		float increment = 1f / (resolution - 1);
		int i = 0;
		for (int x = 0 ; x < resolution ; x++){
			for (int z = 0 ; z < resolution; z++){
				for(int y =0 ; y <resolution ; y ++){
					Vector3 p = new Vector3 (x , y, z ) * increment;
					points [i].position = p;
					points [i].color = new Color (p.x, p.y, p.z);
					points [i++].size = 0.1f;
				}
			}
		}

	}



	private void createPoints2(){

		currentResolution = resolution;
		points = new ParticleSystem.Particle[9];
		float increment = 1f / (resolution - 1);
		int x = 0, y = 0, z = 0; 
		for (int i = 0 ; i < resolution ; i++){
			x++;
			y++;
			z++;
					Vector3 p = new Vector3 (x , y, z ) ;
					points [i].position = p;
					points [i].color = new Color (p.x, p.y, p.z);
					points [i++].size = 1f;

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





	private static float Linear(Vector3 p, float t){
		return 1f - p.x - p.y - p.z + 0.5f * Mathf.Sin (t);
	}
	private static float Exponential(Vector3 p, float t){
		return 1f - p.x * p.x - p.y * p.y - p.z * p.z + 0.5f * Mathf.Sin (t);
	}
	private static float Parabola(Vector3 p, float t){
		p.x += p.x - 1f;
		p.z += p.z - 1f;
		return 1f- p.x * p.x - p.z * p.z + 0.5f * Mathf.Sin (t);
	}
	private static float Sine(Vector3 p, float t){
		float x = Mathf.Sin (2 * Mathf.PI * p.x);
		float y = Mathf.Sin (2 * Mathf.PI * p.y);
		float z = Mathf.Sin (2 * Mathf.PI * p.z + (p.y > 0.5f ? t : -t));
		return x * x * y * y * z * z;

	}

	private static float Ripple (Vector3 p, float t){
		p.x -= 0.5f;
		p.y -= 0.5f;
		p.z -= 0.5f;
		float SquareRadius = p.x * p.x + p.y * p.y + p.z * p.z;
		return Mathf.Sin (4f * Mathf.PI * SquareRadius - 2f * t);
	}
}
