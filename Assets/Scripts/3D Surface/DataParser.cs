using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


public static  class DataParser  {

	public static float[]  parsing () {
		TextAsset theSourceFile = Resources.Load ("test1") as TextAsset ;
		string myText = theSourceFile.text;
		myText = myText.Replace("\r", "").Replace("\n", "");
		string[] tokens = myText.Split(","[0]);
		float[] dataY =Array.ConvertAll<string, float>(tokens, float.Parse);


		//Debug.Log (dataY[120]);

		return dataY;
	}

}