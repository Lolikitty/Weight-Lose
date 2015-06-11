using UnityEngine;
using System.Collections;

public class FoodCamera2Warning : MonoBehaviour {

	public GameObject warningObj;

	void OnClick () {
		warningObj.SetActive (false);
	}
}
