using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; 


#if UNITY_EDITOR
using UnityEditor;
#endif

public class DetroitGraphMarker : MonoBehaviour {


	public  TextAsset theSourceFile ;
	public GameObject marker;

	public enum ColumnProperty {
		FTP,
		UEMP,
		MAN,
		LIC,
		GR,
		CLEAR,
		WM,
		NMAN,
		GOV,
		HE,
		WE,
		HOM,
		ACC,
		ASR
	}

	public ColumnProperty X ;
	public ColumnProperty Y ;
	public ColumnProperty Z ;

	//public int gene = 4;

	//Rescale data values to match a desired range of space
	public Vector2 xMinMax  = new Vector2(0, 100) ;
	public Vector2 yMinMax  = new Vector2(0,100);  
	public Vector2 zMinMax  = new Vector2(0,100);  

	public Vector2 axesMinMax = new  Vector2(0, 100);  //smallest/largest virtual units for display area

	public pointClass point ;


	// Use this for initialization
	void Start () {

		string myText = theSourceFile.text;
		List<string> myList = new List<string>();

		int xColumn = getProperty (X);
		int yColumn = getProperty (Y);
		int zColumn = getProperty (Z);

		Debug.Log ("xColumn = " + xColumn);
		Debug.Log ("yColumn = " + yColumn);
		Debug.Log ("zColumn = " + zColumn);




		//string[] tokens = myText.Split('#');
		string[] tokens = myText.Split("\n"[0]);

		for (int ii = 1; ii < tokens.Length -1; ++ii)
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
			float xmax = GetMax (zColumn, myList);
			float ymax = GetMax (yColumn, myList);
			float zmax = GetMax (zColumn, myList);


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
				//float xPct   = (x-xMinMax[0]) / (xMinMax[1] - xMinMax[0]);
				//x = (xPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];
				x = axesMinMax[1] * x/xmax;
				//print (x) ;
				// print (yMinMax[1] - yMinMax[0]);
				//float yPct = (y-yMinMax[0]) / (yMinMax[1] - yMinMax[0]);
				//y = (yPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];
				y = axesMinMax [1] * y / ymax;
				print (y) ;
//				float zPct  = (z-zMinMax[0]) / (zMinMax[1] - zMinMax[0]);
//				z = (zPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];
				z = axesMinMax[1]*z /zmax;

				Vector3 vectoras = new Vector3(x,y,z);

				// Use Instantiate to make a copy of the 3D marker at the desired location
				GameObject myMarker   = Instantiate (marker, vectoras , Quaternion.identity) as GameObject;
				pointClass point = myMarker.GetComponent<pointClass> ();
				point.xPosition = x;
				point.yPosition = y;
				point.zPosition = z;
				//pointClass point = new pointClass(x);
				//point.name = vectoras.x;

				//temp.GetComponent<this>().cell;
//				if (category == "Iris-setosa"){
//					myMarker.GetComponent<Renderer>().material.color = Color.blue ;
//				}
//				else if (category == "Iris-versicolor"){
//					myMarker.GetComponent<Renderer>().material.color = Color.green ;
//				}
//				else if (category == "Iris-virginica"){
//					myMarker.GetComponent<Renderer>().material.color = Color.yellow ;
//				}
//				else {
//					myMarker.GetComponent<Renderer>().material.color = Color.red ;
//
//				}
				myMarker.GetComponent<Renderer>().material.color = Color.red ;

				// Send a message to the marker's LabelItems script, calling the SetTect function to set the label's text
				//myMarker.SendMessage ("SetText" , myLabel. SendMessageOptions.DontRequireReceiver);

			}  // end if

		}



	}


	private int  getProperty( ColumnProperty o){
		//Debug.Log ("xColumnProperty = " + ColumnProperty.sepalLength);
		//Debug.Log ("X = " + o);
		int xstili; 
		if (o == ColumnProperty.FTP) {
			//Debug.Log ("isxyei");
			xstili = 0;
		}
		else if (o == ColumnProperty.UEMP) {
			xstili = 1;
		} 
		else if (o == ColumnProperty.MAN) {
			xstili = 2;
		} 
		else if (o == ColumnProperty.LIC) {
			xstili = 3;
		} 
		else if (o == ColumnProperty.GR) {
			xstili = 4;
		} 
		else if (o == ColumnProperty.CLEAR) {
			xstili = 5;
		} 
		else if (o == ColumnProperty.WM) {
			xstili = 6;
		} 
		else if (o == ColumnProperty.NMAN) {
			xstili = 7;
		} 
		else if (o == ColumnProperty.GOV) {
			xstili = 8;
		} 
		else if (o == ColumnProperty.HE) {
			xstili = 9;
		} 
		else if (o == ColumnProperty.WE) {
			xstili = 10;
		} 
		else if (o == ColumnProperty.HOM) {
			xstili = 11;
		} 
		else if (o == ColumnProperty.ACC) {
			xstili = 12;
		} 
		else if (o == ColumnProperty.ASR) {
			xstili = 13;
		} 

		else {
			xstili = 0;
		}
		//Debug.Log (xstili);

		return xstili;

	}

	public float GetMax(int k, List<string> myList){
		float max = 0; 
		for (int i=0; i< myList.Count-1; i++){
			string[] tokens2 = myList[i].Split(',');
			//Debug.Log (tokens2.Length);
			float val = float.Parse(tokens2[k]);
				if (max < val)
				{
					max = val;
					//Debug.Log("max = " + max + " val = " + val );
				}
		}
		return max;
	}
}
