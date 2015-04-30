using UnityEngine;
using System.Collections;

public class LwFoodHistory_Food : MonoBehaviour {

	public string pathJPG;
	public string pathPNG;
	public string pathInfo;
	public string [] fileInformation;

	// Unity Override Methods ==============================================================================================================================

	void OnClick () {
		LwFoodInformation.fileInformation = fileInformation;
		LwFoodInformation.pathJPG = pathJPG;
		LwFoodInformation.pathPNG = pathPNG;
		LwFoodInformation.pathInfo = pathInfo;
		Application.LoadLevel ("FoodInformation");
	}

	// Custom Methods ======================================================================================================================================

}
