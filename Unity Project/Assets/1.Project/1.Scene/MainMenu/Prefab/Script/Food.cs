using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public UILabel foodName;
	public UILabel foodKal;

	public GameObject foodButton;

	// Unity Override Methods ==============================================================================================================================

	void Awake(){
		UIEventListener.Get (foodButton).onClick = FoodButton;
	}

	void Start () {
		foreach(Transform t in GetComponentsInChildren<Transform>()){
			if(t.name == "Root"){
				t.transform.localEulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
				break;
			}
		}
	}

	void Update () {	
	}

	// Custom Methods ======================================================================================================================================

	void FoodButton(GameObject obj){
		print ("ok");
	}

}
