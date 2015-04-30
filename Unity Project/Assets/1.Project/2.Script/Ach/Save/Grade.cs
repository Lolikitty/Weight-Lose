using UnityEngine;
using System.Collections;

public class Grade : MonoBehaviour
{
	
//	public void Save(){
//
//	}
//
//	public void GradeUpdate(){
//
//	}

	public int GetTotalGrade(){ 
		UpdateMission update = new UpdateMission();
		int sum = 0;
		for (int i=1; i <= 9; i++) {
			int tmp = update.updateMission(i);
			sum += tmp ;
		}

		SaveCost sc = new SaveCost ();
		int cost = sc.GetTotalCost();

		sum -= cost;

		if (sum <= 0) {
			sum  = 0;
		}
		return sum;
	}
}