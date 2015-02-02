using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	public GameObject clock;
	public GameObject password;

	public GameObject Sw_alarm;
	public GameObject Sw_food;
	public GameObject Sw_password;

	public UISprite Bg_alarm;
	public UISprite Bg_food;
	public UISprite Bg_password;






	void Start () {
		UIEventListener.Get (clock).onClick = Clock_click;
		UIEventListener.Get (password).onClick = Password_click;

		UIEventListener.Get (Sw_alarm).onDrag = Sw_drag;
		UIEventListener.Get (Sw_food).onDrag = Sw_drag;
		UIEventListener.Get (Sw_password).onDrag = Sw_drag;
	}


	void Clock_click(GameObject obj){
		Application.LoadLevel ("Water_clock");

	}

	void Password_click(GameObject obj){

		Application.LoadLevel ("SetPassword");

	}

	//82 107

	void Sw_drag(GameObject obj , Vector2 delta){

		if(obj.transform.localPosition.x >= 82 || obj.transform.localPosition.x <= 107){

			obj.transform.localPosition += new Vector3 (delta.x, 0 , 0);



		}




	}

}
