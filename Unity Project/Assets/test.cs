using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class test : MonoBehaviour {

	public GameObject ok;

	string [] foodName1 = {"蔥油餅","油條","豆漿","蛋餅","飯糰","荷包蛋","包子","刈包","饅頭","燒餅","小籠包","燒賣"};
	string [] foodKal1 = {"500","400","350","450","550","300","350","450","300","350","600","600"};
	string [] foodName2 = {"鬆餅","牛奶","麥片","吐司"};
	string [] foodKal2 = {"500","400","350","450"};
	string [] foodName3 = {"牛肉麵"};
	string [] foodKal3 = {"900"};
	string [] foodName4 = {"麥當勞"};
	string [] foodKal4 = {"900"};
	string [] foodName5 = {"港式飲茶"};
	string [] foodKal5 = {"700"};
	string [] foodName6 = {"義大利麵"};
	string[] foodKal6 = {"800"};
	string [] foodName7 = {"麥芽糖"};
	string [] foodKal7 = {"300"};
	
	
	string [] foodName8 = {"巧克力","紅茶","咖啡","藍山","曼特寧","巴西","卡布奇諾","拿鐵"};
	string [] foodKal8 = {"600","300","350","300","400","450","350","500"};
	
	string [] foodKind = {"中式早餐","西式早餐","中式午餐","西式午餐","中式晚餐","西式晚餐","中式點心","西式點心"};
	List<string[]> food_list = new List<string[]>();
	List<string[]> kal_list = new List<string[]>();

	int i = 0;


	void Start(){


	//	JsonFoodSave ("ew" , food2);
		FoodGet ();

	}

	void FoodGet(){


		List<string> foods = new List<string> ();
		
		int kal_now = FoodStatusCircle.kalNow;

		string JsonFoodDataPath = Application.dataPath + "/food.txt";
		
//		string JsonFoodDataPath = imgDir + "FoodMenu.txt";
		
		JArray ja = JsonConvert.DeserializeObject<JObject> (File.ReadAllText (JsonFoodDataPath)) ["Food"] as JArray;
		
		for(int i = 0; i < ja.Count; i++){
			//


			Debug.Log((string)ja[i]["name"]);
				//if(foodName == (string)ja[i]["name"]){
			//			
						JArray Foods = (JArray)ja[i]["foods"];
						//for(int j = 0; j < Foods.Count; i++){
			//
								//string thisKal = (string)Foods[j]["kal"];


						//	Debug.Log((string)Foods[j]["kal"]);
			//
			//
								//if(Convert.ToInt32(thisKal) <= kal_now){
			//
									//foods.Add((string)Foods[j]["name"]);
			//
								
						//}
				//}
		}












		//string JsonFoodDataPath = imgDir + "/FoodMenu.txt";

//		string JsonFoodDataPath = Application.dataPath + "/food.txt";
//
//		List<object> Food = null;
//		List<object> FoodCon = null;
//		
//		if (!File.Exists (JsonFoodDataPath)) {
//			Food = new List<object> ();
//			File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
//			
//			food_list.Add(foodName1);
//			food_list.Add(foodName2);
//			food_list.Add(foodName3);
//			food_list.Add(foodName4);
//			food_list.Add(foodName5);
//			food_list.Add(foodName6);
//			food_list.Add(foodName7);
//			food_list.Add(foodName8);
//			
//			kal_list.Add(foodKal1);
//			kal_list.Add(foodKal2);
//			kal_list.Add(foodKal3);
//			kal_list.Add(foodKal4);
//			kal_list.Add(foodKal5);
//			kal_list.Add(foodKal6);
//			kal_list.Add(foodKal7);
//			kal_list.Add(foodKal8);
//			
//			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
//			
//			for(int i = 0 ; i < 8 ; i++){
//
//				FoodTime foodTime = new FoodTime();
//
//				foodTime.name = foodKind[i];
//
//				for(int j = 0; j<food_list[i].Length;j++){
//					
//					Foods foods = new Foods();
//
//					foods.name = food_list[i][j];
//
//					foods.kal = kal_list[i][j];
//
//					foods.JPGPath = "/";
//
//					foods.PNGPath = "/";
//
//
//					foodTime.foods.Add(foods);
//
//				}
//				Food.Add(foodTime);
//
//				File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
//
//				Debug.Log(i);
			}
	
			
		}
		
		
//		
//	}
//}


	


//	void JsonFoodSave(string name, string[] food2){
//		string JsonFoodDataPath = Application.dataPath + "/Food.txt";
//		
//		var food = new {
//			Name = name,
//
//			Food2 = food2
//
//		};
//		
//		List<object> Food = null;
//		
//		if(File.Exists(JsonFoodDataPath)){
//			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
//			Food.Add(food);
//		}else{
//			Food = new List<object> (){food};
//		}
//		File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
//	}


