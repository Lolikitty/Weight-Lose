using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class FoodStatusCircle : MonoBehaviour {

	public Transform pin;
	public GameObject food;
	public GameObject root;
	public UILabel kal_num = null;
	public UITexture [] love;
	public Texture2D blackLove;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {
		string nowDate = DateTime.Now.ToString ("yyyy-MM-dd");
		string path= Application.persistentDataPath + "/" + nowDate;
		
		if(!Directory.Exists(path)){
			return;
		}

		string [] filesInfo = Directory.GetFiles (path, "*.info");
		int AllKal = 0;
		
		for(int i = 0; i<filesInfo.Length; i++){
			int kal = int.Parse(filesInfo[i].Split('_')[1]);
			AllKal += kal;
		}
		
		float az = 0;

		List<float> angles = new List<float>();

		for(int i = 0; i<filesInfo.Length; i++){
			string [] foodInfoArray = filesInfo[i].Split('_');
			int kal = int.Parse(foodInfoArray[1]);
			string foodName = foodInfoArray[2];
			// Create Pin Prefab
			Transform t = Instantiate(pin) as Transform;
			t.transform.parent = root.transform;
			t.localScale = Vector3.one;
			t.localPosition = Vector3.zero;
			float z = 360-(((float) kal / (float)AllKal) * 360);
			az +=z;
			t.localEulerAngles = new Vector3(0, 0, az);
			angles.Add(az);
		}

		for(int i = 0; i<filesInfo.Length; i++){
			string [] foodInfoArray = filesInfo[i].Split('_');
			int kal = int.Parse(foodInfoArray[1]);
			string foodName = foodInfoArray[2];

			GameObject f = Instantiate(food) as GameObject;
			f.GetComponent<Food>().foodName.text = foodName;
			f.GetComponent<Food>().foodKal.text = "" + kal + " kal";
			f.transform.parent = root.transform;
			f.transform.localScale = Vector3.one;
			f.transform.localPosition = Vector3.zero;
			if(i == 0){
				f.transform.localEulerAngles = new Vector3(0, 0, angles[0]%360 + ((360-angles[0]%360)/2));
			}else if(i == angles.Count-1){
				f.transform.localEulerAngles = new Vector3(0, 0, angles[i]%360 + ((angles[i-1]%360-0%360)/2));
			}else{
				f.transform.localEulerAngles = new Vector3(0, 0, angles[i]%360 + ((angles[i-1]%360-angles[i]%360)/2));
			}
		}
		
		float kalInit = 2000;
		kal_num.text = "" + (kalInit-AllKal);
		float loveN = 200;
		
		for(int i=0; i<love.Length; i++){
			if((kalInit-AllKal) < loveN * -i){
				love[i].mainTexture = blackLove;
			}
		}
	}
	
	// Custom Methods ======================================================================================================================================

	void Update () {
	
	}
}
