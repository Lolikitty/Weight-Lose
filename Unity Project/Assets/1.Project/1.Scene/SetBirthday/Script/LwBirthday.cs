using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

public class LwBirthday : MonoBehaviour {

	public GameObject buttonOk;
	public GameObject buttonBoy;
	public GameObject buttonGirl;

	//*原本*
	public ChooseNumber  Y1, Y2, Y3, Y4, M1, M2, D1, D2;
	//*-*

	/*
	//*修改*
	public ChooseNumber  Y2, Y3, Y4, M1, M2, D1, D2;
	public ChooseNumberY1  Y1;

	public ChooseNumberD1 D1;
	public ChooseNumberD2 D2;
	public ChooseNumberM1 M1;
	public ChooseNumberM2 M2;
	public ChooseNumberY1 Y1;
	public ChooseNumberY2 Y2;
	public ChooseNumberY3 Y3;
	public ChooseNumberY4 Y4;
	//*-*
	*/

	int yyyy;
	int MM;
	int dd;

	string sex = "Boy";

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
		UIEventListener.Get(buttonBoy).onClick = ButtonBoy;
		UIEventListener.Get(buttonGirl).onClick = ButtonGirl;

	}

	// Custom Methods ======================================================================================================================================

	void ButtonBoy(GameObject button){
		buttonBoy.GetComponent<UIButton> ().defaultColor = Color.white;
		buttonGirl.GetComponent<UIButton> ().defaultColor = Color.gray;
		sex = "Boy";
	}

	void ButtonGirl(GameObject button){
		buttonBoy.GetComponent<UIButton> ().defaultColor = Color.gray;
		buttonGirl.GetComponent<UIButton> ().defaultColor = Color.white;
		sex = "Girl";
	}

	void ButtonOk(GameObject button){

		yyyy = int.Parse("" + Y1.chooseNumber + Y2.chooseNumber + Y3.chooseNumber + Y4.chooseNumber);
		MM = int.Parse("" + M1.chooseNumber + M2.chooseNumber);
		dd = int.Parse("" + D1.chooseNumber + D2.chooseNumber);

		print ("yyyy = " + yyyy);

		// 之後這裡要做檢查

		PlayerPrefs.SetString ("Sex", "" + sex);
		PlayerPrefs.SetString ("BirthdayYear", "" + yyyy);
		PlayerPrefs.SetString ("BirthdayMonth", "" + MM);
		PlayerPrefs.SetString ("BirthdayDay", "" + dd);
		PlayerPrefs.Save ();

		LwMainMenu.UploadBirthdayAndWeight = true;
		Application.LoadLevel ("SetWeight");
	}
	
}
