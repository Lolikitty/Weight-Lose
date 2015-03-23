using UnityEngine;
using System.Collections;

public class InitToScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public static bool toBirthday = false;

	// Update is called once per frame
	void Update () {
		if(toBirthday){
			toBirthday = false;
			Application.LoadLevel("SetBirthday");
		}
	}
}
