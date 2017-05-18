 #pragma strict

var theSourceFile : TextAsset;
var marker : Transform;   // 3D object that will mark data points
var xColumn: float; 
var yColumn: float; 
var zColumn: float; 
var labelColumn: float;

//Rescale data values to match a desired range of space
var xMinMax : Vector2 = Vector2(0,100);  //smallest /largest value of X in data set
var yMinMax : Vector2 = Vector2(0,100);  
var zMinMax : Vector2 = Vector2(0,100);  

var axesMinMax : Vector2 = Vector2(0, 100);  //smallest/largest virtual units for display area

function Start () {

	var myText = theSourceFile.text;
	var myList = myText.Split("#"[0]);	//split text into lines
	for (var i=1; i< myList.length; i++){
		var dataList = myList[i].Split("\t"[0]);  //split each line into columns

		if (dataList.length > 1){
			var x = parseFloat (dataList[xColumn]);
			var y = parseFloat (dataList[yColumn]);
			var z = parseFloat (dataList[zColumn]);
			var myLabel : String = dataList[labelColumn];

			//scale variables to fit the desired range of virtual space
			var xPct : float = (x-xMinMax[0]) / (xMinMax[1] - xMinMax[0]);
			x = (xPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];
			print (y) ;
			print (yMinMax[1] - yMinMax[0]);
			var yPct : float = (y-yMinMax[0]) / (yMinMax[1] - yMinMax[0]);
			y = (yPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];
			print (yPct) ;
			var zPct : float = (z-zMinMax[0]) / (zMinMax[1] - zMinMax[0]);
			z = (zPct * (axesMinMax[1] -axesMinMax[0])) + axesMinMax[0];

			// Use Instantiate to make a copy of the 3D marker at the desired location
			var myMarker : Transform = Instantiate (marker, Vector3(x,y,z) , Quaternion.identity);
			// Send a message to the marker's LabelItems script, calling the SetTect function to set the label's text
			//myMarker.SendMessage ("SetText" , myLabel. SendMessageOptions.DontRequireReceiver);

		}  // end if
	}	// end for 

}

