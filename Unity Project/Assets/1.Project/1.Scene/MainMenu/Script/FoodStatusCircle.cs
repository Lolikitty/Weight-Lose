using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class FoodStatusCircle : MonoBehaviour {

	public Transform pin;
	public GameObject food;
	public GameObject root;
	public UILabel kal_num = null;
	public UITexture [] love;
	public Texture2D blackLove;

	public static int kalNow;

	// Unity Override Methods ==============================================================================================================================

	void Awake () {

		string JsonFoodDataPath = Application.persistentDataPath + "/Food.txt";
		
		if(!File.Exists(JsonFoodDataPath)){
			return;
		}

		path = JsonFoodDataPath;

		int AllKal = 0;

		JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonFoodDataPath)) ["Food"] as JArray;

		for(int i = 0; i < ja.Count; i++){
			DateTime dt = (DateTime) ja[i]["Date"];
			if(dt.Date == DateTime.Today){
				AllKal+=int.Parse(ja[i]["Kal"].ToString());
				msg += ja[i]["Kal"].ToString() + ", ";
			}
		}

		float az = 0;

		List<float> angles = new List<float>();

		for(int i = 0; i < ja.Count; i++){
			DateTime dt = (DateTime) ja[i]["Date"];
			if(dt.Date == DateTime.Today){
				int kal = int.Parse(ja[i]["Kal"].ToString());
				string foodName = ja[i]["Name"].ToString();
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
		}

		int k = 0;

		for(int i = 0; i < ja.Count; i++){
			DateTime dt = (DateTime) ja[i]["Date"];
			if(dt.Date == DateTime.Today){
				int kal = int.Parse(ja[i]["Kal"].ToString());
				string foodName = ja[i]["Name"].ToString();

				GameObject f = Instantiate(food) as GameObject;
				f.GetComponent<Food>().foodName.text = foodName;
				f.GetComponent<Food>().foodKal.text = "" + kal + " kal";
				f.transform.parent = root.transform;
				f.transform.localScale = Vector3.one;
				f.transform.localPosition = Vector3.zero;
				if(k == 0){
					f.transform.localEulerAngles = new Vector3(0, 0, angles[0]%360 + ((360-angles[0]%360)/2));
				}else if(k == angles.Count-1){
					f.transform.localEulerAngles = new Vector3(0, 0, angles[k]%360 + ((angles[k-1]%360-0%360)/2));
				}else{
					f.transform.localEulerAngles = new Vector3(0, 0, angles[k]%360 + ((angles[k-1]%360-angles[k]%360)/2));
				}
				k++;
			}
		}
		
		float kalInit = 2000;
		kal_num.text = "" + (kalInit-AllKal);
		float loveN = 200;

		kalNow = Convert.ToInt32(kalInit - AllKal);
		
		for(int i=0; i<love.Length; i++){
			if((kalInit-AllKal) < loveN * -i){
				love[i].mainTexture = blackLove;
			}
		}
	}
	
	// Custom Methods ======================================================================================================================================

	string msg = "Msg : ";
	string path = "Path : ";

	void OnGUI () {
		GUILayout.Label (path);
		GUILayout.Label (msg);
	}
}
