using UnityEngine;
using System.Collections;
using System;

public class LwUser_Food : MonoBehaviour {

	// Unity Override Methods ==============================================================================================================================

	void OnClick () {
		LwFoodHistory.chooseYear = int.Parse(DateTime.Now.ToString ("yyyy"));
		LwFoodHistory.chooseMonth = int.Parse(DateTime.Now.ToString ("MM"));
		Application.LoadLevel ("FoodHistory");
	}

	// Custom Methods ======================================================================================================================================

}
