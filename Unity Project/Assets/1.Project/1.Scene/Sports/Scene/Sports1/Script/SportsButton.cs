using UnityEngine;
using System.Collections;

public class SportsButton : MonoBehaviour {

	public static string clickButton;


	//請在intit設定PlayerPrefs.SetString ("FavoriteSport","0")


	// Use this for initialization
	void OnClick () {
		clickButton = name;
		//Add to favorite
		//print (clickButton);
		string key = "sportItem_" + clickButton;
		PlayerPrefs.SetString(key, "1");
		PlayerPrefs.Save ();

		PlayerPrefs.SetString ("FavoriteSport","1"); //設定為已經有最愛運動項目
		PlayerPrefs.Save ();
		/*
		for (int i=1; i<12; i++) {
			string n = i.ToString();
			string k = "sportItem_" + n;
			PlayerPrefs.SetString(k, "0");
			PlayerPrefs.Save ();
		}
		*/
	}

}
