using UnityEngine;
using System.Collections;

public class FirstSelectCup : MonoBehaviour {
	
	void OnClick(){
		PlayerPrefs.SetInt("FirstWater",1);
	}
}
