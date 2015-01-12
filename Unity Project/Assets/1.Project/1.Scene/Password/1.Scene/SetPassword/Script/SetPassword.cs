using UnityEngine;
using System.Collections;

public class SetPassword : MonoBehaviour {

	public ChooseNumberNGUI cnn1;
	public ChooseNumberNGUI cnn2;
	public ChooseNumberNGUI cnn3;
	public ChooseNumberNGUI cnn4;

	public static string cnn;

	void Update () {
		cnn = "" + cnn1.chooseNumber + cnn2.chooseNumber + cnn3.chooseNumber + cnn4.chooseNumber;
	}
}
