using UnityEngine;
using System.Collections;

public class LwMessage_Unit : MonoBehaviour {

	public UILabel name;
	public UITexture userImg;
	public UILabel message;



	public string NAME = "";

	void OnGUI(){
//		GUILayout.Label (name.text);
	}

	void Start(){
		name.text = NAME;
	}

}
