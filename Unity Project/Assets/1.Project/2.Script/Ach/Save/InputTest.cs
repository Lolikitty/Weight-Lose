using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class InputTest : MonoBehaviour {

	// Use this for initialization
	void Start () {



		//---> 1.
		SaveFoodBeta sfbeta = new SaveFoodBeta ();
		DateTime breakfast = new DateTime(2015,2,23,8,0,0);
		DateTime lunch = new DateTime(2015,2,23,12,0,0);
		DateTime dinner = new DateTime(2015,2,23,18,0,0);


		DateTime b = breakfast.AddDays (-40);
		DateTime l = lunch.AddDays (-40);
		DateTime di = dinner.AddDays (-40);

		sfbeta.Save (b,"apple",600);
		sfbeta.Save (l,"apple",600);
		sfbeta.Save (di,"apple",600);
		//連續10天
		for(DateTime d = DateTime.Now ;d.Date >= DateTime.Now.AddDays(-20).Date ; d = d.AddDays(-1)){
			sfbeta.Save (breakfast,"apple",600);
			sfbeta.Save (lunch,"apple",600);
			sfbeta.Save (dinner,"apple",600);
			breakfast = breakfast.AddDays(-1);
			lunch = lunch.AddDays(-1);
			dinner = dinner.AddDays(-1);
		}
		//<---


		//---> 2.
		SaveFWater sfwater = new SaveFWater ();
		DateTime qq = DateTime.Now.AddDays(-20);
		DateTime ww = DateTime.Now;
		sfwater.Save (qq,ww);
		//<--- 


		//---> 3.
		SaveFWeight sfweight = new SaveFWeight ();
		sfweight.Save (qq,ww);
		//<--- 


		//---> 4.
		SaveHeightWaistline shwaistline = new SaveHeightWaistline ();

		for(DateTime st = DateTime.Now.AddDays(-20) ; st.Date <= DateTime.Now.AddDays(-1).Date ;st = st.AddDays(1)){
			shwaistline.f(st,170.0f,40.0f);
		}
		//<---


		//---> 5.
		SaveLoginLog sllog = new SaveLoginLog ();
		sllog.Save (DateTime.Now.AddDays(-20),DateTime.Now);
		//<--- 


		//---> 6.
		//SaveLvUpdateHistory suhistory = new SaveLvUpdateHistory ();


		//<---


		//---> 7.
		SaveMissionItemCount smicount = new SaveMissionItemCount ();
		smicount.Save (6,12);
		smicount.Save (7,12);
		smicount.Save (8,12);

		//<--- 



		//---> 8.
		//SaveMissionLv smlv = new SaveMissionLv ();
		//<---



		//---> 9.
		SaveMWeight smweight = new SaveMWeight ();
		smweight.Save (qq,ww);

		//<---


		//---> 10.
		SaveSports ssports = new SaveSports ();
		for(DateTime st = DateTime.Now.AddDays(-20) ; st.Date <= DateTime.Now.Date ;st = st.AddDays(1)){
			ssports.f(st , "sport1" ,50);
		}
		//<---

		//---> 11.
		SaveWater swater = new SaveWater ();
		for(DateTime st = DateTime.Now.AddDays(-20) ; st.Date <= DateTime.Now.Date ;st = st.AddDays(1)){
			swater.Save (st ,2500);
		}

		//<---


		//---> 12.
		SaveWeight sweight = new SaveWeight ();
		for(DateTime st = DateTime.Now.AddDays(-20) ; st.Date <= DateTime.Now.Date ;st = st.AddDays(1)){
			sweight.f (st ,70);
		}
		//<---

	}

}
