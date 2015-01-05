using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class LwFoodHistory : MonoBehaviour {

	public GameObject buttonExit;

	public GameObject buttonL;
	public GameObject buttonR;


	public UILabel labelDate;
	public UITexture food;
	public UIScrollView sv;

	void Awake () {
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		UIEventListener.Get(buttonL).onClick = ButtonL;
		UIEventListener.Get(buttonR).onClick = ButtonR;

		month.text = monthTw[chooseMonth-1];
	}

	IEnumerator Start(){
		yield return StartCoroutine(Run());
	}

	IEnumerator Run(){
		int dateDistance = 100;
		int layerDistance = 150;
		int dateAndFoodDistance = 50;
		int FoodDistance = 130;
		//-------------------------
		int labelY = 500;
		int x = -100;
		int y = 0;
		int y2 = 0;
		int layerCount = 1;
		
		string path = null;
		
		foreach(string [] yyyyMMdd in GetYyyyMMdd ()){



			#if UNITY_EDITOR
			path = Application.persistentDataPath + "/" + yyyyMMdd [0] + "-" + yyyyMMdd [1] + "-" + yyyyMMdd [2];
			#elif UNITY_ANDROID || UNITY_IPHONE
			path = yyyyMMdd [0] + "-" + yyyyMMdd [1] + "-" + yyyyMMdd [2];
			#endif
			//				msg2 = path;
			
			if((path.IndexOf(chooseYear+"-"+(chooseMonth<10?"0":"")+chooseMonth)==-1)){
				continue;
			}
			
			string [] files = Directory.GetFiles (path, "*.png");
			string [] filesJPG = Directory.GetFiles (path, "*.jpg");
			string [] filesInfo = Directory.GetFiles (path, "*.info");
			
			int countY = 0;			
			int fl = files.Length;			
			while(fl > 0){
				fl -= 3;
				countY++;
			}			
			labelY += countY * layerDistance + dateDistance;
			
			y = labelY+dateAndFoodDistance;
			
			UILabel label = Instantiate (labelDate) as UILabel;
			label.transform.parent = sv.transform;
			label.transform.localPosition = new Vector3(-100, labelY);
			label.transform.localScale = Vector3.one;
			label.text = yyyyMMdd[1]+"/"+yyyyMMdd[2];
			
			
			
			for(int i=0; i<files.Length; i++){
				string [] fileInformation = filesInfo[i].Split('_');
				
				UITexture t = Instantiate(food) as UITexture;
				t.transform.parent = sv.transform;
				t.transform.localScale = Vector3.one;
				t.GetComponent<LwFoodHistory_Food>().pathJPG = filesJPG[i];
				t.GetComponent<LwFoodHistory_Food>().pathPNG = files[i];
				t.GetComponent<LwFoodHistory_Food>().pathInfo = filesInfo[i];
				t.GetComponent<LwFoodHistory_Food>().fileInformation = fileInformation;
				t.GetComponentInChildren<UILabel>().text = fileInformation[2];
				
				if(i%3 == 0){
					x = -FoodDistance;
					y -= layerDistance;
				}else if(i%3 == 1){
					x = 0;
				}else if(i%3 == 2){
					x = FoodDistance;
				}
				
				t.transform.localPosition = new Vector3(x, y);
				x += FoodDistance;
				
				WWW www = new WWW ("file://" + files[i]);
				yield return www;
				t.mainTexture = www.texture;
				
			}
		}
		sv.OnScrollBar ();	
	}

	string msg2 = "No";
	public GUIStyle gs;
	void OnGUI(){
//		GUILayout.Label (msg2, gs);
	}
	
	void ButtonExit(GameObject button){		
		Application.LoadLevel ("User");
	}

	public UILabel month;

	public static int chooseYear = 1;
	public static int chooseMonth = 1;
	string[] monthTw = {"一月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","十二月"};

	void ButtonL(GameObject button){
		chooseMonth--;
		if(chooseMonth<1){
			chooseMonth = 12;
			chooseYear--;
		}
		month.text = monthTw[chooseMonth-1];
		Application.LoadLevel ("FoodHistory");
	}

	void ButtonR(GameObject button){		
		chooseMonth++;
		if(chooseMonth>12){
			chooseMonth = 1;
			chooseYear++;
		}		
		month.text = monthTw[chooseMonth-1];
		Application.LoadLevel ("FoodHistory");
	}

	HashSet <string[]> GetYyyyMMdd(){
		HashSet <string[]> yyyyMMdd = new HashSet <string[]>();
		
		string dir;
		
		foreach(string s in Directory.GetDirectories(Application.persistentDataPath)){
			dir = s.Split('/')[s.Split('/').Length-1];
			dir = s.Split('\\')[s.Split('\\').Length-1];
			if(dir.IndexOf("-") != -1){
				yyyyMMdd.Add(dir.Split('-'));
			}
		}

		return yyyyMMdd;
	}



}
