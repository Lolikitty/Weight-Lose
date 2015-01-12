using UnityEngine;
using System.Collections;
using System.IO;

public class InputPassword : MonoBehaviour {

	public ChooseNumberNGUI cnn1;
	public ChooseNumberNGUI cnn2;
	public ChooseNumberNGUI cnn3;
	public ChooseNumberNGUI cnn4;

	public GameObject buttonOk;
	
	void Start () {
		UIEventListener.Get (buttonOk).onClick = ButtonOk;
	}
	
	void ButtonOk(GameObject obj){
		string cnn = "" + cnn1.chooseNumber + cnn2.chooseNumber + cnn3.chooseNumber + cnn4.chooseNumber;

		string [] files = Directory.GetFiles (Application.persistentDataPath, "*.pw");
		string pw = Path.GetFileNameWithoutExtension (files [0]);

		if(pw == cnn){
			print (pw + "    " + cnn);
			GetComponent<ToScene>().SetClick();
		}else{
			print ("Error");
		}

	}
}
