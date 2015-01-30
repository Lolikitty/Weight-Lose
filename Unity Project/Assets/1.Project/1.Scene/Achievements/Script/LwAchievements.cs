using UnityEngine;
using System.Collections;

public class LwAchievements : MonoBehaviour {

	public GameObject buttonExit;
	public GameObject barControl;
	public Transform sv;

	float start_x = -130.0f; //起始x座標
	float start_y = 260.0f; //起始y座標
	float per_y_range = 130.0f; //間距
	float scale_x = 1.35182f ,scale_y = 1.837994f; //縮放倍數
	int count = 20; //數量
	// Unity Override Methods ==============================================================================================================================

	void Start () {
		UIEventListener.Get(buttonExit).onClick = ButtonExit;

		for (int i = 0; i < count; i++, start_y -= per_y_range) {
			GameObject g = Instantiate (barControl) as GameObject;
			g.transform.parent = sv;
			g.transform.localPosition = new Vector3 (start_x, start_y);
			g.transform.localScale = new Vector3 (scale_x, scale_y);
		}
	}

	void Update () {	
	}

	// Custom Methods ======================================================================================================================================

	void ButtonExit(GameObject obj){
		Application.LoadLevel ("MainMenu");
	}
}
