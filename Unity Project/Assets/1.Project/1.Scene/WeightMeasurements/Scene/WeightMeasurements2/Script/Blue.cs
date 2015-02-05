using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		//從SD卡取出體重
		float [] w = new float[10];
		
		for(int i = 0; i < w.Length; i++){
			w [i] = PlayerPrefs.GetFloat("Weight" + i);
		}
		v2 = w;

		//Instantiate (yLine, new Vector3 (10.0f, 0, 0), Quaternion.identity);// as Transform;   //
		//對x軸設定
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
		lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
		//lineGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等
		
		//對y軸設定
		//lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		//lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = w.Max() + 2.0f; //設定y軸 最大值
		lineGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = w.Min() - 2.0f; //設定y軸 最小值

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
		}

		//series.GetComponent<WMG_Series> ().pointValues = sV1; //預設只有一種長條
		series2.GetComponent<WMG_Series> ().pointValues = sV2; //預設只有一種長條
	}
	void Update(){
		YAxisMarks.transform.position = new Vector3(315.0f,100.0f,0);
		//YAxisMarks.GetComponent<WMG_Grid> ().noHorizontalLinks = true;
	}


	// Update is called once per frame
	/*
	void InitGraphSet(float xAxisMaxValue,float xAxisMinValue,int xAxisNumTicks,float xAxisLength
	                  ,float yAxisMaxValue,float yAxisMinValue,int yAxisNumTicks,float yAxisLength
	                  ,List<Vector2> sV2){

		//對x軸設定
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisMaxValue = xAxisMaxValue; 
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisMinValue = xAxisMinValue;
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisNumTicks = xAxisNumTicks;	//
		barGraph.GetComponent<WMG_Axis_Graph>().xAxisLength = xAxisLength;	//x軸實際長度
		//barGraph.GetComponent<WMG_Axis_Graph>().xAxisLabels = xAxisLabels; // = xAxisNumTicks ，兩個要對等

		//對y軸設定
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisMaxValue = yAxisMaxValue; 
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisMinValue = yAxisMinValue;
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisNumTicks = yAxisNumTicks;	
		barGraph.GetComponent<WMG_Axis_Graph>().yAxisLength = yAxisLength;	
		//barGraph.GetComponent<WMG_Axis_Graph>().yAxisLabels = yAxisLabels; 

		//設定x軸長條的數據
		/*
	 	sV2 = new List<Vector2> ();
		for (int i=0; i < xAxisNumTicks-1; i++) {
			sV2.Add(new Vector2(0,v[i]));	
			//series.GetComponent<WMG_Series> ().pointValues.Add(new Vector2(0,v[i]));
			//print(series.GetComponent<WMG_Series> ().pointValues[i]);
		}

		series2.GetComponent<WMG_Series> ().pointValues = sV2; //預設只有一種長條
	}
	*/
}
