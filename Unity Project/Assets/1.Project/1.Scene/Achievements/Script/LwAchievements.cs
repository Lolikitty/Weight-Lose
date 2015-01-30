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
	public int count; //數量
	public int[] dayValue; //每天的成就狀態，對應BlueMark，RedMark，GrayMark。
	public int strSize;
	public string[] titleStr;
	public string[] titleContentStr;

	// Unity Override Methods ==============================================================================================================================
	void Awake(){
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		for (int i = 0; i < count; i++, start_y -= per_y_range) {
			

			for(int j=0;j<7;j++){
				dayValue[j] = Random.Range(0,999)%3;
				//print (dayValue[j]);
			}

			GameObject g = Instantiate (barControl) as GameObject;
			g.transform.parent = sv;
			g.transform.localPosition = new Vector3 (start_x, start_y);
			g.transform.localScale = new Vector3 (scale_x, scale_y);
//			g.GetComponent<BarControl>().TitleStr = titleStr[Random.Range(0,5)];
//			g.GetComponent<BarControl>().TitleContentStr = titleContentStr[Random.Range(0,5)];
//			g.GetComponent<BarControl>().Value = dayValue;
			g.GetComponent<BarControl>().UpdateBar(dayValue,titleStr[Random.Range(0,999)%5],titleContentStr[Random.Range(0,999)%5]);
		}
	}
	/*
	void Start () {
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		for (int i = 0; i < count; i++, start_y -= per_y_range) {

			//..
			for(int j=0;j<7;j++)
				dayValue[j] = Random.Range(0,2);

			GameObject g = Instantiate (barControl) as GameObject;
			g.transform.parent = sv;
			g.transform.localPosition = new Vector3 (start_x, start_y);
			g.transform.localScale = new Vector3 (scale_x, scale_y);
			g.GetComponent<BarControl>().TitleStr = titleStr[Random.Range(0,4)];
			g.GetComponent<BarControl>().TitleContentStr = titleContentStr[Random.Range(0,4)];
			g.GetComponent<BarControl>().Value = dayValue;
		}
	}
	 */

	void Update () {	
	}

	// Custom Methods ======================================================================================================================================

	void ButtonExit(GameObject obj){
		Application.LoadLevel ("MainMenu");
	}
}
