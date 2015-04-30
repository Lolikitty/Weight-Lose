using UnityEngine;
using System.Collections;
using System;

public class test : MonoBehaviour {

	public ChooseNumberNGUI t1;
	public ChooseNumberNGUI t2;
	public ChooseNumberNGUI t3;

	public GameObject ok;

	int i = 0;


	void Start(){

		UIEventListener.Get (ok).onClick = ok_test;




	}

	void ok_test(GameObject ok){

		t1.Set_number (5 + i);
		t2.Set_number (4 + i);
		t3.Set_number (3 + i);
		i += 1;
	}

}
