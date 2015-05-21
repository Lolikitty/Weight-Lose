using UnityEngine;
using System.Collections;
using System;

public class LwFoodHistory_Food : MonoBehaviour {

	public DateTime pathDate;
	public string pathJPG;
	public string pathPNG;

	// Unity Override Methods ==============================================================================================================================

	void OnClick () {
		LwFoodInformation.pathDate = pathDate;
		LwFoodInformation.pathJPG = pathJPG;
		LwFoodInformation.pathPNG = pathPNG;
		Application.LoadLevel ("FoodInformation");
	}

	// Custom Methods ======================================================================================================================================

}
