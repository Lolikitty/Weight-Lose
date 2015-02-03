﻿using UnityEngine;
using System.Collections;

public class LwWeightMeasurements : MonoBehaviour {

	public GameObject buttonOk;

	public ChooseNumber w1, w2, w3, w4;

	public static float NowWeight;

	public static float [] weight = new float[10];

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
	}

	// Custom Methods ======================================================================================================================================

	void ButtonOk(GameObject button){

		bool isFirst = true;


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

		NowWeight = float.Parse ("" + w1.chooseNumber + w2.chooseNumber + w3.chooseNumber + "." + w4.chooseNumber);

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

		Application.LoadLevel ("WeightMeasurements2");
	}

}










