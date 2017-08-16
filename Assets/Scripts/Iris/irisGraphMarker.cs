using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using System.IO; 

#if UNITY_EDITOR
using UnityEditor;
#endif

public class irisGraphMarker : MonoBehaviour {


	public  TextAsset theSourceFile ;
	public GameObject marker;
	public GameObject Sphereholder;

	public enum ColumnProperty {
		sepalLength,
		sepalWidth,
		petalLength,
		petalWidth
	}

	public ColumnProperty X = ColumnProperty.sepalLength;
	public ColumnProperty Y = ColumnProperty.sepalWidth;
	public ColumnProperty Z = ColumnProperty.petalLength;

	public int gene = 4;

	//Rescale data values to match a desired range of space
	public Vector2 xMinMax  = new Vector2(0, 100) ;
	public Vector2 yMinMax  = new Vector2(0,100);  
	public Vector2 zMinMax  = new Vector2(0,100);  

	public Vector2 axesMinMax = new  Vector2(0, 100);  //smallest/largest virtual units for display area

	public pointClass point ;


	// Use this for initialization
	void Start () {
		theSourceFile = Resources.Load ("iris") as TextAsset;
		string myText = theSourceFile.text;

		List<string> myList = new List<string>();

		marker = Resources.Load ("Sphere2") as GameObject;

		int xColumn = getProperty (X);
		int yColumn = getProperty (Y);
		int zColumn = getProperty (Z);

		Debug.Log ("xColumn = " + xColumn);
		Debug.Log ("yColumn = " + yColumn);
		Debug.Log ("zColumn = " + zColumn);




		//string[] tokens = myText.Split('#');
		string[] tokens = myText.Split("\n"[0]);
		//Debug.Log(tokens.Length);

		for (int ii = 0; ii < tokens.Length -1; ++ii)
		{
			//Debug.Log( tokens[ii] );
			myList.Add (tokens[ii]) ;	//split text into lines

			//Debug.Log (myList[myList.Count - 1]);

		}
		for (int i=0; i< myList.Count; i++){
			List<string> dataList = new List<string>();

			//Debug.Log(myList[i]);
			string[] tokens2 = myList[i].Split(',');

			// dataList = myList[i].Split("\t");  //split each line into columns

			for  (int j=0; j< tokens2.Length ; j++){
				dataList.Add(tokens2[j]);
				//Debug.Log(dataList[j]);

			}
			if (dataList.Count > 1){
				//Debug.Log(dataList.Count);
				float x = float.Parse(dataList[xColumn]) ;
				float y = float.Parse (dataList[yColumn]);
				float z = float.Parse (dataList[zColumn]);
				string category = dataList [dataList.Count -1];
				//Debug.Log (category);

				//scale variables to fit the desired range of virtual space
				float xPct   = (x-xMinMax[0]) / (xMinMax[1] - xMinMax[0]);
				x = (xPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];
				// print (y) ;
				// print (yMinMax[1] - yMinMax[0]);
				float yPct = (y-yMinMax[0]) / (yMinMax[1] - yMinMax[0]);
				y = (yPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];
				//print (yPct) ;
				float zPct  = (z-zMinMax[0]) / (zMinMax[1] - zMinMax[0]);
				z = (zPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];

				Vector3 vectoras = new Vector3(x,y,z);

				// Use Instantiate to make a copy of the 3D marker at the desired location
				GameObject myMarker   = Instantiate (marker, vectoras , Quaternion.identity) as GameObject;
				myMarker.transform.parent = Sphereholder.transform;

				pointClass point = myMarker.GetComponent<pointClass> ();
				point.xPosition = x;
				point.yPosition = y;
				point.zPosition = z;
				//pointClass point = new pointClass(x);
				//point.name = vectoras.x;


				//giving a name to every point

				string pointName = 
					"["+ x + " " + y + " " + z + "]";
				point.transform.name = pointName;
				print (pointName);
				//temp.GetComponent<this>().cell;
				if (category == "Iris-setosa"){
					myMarker.GetComponent<Renderer>().material.color = Color.blue ;
				}
				else if (category == "Iris-versicolor"){
					myMarker.GetComponent<Renderer>().material.color = Color.green ;
				}
				else if (category == "Iris-virginica"){
					myMarker.GetComponent<Renderer>().material.color = Color.yellow ;
				}
				else {
					myMarker.GetComponent<Renderer>().material.color = Color.red ;

				}
				// Send a message to the marker's LabelItems script, calling the SetTect function to set the label's text
				//myMarker.SendMessage ("SetText" , myLabel. SendMessageOptions.DontRequireReceiver);

			}  // end if

		}



	}


	private int  getProperty( ColumnProperty o){
		Debug.Log ("xColumnProperty = " + ColumnProperty.sepalLength);
		Debug.Log ("X = " + o);
		int xstili; 
		if (o == ColumnProperty.sepalLength) {
			//Debug.Log ("isxyei");
			xstili = 0;
		} 
		else if (o == ColumnProperty.sepalWidth) {
			xstili = 1;
		} 
		else if (o == ColumnProperty.petalLength) {
			xstili = 2;
		} 
		else {
			xstili = 3;
		}
		//Debug.Log (xstili);

		return xstili;

	}
}
