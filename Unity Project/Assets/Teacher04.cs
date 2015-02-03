using UnityEngine;
using System.Collections;

public class Teacher04 : MonoBehaviour {

	public GameObject buy_now;

	public GameObject e_buy;

	public GameObject buy_success;

	public UILabel number;
	public UILabel now;
	public UILabel coach_number;
	public UILabel times;
	
	// Use this for initialization
	void Start () {
		
		UIEventListener.Get (buy_now).onClick = Buy_now;
		UIEventListener.Get (e_buy).onClick = E_buy;
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Buy_now(GameObject obj){
		buy_success.transform.localPosition = new Vector3 (0,0,0);

		
	}

	void E_buy(GameObject obj){
		
		buy_success.transform.localPosition = new Vector3 (600, 0, 0);
		
	}
}
