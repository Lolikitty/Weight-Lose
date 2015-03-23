using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ChooseFood : MonoBehaviour {

	public GameObject bg;
	public JArray foods;
	public GameObject diyAdd;
	public UIScrollView fsv2;
	public GameObject food2;

	public static List<GameObject> bgs = new List<GameObject>();

	void Awake () {
		bgs.Add (bg);
		bg.SetActive (false);
	}

	void OnClick () {

		ChooseFood2.bgs.Clear ();

		ShowBG ();
		ShowFood ();

		diyAdd.SetActive (false);
		fsv2.gameObject.SetActive (true);
	}

	void ShowBG(){
		foreach(GameObject g in bgs){
			g.SetActive(false);
		}
		bg.SetActive (true);
	}

	void ShowFood(){
		for(int i = 0; i < fsv2.transform.childCount; i++){
			Destroy(fsv2.transform.GetChild(i).gameObject);
		}

		for(int i = 0, y = 150; i < foods.Count; i++, y -= 70){
			GameObject g = Instantiate(food2) as GameObject;
			g.transform.parent = fsv2.transform;
			g.transform.localScale = Vector3.one;
			g.transform.localPosition = new Vector3(22, y);
			g.GetComponent<UILabel>().text = foods[i]["Name"].ToString();
		}

		fsv2.OnScrollBar ();
	}
}
