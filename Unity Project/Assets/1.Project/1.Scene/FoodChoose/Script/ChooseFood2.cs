using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseFood2 : MonoBehaviour {

	public GameObject bg;
	public GameObject foodProposal;


	public static List<GameObject> bgs = new List<GameObject>();

	public static string CHOOSE_FOOD;
	public static string JPG_PATH = "";

	public int kal = 0;
	public string jpgPath = "";

	void Awake () {
		bgs.Add (bg);
		bg.SetActive (false);
		foodProposal.SetActive(false);
	}

	void Start(){
		if(PlayerPrefs.GetString("go_food") == "true"){
			if(kal < LwFoodChoose.TODAY_SURPLUS_KAL){
				foodProposal.SetActive(true);
			}
		}
	}

	void OnClick () {
		ShowBG ();
		CHOOSE_FOOD = GetComponent<UILabel>().text;
		JPG_PATH = jpgPath;
	}

	void ShowBG(){
		foreach(GameObject g in bgs){
			if(g != null){
				g.SetActive(false);
			}
		}
		bg.SetActive (true);
	}

}
