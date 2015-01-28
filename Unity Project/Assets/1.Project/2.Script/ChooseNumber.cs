using UnityEngine;
using System.Collections;

public class ChooseNumber : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	bool sw, sw2;
	float y1, y2;
	float move1, move2, move3, move3f;
	float offset = 0, offset2;
	float chooseNumberBuffer;

	//*修改後*
	private ChooseNumber chScript;
	private GameObject ChooseNumberXX;
	//*-*

	public float chooseNumber; // 當前數字

	// Unity Override Methods ==============================================================================================================================

	/*
	//*修改後*
	void Awake(){
		f ();
	}

	void Update(){
		f ();
	}

	void f() {
	//*-*
	*/

	//*原本*
	void Update(){
	//-*-

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
				renderer.material.mainTextureOffset = new Vector2(0, offset);
			}
		}

		// 自動移動

		if(sw2){			
			if(move3f > 0){
				if(move3 > 0){
					move3--;
				}else{
					sw2 = false;
				}
			}else if(move3f < 0){
				if(move3 < 0){
					move3++;
				}else{
					sw2 = false;
				}
			}
			offset += move3/4000;
			renderer.material.mainTextureOffset = new Vector2(0, offset);
		}

		// 手動移動

		if(sw){
			y1 = y2;
			y2 = Input.mousePosition.y;
			move1 = move2;
			move2 = y1-y2;
			move3 = Mathf.Lerp(move1, move2, Time.time);
			offset += move3/1000;
			renderer.material.mainTextureOffset = new Vector2(0, offset);
		}
		chooseNumberBuffer = ((float.Parse((offset % 1).ToString ("0.0"))) * 10);
		if(offset < 0){
			chooseNumberBuffer += 10;
		}
		if(chooseNumberBuffer == 10){
			chooseNumberBuffer = 0;
		}
		chooseNumber = chooseNumberBuffer;


		//*修改後*
		//判斷YYYY MM DD 日期是否合法
		/*
		switch(this.gameObject.name){
		case "ChooseNumberY1":
			if(chooseNumber > 2.0f)chooseNumber = 2.0f;
			else if(chooseNumber < 1.0f)chooseNumber = 1.0f;
			//處理捲動的數字
			//...
			break;
		case "ChooseNumberY2":
			ChooseNumberXX = Instantiate (ChooseNumberY1, new Vector3 (6.2f, 5.35f, -0.1f), Quaternion.identity) as GameObject;
			chScript = ChooseNumberXX.transform.GetComponent<ChooseNumber> ();

			if(ch.GetComponent<ChooseNumber>(ChooseNumberY1).chooseNumber == 2.0f )//當為西元2xxx，強制Y2為0
				chooseNumber = 0;

			//處理捲動的數字
			//...
			break;
			
		case "ChooseNumberY3":
			if(GameObject.GetComponent<ChooseNumber>(ChooseNumberY1).chooseNumber == 2.0f 
			   && chooseNumber > 1.0f)//當為西元20xx，Y3 > 1，強制Y3為 1
				chooseNumber = 1.0f;
			//處理捲動的數字
			//...
			break;
		case "ChooseNumberY4":
			if(GameObject.GetComponent<ChooseNumber>(ChooseNumberY1).chooseNumber == 2.0f 
			   && GameObject.GetComponent<ChooseNumber>(ChooseNumberY3).chooseNumber == 1.0f
			   && chooseNumber > 4.0f)//當為西元201x，Y4 > 4，強制Y4為 4
				chooseNumber = 4.0f;
			//處理捲動的數字
			//...
			break;
		case "ChooseNumberM1":
			//處理捲動的數字
			//...
			break;
		case "ChooseNumberM2":
			//處理捲動的數字
			//...
			break;
		case "ChooseNumberD1":
			//處理捲動的數字
			//...
			break;
		case "ChooseNumberD2":
			//處理捲動的數字
			//...
			break;
		default:
			print ("ChooseNumber is error");

		}
		//*-*
		*/
	}

	// Custom Methods ======================================================================================================================================

}
