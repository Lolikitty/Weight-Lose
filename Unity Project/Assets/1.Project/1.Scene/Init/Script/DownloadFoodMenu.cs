using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DownloadFoodMenu : MonoBehaviour {

	IEnumerator Start(){
		
		using (WWW w = new WWW (LwInit.HttpServerPath + "/FoodMenu.txt")) {
			yield return w;
			string JsonFoodDataPath = Application.persistentDataPath + "/FoodMenu.txt";
			File.WriteAllBytes(JsonFoodDataPath, w.bytes);
		}
	}



//	string [] foodName1 = {"蔥油餅","油條","豆漿","蛋餅","飯糰","荷包蛋","包子","刈包","饅頭","燒餅","小籠包","燒賣"};
//	string [] foodKal1 = {"500","400","350","450","550","300","350","450","300","350","600","600"};
//	string [] foodName2 = {"鬆餅","牛奶","麥片","吐司"};
//	string [] foodKal2 = {"500","400","350","450"};
//	string [] foodName3 = {"牛肉麵"};
//	string [] foodKal3 = {"900"};
//	string [] foodName4 = {"麥當勞"};
//	string [] foodKal4 = {"900"};
//	string [] foodName5 = {"港式飲茶"};
//	string [] foodKal5 = {"700"};
//	string [] foodName6 = {"義大利麵"};
//	string[] foodKal6 = {"800"};
//	string [] foodName7 = {"麥芽糖"};
//	string [] foodKal7 = {"300"};
//	
//	
//	string [] foodName8 = {"巧克力","紅茶","咖啡","藍山","曼特寧","巴西","卡布奇諾","拿鐵"};
//	string [] foodKal8 = {"600","300","350","300","400","450","350","500"};
//	
//	string [] foodKind = {"中式早餐","西式早餐","中式午餐","西式午餐","中式晚餐","西式晚餐","中式點心","西式點心"};
//	List<string[]> food_list = new List<string[]>();
//	List<string[]> kal_list = new List<string[]>();
//	
//	void JsonFoodInit(){
//		string JsonFoodDataPath = Application.persistentDataPath + "/FoodMenu.txt";
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
//			
//			
//			
//			Food = (JsonConvert.DeserializeObject<JObject> (File.ReadAllText(JsonFoodDataPath))["Food"] as JArray).ToObject<List<object>>();
//			
//			for(int i = 0 ; i < 8 ; i++){
//				
//				FoodTime foodTime = new FoodTime();
//				
//				foodTime.Name = foodKind[i];
//				
//				for(int j = 0; j<food_list[i].Length;j++){
//					
//					Foods foods = new Foods();
//					
//					foods.Name = food_list[i][j];
//					
//					foods.Kal = kal_list[i][j];
//					
//					foods.JPGPath = "/";
//					
//					foods.PNGPath = "/";
//					
//					
//					foodTime.Foods.Add(foods);
//					
//				}
//				Food.Add(foodTime);
//				
//				File.WriteAllText(JsonFoodDataPath, JsonConvert.SerializeObject(new{Food},Formatting.Indented));
//				
//				//				Debug.Log(i);
//			}
//			
//			
//		}
//	}

}



//class FoodTime{
//	
//	public string Name ;
//	public List<Foods> Foods = new List<Foods>();
//	
//	
//	
//}
//
//class Foods{
//	public string Name;
//	public string Kal;
//	public string JPGPath;
//	public string PNGPath;
//	
//}



	