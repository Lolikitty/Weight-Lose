using UnityEngine;
using System.Collections;
using System;

public class LwAchievements : MonoBehaviour {

	public GameObject buttonExit;
	public GameObject barControl;
	public Transform sv;
	public UILabel totalGradeLabel;

	//BarControl處理
	float start_x = -130.0f; //起始x座標
	float start_y = 260.0f; //起始y座標
	float per_y_range = 130.0f; //間距
	float scale_x = 1.35182f ,scale_y = 1.837994f; //縮放倍數
	public int count; //數量，預設為9
	public int[] dayValue; //預設為[7]，每天的成就狀態，對應BlueMark，RedMark，GrayMark。
	public int strSize;
	public string[] titleStr;
	public string[] titleContentStr; 

	//任務處理
	public int totalGrade; //總分，預設為0
	public int[] hardLevel; //預設為[9]，當前難度等級
	public int[] maxHardLevel;//預設為[9]，各任務最大歷史紀錄的難度等級
	public int[] recordDay; //預設為[9]，各別任務當前成功達成的總天數
	public int perBuyRange; //預設為4200，一個禮拜全部成就皆達成的分數
	public int nowBuyPrice; //預設為4200，只會上升不會下降。
	public int buyCount; //購買教練次數，預設為0
	public int maxLevel; //預設為7，對應一周天數

	//每天登入
	public int[] thisWeekDay; //這一周 [7] ，是否有登入，0=未登入，1=已登入
	public int maxWeekCount; //最大連續登入的周數的紀錄。預設為0
	public int currentWeekCount; //現在連續登入的週數。預設為0

	// Unity Override Methods ==============================================================================================================================
	void Awake(){
		UIEventListener.Get(buttonExit).onClick = ButtonExit;
		//顯示成就
	

		// 第五項登入紀錄 ***
		//放在登入後的頁面，把資料回傳至這個LwAchievements.cs
		//登入天數的處理
		int index = Int32.Parse (DateTime.Now.DayOfWeek.ToString ("d")); //取得今天星期，日=0，一=1，二=2，...
		int thisWeekLoginCount = 0;
		if (thisWeekDay [index] != 1) //登入當天為1
				thisWeekDay [index] = 1;
		for (int i=0; i<maxLevel; i++) {
			switch(thisWeekDay[i]){
			case 1:
				thisWeekLoginCount ++;
				break;
			}
		}

		//更新現在的連續登入的周數，及最大的連續登入的周數
		if (thisWeekLoginCount >= maxLevel)//判斷是否一周每天都登入
			currentWeekCount++; //成功則累計
		else
			currentWeekCount = 0; //失敗則重置
		maxWeekCount = currentWeekCount > maxWeekCount ? currentWeekCount : maxWeekCount ; //最大紀錄數更新

		//---

		//獎勵等級RewardLevel
		for (int i = 0,rewardLevel; i < count; i++, start_y -= per_y_range) {
			rewardLevel = 0;
			for(int j=0;j<7;j++){
				dayValue[j] = UnityEngine.Random.Range(0,999)%3;
				//print (dayValue[j]);
				switch(dayValue[j]){ //判斷獎勵等級
				case 1:
					recordDay[i]++;
					rewardLevel++;
					break;
				}
			}
			//print("recordDay[" + i + "]=" + recordDay[i]);

			GameObject g = Instantiate (barControl) as GameObject;
			g.transform.parent = sv;
			g.transform.localPosition = new Vector3 (start_x, start_y);
			g.transform.localScale = new Vector3 (scale_x, scale_y);
			//更新狀態
			g.GetComponent<BarControl>().UpdateBar(dayValue,titleStr[UnityEngine.Random.Range(0,999)%5],titleContentStr[UnityEngine.Random.Range(0,999)%5]);



			//計算當前任務得分數
			switch(i){
			case 1:case 4:case 9: //第1.4.9項為100分累加
				totalGrade += (rewardLevel*100);
				break;
			case 2:case 3:case 5:case 6:case 7:case 8://第2.3.5.6.7.8項為50分累加
				totalGrade += (rewardLevel*50);
				break;
			}

			//更新當前難度等級，和最大歷史紀錄的難度等級。
			if(rewardLevel >= maxLevel)
				hardLevel[i]++;
			else
				hardLevel[i] = 0;//只要失敗一次，當前紀錄就重置為0
			maxHardLevel[i] = (hardLevel[i] > maxHardLevel[i]) ? hardLevel[i] : maxHardLevel[i]; //更新最大歷史紀錄
		}

		//在迴圈之外
		//更新現在購買教練的價位，對應購買次數，且限制最大價位為perBuyRange * 10
		nowBuyPrice = (perBuyRange * (buyCount + 1)) > (perBuyRange * 10)?(perBuyRange * 10):(perBuyRange * (buyCount+1));


		//更新totalGradeLabel
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
