using UnityEngine;
using System.Collections;

public class UserLoadAnimation : MonoBehaviour {

	public UILabel txt;

	void Start(){
		InvokeRepeating ("RunSec", 0, 0.2f);
	}

	string [] arr = {"載入中","載入中.","載入中..","載入中...", "請稍候","請稍候.","請稍候..","請稍候..."};

	int i = 0;

	void RunSec () {
		txt.text = arr[i];
		i++;
		if(i >= arr.Length){
			i = 0;
		}
	}

	void Update () {
		
	}
}
