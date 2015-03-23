using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class SaveButton : MonoBehaviour {


	public ChooseNumber M1;
	public ChooseNumber M2;
	public ChooseNumber M3;

	SaveSports ss;
	// Use this for initialization
	void Start () {
		ss = new SaveSports ();
	}

	void OnClick(){

		int m = GetSportItemMinute ();
		string item = GetSportItemName ();

		ss.Save (DateTime.Now,item,m);
	}

	public int GetSportItemMinute() {
		
		
		float m1 = M1.chooseNumber;
		float m2 = M2.chooseNumber;
		float m3 = M3.chooseNumber;
		string m = (m1 * 100 + m2 * 10 + m3).ToString();
		
		int mintue = 0;
		mintue = Int32.Parse(m);
		
		return mintue;
	}

	public string GetSportItemName(){
		
		string str = "0";
		switch(LwSports2.nowSelectItem.ToString()){
		case "1(Clone)":
			str = "sport1";
			break;
		case "2(Clone)":
			str = "sport2";
			break;
		case "3(Clone)":
			str = "sport3";
			break;
		case "4(Clone)":
			str = "sport4";
			break;
		case "5(Clone)":
			str = "sport5";
			break;
		case "6(Clone)":
			str = "sport6";
			break;
		case "7(Clone)":
			str = "sport7";
			break;
		case "8(Clone)":
			str = "sport8";
			break;
		case "9(Clone)":
			str = "sport9";
			break;
		case "10(Clone)":
			str = "sport10";
			break;
		case "11(Clone)":
			str = "sport11";
			break;
		}
		
		return str;
	}
}
