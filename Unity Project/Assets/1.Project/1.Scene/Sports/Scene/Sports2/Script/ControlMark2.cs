using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class ControlMark2 : MonoBehaviour {
	
	public UILabel show;
	public UILabel percent;
	
	public GameObject ds;


	DateTime thisWeekStartDate; //??? ???
	DateTime thisWeekEndDate;

	// Use this for initialization
	void Start () {
		DateTime today = DateTime.Now;

		SaveSports ssports = new SaveSports ();

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

		float thisWeekStandardKal = 2000;

		float sumKal = 0;

//		Debug.Log ("thisWeekStartDate = " + thisWeekStartDate);
		for (DateTime d = thisWeekStartDate; d.Date <= today.Date; d = d.AddDays(1)) {
			float tmpKal = ssports.GetDayKal(d);
			sumKal += tmpKal ;
//			Debug.Log("sumKal = " + sumKal);
		}

		float kal = thisWeekStandardKal - sumKal ;
		if (kal <= 0) {
			kal = 0;
		}

		float p = sumKal / thisWeekStandardKal * 100;
		if (p >= 100.0f) {
			p = 100.0f;
		}


		float x = sumKal / thisWeekStandardKal;
		
		if (x >= 1.0f) {
			x = 1.0f;
		}

		ds.GetComponent<UISlider> ().value = x;

		show.text = kal.ToString ();

		percent.text = p.ToString ("f1") + "%";
	}
	
	
}
