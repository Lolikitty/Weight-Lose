using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

public class LwWeight : MonoBehaviour {

	public GameObject buttonOk;

	public ChooseNumber NW1, NW2, NW3, NW4, WW1, WW2, WW3, WW4, M1, M2;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		UIEventListener.Get(buttonOk).onClick = ButtonOk;
	}

	// Custom Methods ======================================================================================================================================
	
	string WeightFirst;
	string WeightTarget;
	string WeightTargetMonth;

	void ButtonOk(GameObject button){

		LwMainMenu.UploadBirthdayAndWeight = true;

		WeightFirst = "" + float.Parse ("" + NW1.chooseNumber + NW2.chooseNumber + NW3.chooseNumber + "." + NW4.chooseNumber);
		WeightTarget = "" + float.Parse ("" + WW1.chooseNumber + WW2.chooseNumber + WW3.chooseNumber + "." + WW4.chooseNumber);
		WeightTargetMonth = "" + int.Parse("" + M1.chooseNumber + M2.chooseNumber);

		// 之後這裡要做檢查

		PlayerPrefs.SetString ("WeightFirst", WeightFirst);
		PlayerPrefs.SetString ("WeightTarget", WeightTarget);
		PlayerPrefs.SetString ("WeightTargetMonth", WeightTargetMonth);
		PlayerPrefs.Save ();

		if(LwUserCamera.toWeight){
			Application.LoadLevel ("User2");	
		}else{
			Application.LoadLevel ("MainMenu");	
		}

	}

}
