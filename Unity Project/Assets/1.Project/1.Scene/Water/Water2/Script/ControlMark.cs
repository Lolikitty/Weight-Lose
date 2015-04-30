using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class ControlMark : MonoBehaviour {

	public UILabel show;
	public UILabel percent;

	public GameObject dw;

	// Use this for initialization
	void Start () {
		DateTime thisDay = DateTime.Now;
		string fileName = "Water.txt";
		string rootIndex = "Water"; //根索引
		
		//取得所有符合日期的項目集合
		List<JObject> set = new GetDateCollection ().getDateItem(thisDay, fileName, rootIndex);
		
		float standardLiter = 2000.0f;

		float sum = 0;
		//process
		foreach (JObject tmp in set) {
			float L = float.Parse(tmp["Liter"].ToString());
			sum += L;
		}	

		
		float w = 0;
		w = standardLiter - sum;
		if (w <= 0) {
			w = 0;
		}
		show.text = w.ToString ();



		float p = 0;
		p = sum / standardLiter * 100;
		if (p >= 100.0f) {
			p = 100.0f;
		}
		percent.text = p.ToString ("f1") + "%";

//		Debug.Log ("sum = " + sum);

		float x = sum / standardLiter;

		if (x >= 1.0f) {
			x = 1.0f;
		}
//		x = 0.5f;
		dw.GetComponent<UISlider> ().value = x;
	}
	

}
