using UnityEngine;
using System.Collections;

public class SelectButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnClick(){
		LwSports2.nowSelectItem = name ; //現在選擇的項目
		//print (name);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
