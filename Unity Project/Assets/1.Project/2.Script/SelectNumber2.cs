using UnityEngine;
using System.Collections;

public class SelectNumber2 : MonoBehaviour {

	public int Number;

	public int minNumber = 0;
	public int maxNumber = 9;
	public float distance = 95f;

	float ya;
	int n;

	// Unity Override Methods ==============================================================================================================================

	void Awake (){
		ya = transform.localPosition.y;
	}

	void Update () {
		n = Compute(minNumber, maxNumber, distance, transform.localPosition.y);
		if(Input.GetMouseButtonUp(0)){
			Invoke("ReCompute", 0.5f);
		}
	}

	// Custom Methods ======================================================================================================================================

	int Compute(int min, int max, float distance, float y){				
		int i;
		if(y > 0){
			i = 1;
			while (y > 0) {
				y-=distance;
				i--;
				if(i<min){
					i=max;
				}
			}
			i--;
			if(i<min){
				i=max;
			}
		}else{
			i = -1;
			while (y < 0) {
				y+=distance;
				i++;
				if(i>max){
					i=min;
				}
			}
		}		
		return i;
	}

	void ReCompute(){
		Number = n;
	}

}


