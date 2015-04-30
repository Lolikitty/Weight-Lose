using UnityEngine;
using System.Collections;

public class ChangeCup : MonoBehaviour {

	public UITexture[] img;
	static int imgIndex;

	public UILabel cc;

	void OnClick(){
		if (name == "ButtonR") {
			imgIndex ++;
			imgIndex %= img.Length;
		} else {
			imgIndex -- ;
			if(imgIndex < 0)imgIndex = img.Length - 1;
		}
		ShowImg (imgIndex);
	}

	void ShowImg(int index){
		for (int i=0; i < img.Length; i++) {
			img [i].gameObject.SetActive(false);
		}
		img [index].gameObject.SetActive (true);
		int L = 200 * (index + 1);
		LwWaiter2.UseCupCC = L;
		cc.text = L.ToString ();

//		switch (index) {
//		case 0:
//			LwWaiter2.UseCupCC = 200;
//			cc.text = "200";
//			img [0].gameObject.SetActive (true);
//
//			break;
//		case 1:
//			LwWaiter2.UseCupCC = 500;
//			cc.text = "500";
//			img [0].gameObject.SetActive (false);
//			img [1].gameObject.SetActive (true);
//			img [2].gameObject.SetActive (false);
//			break;
//		case 2:
//			LwWaiter2.UseCupCC = 1000;
//			cc.text = "1000";
//			img [0].gameObject.SetActive (false);
//			img [1].gameObject.SetActive (false);
//			img [2].gameObject.SetActive (true);
//			break;
//		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
