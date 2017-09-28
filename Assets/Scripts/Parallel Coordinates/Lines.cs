using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour {
	Color col = new Color(190f,98f,103f);
	List<string> rows = new List<string>();
	Vector2[] AxisPositions =  new Vector2[4];

	void Start () {
		PopulateAxisPositions ();
		//DrawLine (start, end, col);
		rows = DataParcing ();
		DrawLines (rows, col);

	}

	void DrawLine(Vector3 start , Vector3 end, Color color )
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
	}

	void DrawLines (List<string> myList, Color color){
		for (int i = 0; i < myList.Count; i++) 
		{
			List<string> dataList = new List<string> ();
		
			string[] tokens2 = myList [i].Split (',');
			for (int j = 0; j < tokens2.Length ; j++) {
				dataList.Add (tokens2 [j]);
						//Debug.Log(dataList[j]);
			}
			for (int ii = 0; ii < AxisPositions.Length -1; ii++)
			{
				Vector3 start = new Vector3 (AxisPositions[0].x ,float.Parse(dataList [0]), AxisPositions[0].y);
				Vector3 end = new Vector3 (AxisPositions[ii+1 ].x, float.Parse (dataList [ii +1 ]), AxisPositions[ii +1].y);
				GameObject myLine = new GameObject();
				myLine.transform.position = start;
				myLine.AddComponent<LineRenderer>();
				LineRenderer lr = myLine.GetComponent<LineRenderer>();
				lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
				//lr.SetColors(color, color);
				lr.SetWidth(0.1f, 0.1f);
				lr.SetPosition(0, start);
				lr.SetPosition(1, end);


				// A simple 2 color gradient with a fixed alpha of 1.0f.
				GradientColorKey[]  linegradient  ;
				if(ii ==0)
				{
					linegradient = new GradientColorKey[] {
						new GradientColorKey (Color.grey, 0.0f),
						new GradientColorKey (Color.green, 1.0f)
					};
				}
				else if (ii == 1)
				{	
					linegradient = new GradientColorKey[] {
						new GradientColorKey (Color.grey, 0.0f),
						new GradientColorKey (Color.yellow, 1.0f)
					};				}
				else
				{
					linegradient = new GradientColorKey[] {
						new GradientColorKey (Color.grey, 0.0f),
						new GradientColorKey (Color.red, 1.0f)
					};
				}
				float alpha = 1.0f;
				Gradient gradient = new Gradient();
				gradient.SetKeys(
					

					linegradient,
					new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
				);
				lr.colorGradient = gradient;
			}
		}
	}


	List<string> DataParcing ()
	{
		TextAsset theSourceFile = Resources.Load ("PCSample") as TextAsset ;
		string myText = theSourceFile.text;
		List<string> myList = new List<string>();
		string[] tokens = myText.Split("\n"[0]);
		for (int ii = 0; ii < tokens.Length -1; ++ii)
		{
			//Debug.Log( tokens[ii] );
			myList.Add (tokens[ii]) ;	//split text into lines
			Debug.Log(myList[ii]);

		}
		return myList;
//		for (int i = 0; i < myList.Count; i++) {
//			List<string> dataList = new List<string> ();
//
//			//Debug.Log(myList[i]);
//			string[] tokens2 = myList [i].Split (',');
//
//			// dataList = myList[i].Split("\t");  //split each line into columns
//
//			for (int j = 0; j < tokens2.Length - 1; j++) {
//				dataList.Add (tokens2 [j]);
//				Debug.Log(dataList[j]);
//
//			}
//		}

	}

	void PopulateAxisPositions()
	{
		AxisPositions [0] = new Vector2 (0, 0);
		AxisPositions [2] = new Vector2 (3.54f, 3.54f);
		AxisPositions [3] = new Vector2 (- 3.54f, 3.54f);
		AxisPositions [1] = new Vector2 (0, 5);

	}


}
