using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IndivCharSelect : MonoBehaviour {
	public int controllerID;

	public Image sliderR;
	public Image sliderG;
	public Image sliderB;
	public Text classText;
	public Text profileText;
	public Image selectionBlock;
	public Image colourBlock;

	int 	sliderMin;
	int 	maxSliderVal;
	int 	sliderRVal;
	int 	sliderGVal;
	int 	sliderBVal;
	int 	selectedClass;
	float 	initialSliderX;

	int 	selectedOption;

	bool 	ready;
	Vector2 input;

	float 	inputCooldown;

	void Start () {
		sliderMin 	 = -20;
		maxSliderVal = 40;
		sliderRVal 	 = 1;
		sliderGVal 	 = 1;
		sliderBVal 	 = 1;
		selectedClass = 0;

		sliderR.transform.Translate (sliderMin, 0, 0);
		sliderG.transform.Translate (sliderMin, 0, 0);
		sliderB.transform.Translate (sliderMin, 0, 0);

		initialSliderX = sliderR.transform.position.x - 20;

		classText.text = "Archer";

		ready 			= false;
		selectedOption 	= 1;
	}

	void Update () {
		input = new Vector2 (Input.GetAxis ("LeftJoystickX_P" + controllerID.ToString ()), Input.GetAxis("LeftJoystickY_P" + controllerID.ToString ()));
	
		inputCooldown -= Time.deltaTime;

		if (inputCooldown <= 0f) {
			if (input.y >= 0.4f) {
				if(selectedOption < 4){
					selectedOption ++;
					moveSelectionBlock();
				}
			} else if (input.y <= -0.4f) {
				if(selectedOption > 0){
					selectedOption--;
					moveSelectionBlock();
				}
			}
		}

		if (selectedOption == 0) {
			if (Input.GetButtonDown ("A_P" + controllerID.ToString ())){
				showProfileList ();
				bool isChoosingProfile = true;
			}
		}

		if (selectedOption >= 1 && selectedOption <= 3)
			isSelectingColour ();

		if (selectedOption == 4 && inputCooldown <= 0f)
			isChoosingClass ();
	}

	void showProfileList(){

	}

	void moveSelectionBlock(){
		inputCooldown = 0.3f;
		switch(selectedOption){
			case 0:
			selectionBlock.transform.position = new Vector3(initialSliderX, profileText.transform.position.y, profileText.transform.position.z);
				break;
			case 1:
			selectionBlock.transform.position = new Vector3(initialSliderX, sliderR.transform.position.y, sliderR.transform.position.z);
				break;
			case 2:
			selectionBlock.transform.position = new Vector3(initialSliderX, sliderG.transform.position.y, sliderG.transform.position.z);
				break;
			case 3:
			selectionBlock.transform.position = new Vector3(initialSliderX, sliderB.transform.position.y, sliderB.transform.position.z);
				break;
			case 4:
			selectionBlock.transform.position = new Vector3(initialSliderX, classText.transform.position.y, classText.transform.position.z);
				break;
			default: break;
		}
	}

	void isChoosingClass(){
		if (input.x >= 0.4f) {
			if (selectedClass < 3){
				selectedClass += 1;
				inputCooldown = 0.3f;
			}
		} else if (input.x <= -0.4f) {
			if(selectedClass > 0){
				selectedClass -= 1;
				inputCooldown = 0.3f;
			}
		}

		switch (selectedClass) {
		case 0:
			classText.text = "Archer";
			break;
		case 1:
			classText.text = "Warrior";
			break;
		case 2:
			classText.text = "Mage";
			break;
		case 3: 
			classText.text = "Tactician";
			break;
		default:
			break;
		}

		CharSelect.enteredPlayers[controllerID].selectedClass = selectedClass;
	}

	void isSelectingColour(){
			switch(selectedOption){
			case 1:
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
			case 2:
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
			case 3:
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
}
