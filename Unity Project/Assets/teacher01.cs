using UnityEngine;
using System.Collections;

public class teacher01 : MonoBehaviour {

	public GameObject cancel;
	public GameObject agree;
	
	// Use this for initialization
	void Start () {

		UIEventListener.Get (cancel).onClick = Cancel;
		UIEventListener.Get (agree).onClick = Agree;



	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Cancel(GameObject obj){

		Application.LoadLevel ("User");

	}

	void Agree(GameObject obj){

		Application.LoadLevel ("Teacher02");

	}

}
