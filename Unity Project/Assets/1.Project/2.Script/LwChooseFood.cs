using UnityEngine;
using System.Collections;

public class LwChooseFood : MonoBehaviour {

	public string buttonName = "No";
	public string buttonKal = "0";
	public string buttonPath = "";
	public string buttonPathJPG = "";
	public string buttonPathPNG = "";

	public static string chooseName;
	public static string chooseKal;
	public static string choosePath;
	public static string choosePathJPG;
	public static string choosePathPNG;

	static int id;

	// Unity Override Methods ==============================================================================================================================

	void Start () {
		transform.GetChild(0).gameObject.SetActive(false);
	}

	void Update () {
		if(id != GetInstanceID()){
			transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	// Custom Methods ======================================================================================================================================

	void OnClick () {
		transform.GetChild(0).gameObject.SetActive(true);
		id = GetInstanceID();
		chooseName = buttonName;
		chooseKal = buttonKal;
		choosePath = buttonPath;
		choosePathJPG = buttonPathJPG;
		choosePathPNG = buttonPathPNG;
		print (chooseName + "     " + chooseKal);
	}
}
