using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class LwAchievements : MonoBehaviour {
	
	public GameObject buttonExit;
	public GameObject barControl;
	public Transform sv;
	public UILabel totalGradeLabel;
	
	//BarControl??
	float start_x = -130.0f; //??x??
	float start_y = 240.0f; //??y??
	float per_y_range = 130.0f; //??
	float scale_x = 1.35182f ,scale_y = 1.837994f; //????
	public int itemCount = 9; //??,???9
	public int[] dayValue; //???[7] ???????,???????,??BlueMark,RedMark,GrayMark?
	public int strSize;
	public string[] titleStr;
	public string[] titleContentStr; 
	
	//????
	public int totalGrade; //??,???0
	public int[] hardLevel; //???[9],??????
	public int[] maxHardLevel;//???[9],??????????????
	public int[] recordDay; //???[9],????? ?????????
	public int perBuyRange; //???4200,??????????????
	public int nowBuyPrice; //???4200,?????????
	public int buyCount; //??????,???0
	public int maxLevel; //???7,??????
	
	//????
	public int[] thisWeekDay; //??? [7] ,?????,0=???,1=???
	public int maxWeekCount; //????????????????0
	public int currentWeekCount; //?????????????0
	
	
	DateTime today; ///??????
	DateTime thisWeekStartDate; //??? ???
	DateTime thisWeekEndDate; //??? ???
	
	// Unity Override Methods ==============================================================================================================================
	void Awake(){
		UIEventListener.Get(buttonExit).onClick = ButtonExit;

		int standardCount = 7;
		//titleStr = new string[9];
		dayValue = new int[7];

		today = DateTime.Now;

		switch(today.DayOfWeek.ToString()){ //???? ??? ? ???
		case "Monday":
			thisWeekStartDate = today.Date;
			thisWeekEndDate = today.AddDays(6).Date;
	
			break;
		case "Tuesday":
			thisWeekStartDate = today.AddDays(-1).Date;
			thisWeekEndDate = today.AddDays(5).Date;

			break;
		case "Wednesday":
			thisWeekStartDate = today.AddDays(-2).Date;
			thisWeekEndDate = today.AddDays(4).Date;		

			break;
		case "Thursday":
			thisWeekStartDate = today.AddDays(-3).Date;
			thisWeekEndDate = today.AddDays(3).Date;		
			break;
		case "Friday":
			thisWeekStartDate = today.AddDays(-4).Date;
			thisWeekEndDate = today.AddDays(2).Date;

			break;
		case "Saturday":
			thisWeekStartDate = today.AddDays(-5).Date;
			thisWeekEndDate = today.AddDays(1).Date;

			break;
		case "Sunday":
			thisWeekStartDate = today.AddDays(-6).Date;
			thisWeekEndDate = today.Date;

			break;
		}
		
		
		int missionGrade = 0;
		//????RewardLevel //??? i+1
		UpdateMission update = new UpdateMission();
		for (int i = 0,rewardLevel,j; i < itemCount; i++, start_y -= per_y_range) {
//			for(int b=0;b<7;b++){ //reset dayValue
//				dayValue[b] = 0;
//			}
			missionGrade = 0; //reset
			rewardLevel = 0; 
			j=0;

			for(int k = 0 ;k <= 6 ;k++){ //reset
				dayValue[k] = 0;
			}

//			UpdateMission update = new UpdateMission();
			missionGrade = update.updateMission(i+1);//??MaxLv NowLv ,???????
			totalGrade += missionGrade ; //????????

			//Debug.Log ("i ="+i+"...missionGrade =" + missionGrade);

			switch(i+1){
			case 6:case 7:case 8:
//				Debug.Log("678 MissionItem = " + (i+1));
				SaveMissionItemCount smic = new SaveMissionItemCount();
				int missionItemCount =  smic.GetMissionItemReachCount(i+1);
//				Debug.Log("missionItemCount = " + missionItemCount);
				if(missionItemCount != 0){
					int tmp = missionItemCount % standardCount;
//					Debug.Log("tmp = " + tmp);
					if(tmp == 0){					
//						Debug.Log("missionItemCount == 0");
						for(int m = 0;m < standardCount ; m++){
							dayValue[m] = 1;
						}
					}else{		
//						Debug.Log("tmp = " + tmp );
						for(int n = 0;n < tmp; n++){
							dayValue[n] = 1;
//							Debug.Log("n = " + n);
						}
					}
				}

				break;
			case 1:case 2:case 3:case 4:case 5:case 9:
//				Debug.Log("MissionItem = " + (i+1));
				bool dayGoal = false;
				for(DateTime d = thisWeekStartDate; d.Date <= today.Date; j++,d = d.AddDays(1) ){ //???????? thisWeekStartDate ~ today
//					print("d = " + d.Date);
//					print ("i = " + i);
					switch(i+1){
					case 1:
						dayGoal = new M1DayGoal().TheDayGoal(d);
						break;
					case 2:
						dayGoal = new M2DayGoal().TheDayGoal(d);
						break;
					case 3:
						dayGoal = new M3DayGoal().TheDayGoal(d);
						break;
					case 4:
						dayGoal = new M4DayGoal().TheDayGoal(d);
						break;
					case 5:
						dayGoal = new M5DayGoal().TheDayGoal(d);
						break;
					case 9:
						dayGoal = new M9DayGoal().TheDayGoal(d);
						break;
					}
//					bool dayGoal = new M1DayGoal().TheDayGoal(d);
					dayValue[j] = (dayGoal?1:2); //0?? 1?? 2??
					//print("dayValue[j] j=" + j);
					//print (dayValue[j]);

					switch(dayValue[j]){ //??????
					case 1:
						recordDay[i] ++; //????? ???????
						rewardLevel ++;
						break;
					}
				} 

				break;
			}

			
			GameObject g = Instantiate (barControl) as GameObject;
			g.transform.parent = sv;
			g.transform.localPosition = new Vector3 (start_x, start_y);
			g.transform.localScale = new Vector3 (scale_x, scale_y);
			//????


			string TitleTmp = titleStr[i];

			int nowLv = new SaveMissionLv().GetMissionNowLv(i+1);



			//print(TitleTmp);

//			if(i==3)
//				for(int x=0;x<7;x++)
//					Debug.Log("daValue["+x+"]="+dayValue[x]);
					//dayValue[x] = 0;
//			if(i==3){
			//g.GetComponent<BarControl>().UpdateBar(dayValue,TitleTmp,TitleTmp,nowLv);
//			}
//			if(i==5 || i==6 || i==7)
//				for(int w=0;w<7;w++)
//					Debug.Log("Item = " + (i+1) +"dayValue =" + dayValue[w]);
//
			DateTime lastWeekStartDate = thisWeekStartDate.AddDays(-7);
			DateTime lastWeekEndDate = thisWeekEndDate.AddDays(-7);

			SaveLvUpdateHistory h = new SaveLvUpdateHistory();

//			Debug.Log("i = " + (i+1) + ", lastWeekStartDate =" + lastWeekStartDate);
			bool isLvUpdate = h.IsUpdateAddLv(i+1 ,lastWeekStartDate ,lastWeekEndDate);

			int extraGrade = 0;
			int baseGrade = 100;

			if(isLvUpdate){ // Get MissionN's Grade
				SaveMissionLv sml = new SaveMissionLv();
				extraGrade = sml.GetMissionNowLv(i+1) * baseGrade;
			}

			g.GetComponent<BarControl>().UpdateBar(dayValue,TitleTmp,TitleTmp,nowLv,isLvUpdate,extraGrade);
		}
		

		totalGradeLabel.text = totalGrade.ToString();
		

		/*
		print ("totalGrade = " + totalGrade);
		print ("nowBuyPrice = " + nowBuyPrice);
		print ("buyCount = " + buyCount);
		*/
	}

	
	void Update () {	
	}
	
	// Custom Methods ======================================================================================================================================
	
	void ButtonExit(GameObject obj){
		Application.LoadLevel ("MainMenu");
	}
}
