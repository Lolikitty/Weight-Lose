using UnityEngine;
using System.Collections;

public class FirstSelectSportItem : MonoBehaviour {
	
	void OnClick(){
		PlayerPrefs.SetInt("FirstSports",1);

		string key = "sportItem_" + name.ToString ();
		PlayerPrefs.SetString (key,"1");//.GetString(key)
	
	}
}
