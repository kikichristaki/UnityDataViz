 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;

 //using UnityEditor;
 using System.IO; 

#if UNITY_EDITOR
using UnityEditor;
#endif

 public class Grapher5 : MonoBehaviour
 {
     ParticleSystem.Particle[] cloud;
     bool bPointsUpdated = false;
	 public	Vector3[] positionArray = new Vector3[9];
	 public Color[] colors = new Color[9]; 
	 
     void Start ()
     {
                Read();

     	//populatePositionArray();
	    populateColors(); 
     	SetPoints (positionArray, colors );
     }
     
     void Update () 
     {
         if (bPointsUpdated)
         {
             GetComponent<ParticleSystem>().SetParticles(cloud, cloud.Length);
             bPointsUpdated = false;
         }
     }
     
     public void SetPoints(Vector3[] positions, Color[] colors)
     {        
         cloud = new ParticleSystem.Particle[positions.Length];
          Debug.Log("ginetai kati")    ;
         for (int ii = 0; ii < positions.Length; ++ii)
         {
             cloud[ii].position = positions[ii];            
			 cloud[ii].startColor = colors[ii];
             cloud[ii].size = 0.051f;        
             Debug.Log(cloud[ii])    ;
         }
 
         bPointsUpdated = true;
     }

     public void populatePositionArray()
     {
		positionArray[0] = new Vector3(0.58f,0.9f,0.87f);            
	 	positionArray[1] = new Vector3(0.1f,0.1f,0.1f);
	 	positionArray[2] = new Vector3(0.2f,0.3f,0.4f);
	 	positionArray[3] = new Vector3(0.5f,0.5f,0.5f);
	 	positionArray[4] = new Vector3(0.7f,0.8f,0.9f);            
	 	positionArray[5] = new Vector3(0.16f,0.18f,0.1f);
	 	positionArray[6] = new Vector3(0.26f,0.39f,0.9f);
	 	positionArray[7] = new Vector3(0.58f,0.9f,0.87f);

     }

     public void populateColors()
     {
     	colors[0] = Color.cyan;
        colors[1] = Color.red;
        colors[2] = Color.green;
        colors[3] = Color.magenta;
        colors[4] = Color.cyan;
        colors[5] = Color.red;
        colors[6] = Color.green;
        colors[7] = Color.magenta;
        colors[8] = Color.red;

     }

     public void Read()
    {
        string path = "Assets/Scripts/data1.csv";
        string line;
        int index = 0;


        StreamReader theReader = new StreamReader(path);
        //Debug.Log(theReader.ReadToEnd());

        using (theReader)
         {
             // While there's lines left in the text file, do this:
             do
             {
                 line = theReader.ReadLine();
                
                 if (line != null)
                 {
                    
                     string[] entries = line.Split(',');

                     if (entries.Length > 0)
                     {
                     //  float x =     float.TryParse(entries[0]);
                        float number;
                        
                         Debug.Log ( float.Parse(entries[0]) );
                       positionArray[index] = new Vector3(float.Parse(entries[0]),float.Parse(entries[1]),float.Parse(entries[2]) );        

                   // Debug.Log (entries[0]);

                    // Debug.Log ("mpainei edw?");
                        index++;
                     }
                 }
             }
             while (line != null);
             // Done reading, close the reader and return true to broadcast success    
             theReader.Close();
             
         }
        //reader.Close();

    }
 }








