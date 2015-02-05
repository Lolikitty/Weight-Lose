using UnityEngine;
using System.Collections;

public class LwWaiter2 : MonoBehaviour {

	public UISlider uis;
	public GameObject WaiterBottomObj;
	public TextMesh NowCC;

	public GameObject buttonOk;

	public static int WaiterValue;
	public static int UseCupCC = 200;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {

	}

	void Update () {
		WaiterBottomObj.SetActive(uis.value < 0.02f ? false : true);
		WaiterValue = (int)(uis.value * UseCupCC);
		NowCC.text = UseCupCC - WaiterValue + " c.c.";
	}

	// Custom Methods ======================================================================================================================================



}
