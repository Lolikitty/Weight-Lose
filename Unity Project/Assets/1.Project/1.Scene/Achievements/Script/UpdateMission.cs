using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class UpdateMission : MonoBehaviour {
	
	public int updateMission(int MissionNumber){ //傳回項目總得分和更新 NowLv

		if (MissionNumber < 1 || MissionNumber > 9) {
			return 0;
		}

		int p = 1; //[1.4.9]=2 
		int standardDays = 7; 
		int standardCounts = 7; //[7]. [8] .[9]

		switch(MissionNumber){ //設定p和standardDays
		case 1:
			p=2;
			break;
		case 4:
			p=2;
			break;

		case 9:
			p=2;

			break;
		}

		int Grade = 0;
		int baseUpdateLvGrade = 100;
		int count = 0; 
		int NowLv = 1;
		int c = 0; // Mission 6 7 8 

		bool thisWeekGoal = true;

		DateTime startDate = new GetItemStartEndDate().getItemStartEndDate(MissionNumber,0);
		DateTime endDate = new GetItemStartEndDate().getItemStartEndDate(MissionNumber,1);

//		if (MissionNumber == 6 || MissionNumber == 7 || MissionNumber == 8) {
//			Debug.Log("MissionNumber = " + MissionNumber + " ,startDate = " + startDate + " ,endDate = " + endDate);
//		}
//		if (MissionNumber == 4) {
//			Debug.Log("MissionNumber = " + MissionNumber + "  startDate = " + startDate);
//			Debug.Log ("MissionNumber = " + MissionNumber + "  endDate = " + endDate);
//		}

		if(startDate <= endDate){
			for (DateTime d = startDate; d.Date <= endDate.Date ; d = d.AddDays(1)) {

//				Debug.Log ("MissionNumber = " + MissionNumber + "  d.Date = " + d.Date);

				bool theDayGoal = false;
				switch(MissionNumber){
				case 1:
					theDayGoal = new M1DayGoal().TheDayGoal(d);
					break;
				case 2:
					theDayGoal = new M2DayGoal().TheDayGoal(d); 
					break;
				case 3:
					theDayGoal = new M3DayGoal().TheDayGoal(d);
					break;
				case 4:
					theDayGoal = new M4DayGoal().TheDayGoal(d);
					break;
				case 5:
					theDayGoal = new M5DayGoal().TheDayGoal(d);
					break;
				case 6:
					theDayGoal = new M6DayGoal().TheDayGoal(d);
//					Debug.Log("theDayGoal = " + theDayGoal);
					break;
				case 7:
					theDayGoal = new M7DayGoal().TheDayGoal(d);
//					Debug.Log("theDayGoal = " + theDayGoal);
					break;
				case 8:
					theDayGoal = new M8DayGoal().TheDayGoal(d);
//					Debug.Log("theDayGoal = " + theDayGoal);
					break;
				case 9:
					theDayGoal = new M9DayGoal().TheDayGoal(d);
					break;
				}
//				if(MissionNumber == 3) 
//					Debug.Log("A");
				//Debug.Log(d.DayOfWeek.ToString());
				if((MissionNumber == 6 || MissionNumber == 7 || MissionNumber == 8) && theDayGoal){ //是否為第6 7 8項目
//					Debug.Log("MissionNumber = " + MissionNumber + "d = " + d.Date);
					int tmpLv = 1 + (c/standardCounts) ;

//					Debug.Log("tmpLv = " + tmpLv);

					c++;

//					Debug.Log("c = " + c);

//					Debug.Log("MissionNumber = " + MissionNumber + " c = " + c);

					SaveMissionItemCount s = new SaveMissionItemCount();
					s.Save(MissionNumber,c);	//存入

					MissionGrade mg = new MissionGrade();
					int baseGrade = mg.GetMissionGrade(MissionNumber);
					Grade += (baseGrade * p); //取得分數

					if(d.DayOfWeek.ToString() == "Sunday"){//檢查Lv更新

//						Debug.Log("c = " + c);


						NowLv = 1 + (c/standardCounts); 

//						Debug.Log("mn678 NowLv = " + NowLv);
//						Debug.Log("tmpLv = " + tmpLv + ", NowLv = " + NowLv);

						if(c%standardCounts == 0){ //等級改變

//							Debug.Log("tmpLv = " + tmpLv + ", NowLv = " + NowLv) ;


							int MaxLv = 10; 
							
							NowLv = (NowLv > MaxLv ? MaxLv : NowLv);

							Grade += (NowLv * baseUpdateLvGrade*7); //升級額外獎勵分數，一周
							SaveMissionLv smlv = new SaveMissionLv ();
							smlv.Save(d,MissionNumber,NowLv);
						}
					}				
				}else{
					if(d.DayOfWeek.ToString() == "Sunday"){ // 此日為禮拜日
						if(theDayGoal){ //此日達成

							MissionGrade mg = new MissionGrade();
							int baseGrade = mg.GetMissionGrade(MissionNumber);
							Grade += (baseGrade * p); //取得分數

							count++;
							if(count == standardDays && thisWeekGoal){ //周任務達成
								NowLv++;

								int MaxLv = 10; 
								
								NowLv = (NowLv > MaxLv ? MaxLv : NowLv);

								Grade += (NowLv * baseUpdateLvGrade); //升級額外獎勵分數
								SaveMissionLv smlv = new SaveMissionLv ();
								smlv.Save(d,MissionNumber,NowLv);
							}
						}
						count = 0;//重置
						thisWeekGoal = true;
						
					}else{//此日非禮拜日
						if(theDayGoal){
							MissionGrade mg = new MissionGrade();
							int baseGrade = mg.GetMissionGrade(MissionNumber);
							Grade += (baseGrade * p); //取得分數
							count++;
						}else{
							thisWeekGoal = false;
						}
					}
				}	
			}

			//存入對應項目的NowLv
//			SaveMissionLv saveItemLv = new SaveMissionLv ();
//			saveItemLv.Save (MissionNumber,NowLv);

			if(MissionNumber == 6 || MissionNumber == 7 || MissionNumber == 8){ //是否為第6 7 8項目
				SaveMissionItemCount s  = new SaveMissionItemCount();
				s.Save(MissionNumber,c);
			}
		}
		//傳回項目得分的總分
		return Grade;
	}
}