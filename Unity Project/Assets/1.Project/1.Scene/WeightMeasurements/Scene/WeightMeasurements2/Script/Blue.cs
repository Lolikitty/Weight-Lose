using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class Blue : MonoBehaviour {
	
	//red
	public GameObject lineGraph;
	public GameObject backGround;
	public GameObject series2;
	public GameObject YAxisMarks;
	
	//barGraph 的 屬性 //預設值
	public float xAxisMaxValue = 100.0f; 
	public float xAxisMinValue = 0f;
	public int xAxisNumTicks = 11;	//切割n份，要n+1
	public float xAxisLength = 280.0f;	//x軸實際長度
	
	public float yAxisMaxValue = 30.0f; 
	public float yAxisMinValue = 22.0f;
	public int yAxisNumTicks = 9;
	public float yAxisLength = 180.0f;
	
	//public List<string> xAxisLabels; // = xAxisNumTicks-1 ，兩個要對等
	//public List<string> yAxisLabels;
	//public List<Vector2> sV1; //Vector 數目對應 int，長條的數據
	List<Vector2> sV2; //Vector 數目對應 int，長條的數據
	
	//9
	float[] v2;// = new float[]{30.0f,35.0f,40.0f,45.0f,50.0f,55.0f,60.0f,65.0f,70.0f};
	
	// Use this for initialization
	void Start () {

		v2 = new float[xAxisNumTicks - 1];
		for (int i=0; i < xAxisNumTicks - 1; i++) {
			v2[i] = 0 ;
		}


		Set (DateTime.Now);


		lineGraph.transform.localScale = new Vector2(2.0f,2.0f);
		lineGraph.transform.localPosition = new Vector2 (-250.0f,-380.0f);



		//對x軸設定
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
		//lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等
		
		//對y軸設定
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;
//		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = w.Max() + 2.0f; //設定y軸 最大值
//		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = w.Min() - 2.0f; //設定y軸 最小值

		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisNumTicks = yAxisNumTicks;	
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisLength = yAxisLength;	
		//lineGraph.GetComponent<WMG_Axis_Graph>().yAxisLabels = yAxisLabels; 
		
		//設定x軸長條的數據
		//xAxisNumTicks-1
		//print (xAxisNumTicks);
		sV2 = new List<Vector2> ();
		for (int i=0; i < xAxisNumTicks-1; i++) {
			//sV1.Add(new Vector2(0,v1[i]));	
			sV2.Add(new Vector2(0,v2[i]));
//			Debug.Log("v2[i] = " + v2[i]);
		}

		//series.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
		series2.GetComponent<WMG_Series> ().pointValues = sV2; //預設只有一種長條
	}
	void Update(){
		YAxisMarks.transform.localPosition = new Vector2 (280.0f,0); //.position = new Vector3(315.0f,100.0f,0);
		//YAxisMarks.GetComponent<WMG_Grid> ().noHorizontalLinks = true;
	}

	void Set(DateTime day){
//		lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabelSize = xAxisNumTicks - 1;
		SaveWeight w = new SaveWeight ();


		float lastWeightTmp = 0;
		float wei;
		int i = 0;
		for (DateTime d = day.AddDays(-9); d.Date <= day.Date; d = d.AddDays(1),i++) {

//			Debug.Log("i = " + i);

			lineGraph.GetComponent<WMG_Axis_Graph> ().xAxisLabels[i] = d.Day.ToString();
			wei = w.GetDayWeight(d);
			if(wei==0){
				if(lastWeightTmp == 0){
					lastWeightTmp = w.GetLastDayWeight(d);
					v2[i] = lastWeightTmp;
				}else{
					v2[i] = lastWeightTmp;
				}
			}
			else{
				lastWeightTmp = wei;
				v2[i] = wei;
			}
		}
		float standardMax = 0;
		if (v2.Max () != null) {
			standardMax = v2.Max();
		}
//		Debug.Log (v2.Max());
		yAxisMaxValue = standardMax + 5.0f;
		yAxisMinValue = 0;
	}

}
