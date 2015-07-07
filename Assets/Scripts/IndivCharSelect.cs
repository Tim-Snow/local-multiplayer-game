using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IndivCharSelect : MonoBehaviour {
	public int controllerID;

	public Image sliderR;
	public Image sliderG;
	public Image sliderB;
	public Text classText;
	public Image selectionBlock;
	public Image colourBlock;

	int 	sliderMin;
	int 	maxSliderVal;
	int 	sliderRVal;
	int 	sliderGVal;
	int 	sliderBVal;
	float 	initialSliderX;

	int 	selectedOption;
	int 	selectionBlockSteps;

	bool 	ready;
	Vector2 input;

	float 	inputCooldown;

	void Start () {
		sliderMin 	 = -20;
		maxSliderVal = 40;
		sliderRVal 	 = 1;
		sliderGVal 	 = 1;
		sliderBVal 	 = 1;
		selectionBlockSteps = 10;

		sliderR.transform.Translate (sliderMin, 0, 0);
		sliderG.transform.Translate (sliderMin, 0, 0);
		sliderB.transform.Translate (sliderMin, 0, 0);

		initialSliderX = sliderR.transform.position.x - 20;

		classText.text = "Archer";

		ready 			= false;
		selectedOption 	= 0;
	}

	void Update () {
		input = new Vector2 (Input.GetAxis ("LeftJoystickX_P" + controllerID.ToString ()), Input.GetAxis("LeftJoystickY_P" + controllerID.ToString ()));

		print (selectedOption);

		inputCooldown -= Time.deltaTime;

		if (inputCooldown <= 0f) {
			if (input.y >= 0.4f) {
				if(selectedOption < 4){
					selectedOption ++;
					moveSelectionBlock();
					print ("down");
				}
			} else if (input.y <= -0.4f) {
				if(selectedOption > 0){
					selectedOption--;
					print ("up");
					moveSelectionBlock();
				}
			}
		}

		if (selectedOption >= 0 && selectedOption <= 2)
			isSelectingColour ();
	}

	void moveSelectionBlock(){
		inputCooldown = 0.3f;
		switch(selectedOption){
			case 0:
			selectionBlock.transform.position = new Vector3(initialSliderX, sliderR.transform.position.y, sliderR.transform.position.z);
				break;
			case 1:
			selectionBlock.transform.position = new Vector3(initialSliderX, sliderG.transform.position.y, sliderG.transform.position.z);
				break;
			case 2:
			selectionBlock.transform.position = new Vector3(initialSliderX, sliderB.transform.position.y, sliderB.transform.position.z);
				break;
			case 3:
			classText.text = "NO CHOICE BUT ARCHER";
				break;
			default: break;
		}
	}

	void isSelectingColour(){
			switch(selectedOption){
			case 0:
				if (input.x >= 0.4f) {
					if (sliderRVal <= maxSliderVal){
						sliderR.transform.Translate (1, 0, 0);
						sliderRVal += 1;
					}
				} else if (input.x <= -0.4f) {
					if(sliderRVal >= 1){
						sliderR.transform.Translate (-1, 0, 0);
						sliderRVal -= 1;
					}
				}
				break;
			case 1:
			if (input.x >= 0.4f) {
				if (sliderGVal <= maxSliderVal){
					sliderG.transform.Translate (1, 0, 0);
					sliderGVal += 1;
				}
			} else if (input.x <= -0.4f) {
				if(sliderGVal >= 1){
					sliderG.transform.Translate (-1, 0, 0);
					sliderGVal -= 1;
				}
			}
				break;
			case 2:
			if (input.x >= 0.4f) {
				if (sliderBVal <= maxSliderVal){
					sliderB.transform.Translate (1, 0, 0);
					sliderBVal += 1;
				}
			} else if (input.x <= -0.4f) {
				if(sliderBVal >= 1){
					sliderB.transform.Translate (-1, 0, 0);
					sliderBVal -= 1;
				}
			}
				break;
			default:
				break;
			}

		Color c = new Color ((float)sliderRVal / 40, (float)sliderGVal / 40, (float)sliderBVal / 40);
		colourBlock.color = c;
		CharSelect.enteredPlayers [controllerID].selectedColor = c;
	}

	void isSelectingClass(){
	}
}
