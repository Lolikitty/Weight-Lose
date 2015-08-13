using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LwSports2 : MonoBehaviour {

	public GameObject buttonOk;

	public Transform root;

	public GameObject[] sportButton; //[11]
	public static string nowSelectItem; //判斷現在選擇的項目

	public UICenterOnChild sv_coc;

	public ChooseNumber cn3;

	// Unity Override Methods ==============================================================================================================================

	List<string> arr = new List<string>();

	void Awake () {

		cn3.SetNumber (1);

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
				arr.Add(g.name);
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

	void Update(){
		SpringPanel sp= sv_coc.GetComponent<SpringPanel> ();
		int id = (int) Mathf.Round ((sp.target.x-150)/-200);
		nowSelectItem = arr [id];
//		print  ( arr[id] );
	}


	// Custom Methods ======================================================================================================================================

	void ButtonOk(GameObject button){
		Application.LoadLevel ("Sports2");
//		Application.LoadLevel ("Sports3");
	}

}
