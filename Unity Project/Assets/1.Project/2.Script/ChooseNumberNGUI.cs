using UnityEngine;
using System.Collections;

public class ChooseNumberNGUI : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	bool sw, sw2;
	float y1, y2;
	float move1, move2, move3, move3f;
	float offset = 0 , offset2;
	float chooseNumberBuffer;

	public float chooseNumber; // 當前數字

	// Unity Override Methods ==============================================================================================================================
	
//	void OnClick(){
//		sw = true;
//		y2 = Input.mousePosition.y;
//
//	}

	void Start(){

	}

	public void Stop(){
		sw2 = false;
	}

	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast(ray,out hit)){
			if(Input.GetMouseButtonDown(0)){
				if(hit.collider.name == name){
					sw = true;
					y2 = Input.mousePosition.y;
				}
			}
		}

		// 當放開按鍵後，改成自動移動

		if(Input.GetMouseButtonUp(0)){
			sw = false;
			sw2 = true;
			move3f = move3;
		}

		// 固定到數字中間

		if(!Input.GetMouseButton(0)){
			int p2 = (int)(offset * 100) - (((int)(offset * 10)) * 10);
			if(p2!=0){
				offset+= p2 > 5? 0.001f : -0.001f;
				GetComponent<UITexture>().uvRect = new Rect(0, offset, 1, 0.1f);
			}
		}

		// 自動移動

		if(sw2){			
			if(move3f > 0){
				if(move3 > 0){
					move3--;
				}
			}else if(move3f < 0){
				if(move3 < 0){
					move3++;
				}
			}
			if(-1 < move3 && move3 < 1){
				sw2 = false;
			}

			offset += move3/4000;
			GetComponent<UITexture>().uvRect = new Rect(0, offset, 1, 0.1f);
		}

		// 手動移動

		if(sw){
			y1 = y2;
			y2 = Input.mousePosition.y;
			move1 = move2;
			move2 = y1-y2;
			move3 = Mathf.Lerp(move1, move2, Time.time);
			offset += move3/1000;
			GetComponent<UITexture>().uvRect = new Rect(0, offset, 1, 0.1f);
		}
		chooseNumberBuffer = ((float.Parse(offset.ToString ("0.0"))) * 10);
		while(chooseNumberBuffer < 0){
			chooseNumberBuffer += 10;
		}
		while(chooseNumberBuffer >= 10){
			chooseNumberBuffer -= 10;
		}
		chooseNumber = chooseNumberBuffer;
	}

	public void Set_number(int set){

		offset = set * 0.1f;

//		Debug.Log (set);

		GetComponent<UITexture>().uvRect = new Rect(0, offset , 1, 0.1f);

		chooseNumberBuffer = ((float.Parse((offset % 1).ToString ("0.0"))) * 10);

		while(chooseNumberBuffer < 0){
			chooseNumberBuffer += 10;
		}
		while(chooseNumberBuffer >= 10){
			chooseNumberBuffer -= 10;
		}
		chooseNumber = chooseNumberBuffer;

	}

	// Custom Methods ======================================================================================================================================

}
