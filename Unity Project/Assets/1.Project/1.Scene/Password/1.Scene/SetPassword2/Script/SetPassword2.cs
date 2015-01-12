using UnityEngine;
using System.Collections;
using System.IO;

public class SetPassword2 : MonoBehaviour {

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
		if(SetPassword.cnn == cnn){
			string [] files = Directory.GetFiles (Application.persistentDataPath, "*.pw");
			
			if(files.Length != 0){
				File.Delete(files[0]);
			}


			File.WriteAllBytes(Application.persistentDataPath + "/" + cnn + ".pw", new byte[0]);
			GetComponent<ToScene>().SetClick();
		}else{
			print ("Error");
		}
	}
}
