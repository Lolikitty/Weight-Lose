using UnityEngine;
using System.Collections;

public class LwSports2 : MonoBehaviour {

	public GameObject buttonOk;

	public Transform root;

	public GameObject[] sportButton; //[11]
	public static string nowSelectItem; //判斷現在選擇的項目


	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;

		//print("AA = "+SportsButton.clickButton);

		nowSelectItem = "0";//初始化

		//add sports item
		int x = -150; //初始位置
		for (int i = 0; i < 11; i++) {
			string key = "sportItem_" + (i+1);
			//print(PlayerPrefs.GetString(key));
			if(PlayerPrefs.GetString(key) == "1")
			{//判斷項目是否存在
				GameObject g = Instantiate (sportButton[i]) as GameObject;
				g.transform.parent = root ;
				g.transform.localPosition = new Vector3(x,50,0);
				g.transform.localScale = new Vector3(2.0f,2.0f,2.0f);//Vector3.one;
				x += 200;
			}
		}
//
//		for (int i = 0, x=50; i < 5; i++,x+=200) {
//			GameObject g = Instantiate (myBtn) as GameObject;
//			g.transform.parent = root ;
//			g.transform.localPosition = new Vector3(x,0,0);
//			g.transform.localScale = Vector3.one;
//		}
	}


	// Custom Methods ======================================================================================================================================

	void ButtonOk(GameObject button){
		Application.LoadLevel ("Sports3");
	}

}
