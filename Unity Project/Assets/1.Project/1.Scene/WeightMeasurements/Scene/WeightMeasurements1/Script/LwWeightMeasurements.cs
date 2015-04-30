using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
public class LwWeightMeasurements : MonoBehaviour {

	public GameObject buttonOk;

	public ChooseNumber w1, w2, w3, w4;

	public static float NowWeight;

	public static float [] weight = new float[10];

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
	}

	void Start(){
		SaveWeight sw = new SaveWeight ();
		float w = sw.GetLastDayWeight (DateTime.Now);
		Debug.Log ("w = " + w);
		//123.4
		int ww1 = (int)w / 100;
		int ww2 = (int)w / 10 - ww1 * 10;
		int ww3 = (int)w % 10;
		int w4tmp = (int)w;
		float f = w - (float)w4tmp;
		int ww4 = (int)(f * 10);
		w1.SetNumber (ww1);
		w2.SetNumber (ww2);
		w3.SetNumber (ww3);
		w4.SetNumber (ww4);
	}

	// Custom Methods ======================================================================================================================================

	void ButtonOk(GameObject button){

		bool isFirst = true;

		/*
		for(int i = 0; i<weight.Length; i++){
			weight [i] = PlayerPrefs.GetFloat("Weight"+i);
			if(weight [i] != 0){
				isFirst = false;
			}
		}

		if(isFirst){
			for(int i = 0; i<weight.Length; i++){
				PlayerPrefs.SetFloat ("Weight"+i, 30);
				weight [i] = 30;
			}
		}
		*/
		NowWeight = float.Parse ("" + w1.chooseNumber + w2.chooseNumber + w3.chooseNumber + "." + w4.chooseNumber);
		/*
		for (int i = 0; i<weight.Length; i++) {
			int k = i + 1;
			if(k < weight.Length){
				weight [i] = weight[k];
			}else{
				weight [i] = NowWeight;
			}
			PlayerPrefs.SetFloat ("Weight"+i, weight [i]);
		}

		PlayerPrefs.Save ();
		*/
		SaveWeight sw = new SaveWeight ();
		sw.f(DateTime.Now , NowWeight);

		//Debug.Log("NowWeight = " + NowWeight.ToString() );

		Application.LoadLevel ("WeightMeasurements2");
	}

}










