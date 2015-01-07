using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public UILabel foodName;
	public UILabel foodKal;

	// Unity Override Methods ==============================================================================================================================

	void Start () {
		foreach(Transform t in GetComponentsInChildren<Transform>()){
			if(t.name == "Root"){
				t.transform.eulerAngles = Vector3.zero;
				break;
			}
		}
	}

	void Update () {
	
	}

	// Custom Methods ======================================================================================================================================



}
