using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buildCategories : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject[] roomPrefabArray;
	private GameObject buttonsParent;
	public GameObject buttonSpawner;
	private Vector3 buttonLocation;
	private BuildButton buildButton;
	public static bool ButtonInitialize = false;



	// Use this for initialization
	void Start () {
		buttonsParent = GameObject.Find ("ButtonPanel");
		if (!ButtonInitialize) {
			firstButtons ();
		}
			
	}

	public void TestingClicks(){
		Debug.Log ("This is the testing Clicks system");
	}

	public void NewButtons ()
	{
		

		buttonLocation = buttonSpawner.transform.position;
		Debug.Log ("Button location detected at: " + buttonLocation + " Spawner at: " + buttonSpawner.transform.position);
		GameObject[] existingButtons = GameObject.FindGameObjectsWithTag ("BuildButton");
		Debug.Log ("Existing Buttons are" + existingButtons);
		foreach (GameObject currentButton in existingButtons) {
			Destroy (currentButton);
		}

		foreach (GameObject currentRoomPrefab in roomPrefabArray) {
			GameObject button = Instantiate (buttonPrefab, buttonLocation, Quaternion.identity) as GameObject;
			buildButton = button.GetComponent<BuildButton> ();
			buildButton.roomPrefab = currentRoomPrefab;
			button.transform.SetParent (buttonsParent.transform, false);
			buttonLocation.y -= 250.0079f;
		}
	}

	void firstButtons(){
		Debug.Log ("Button location detected at: " + buttonLocation + " Spawner at: " + buttonSpawner.transform.position);

		buttonLocation = buttonSpawner.transform.position;
		foreach (GameObject currentRoomPrefab in roomPrefabArray) {
			GameObject button = Instantiate (buttonPrefab, buttonLocation, Quaternion.identity) as GameObject;
			button.transform.SetParent (buttonsParent.transform, false);
			buildButton = button.GetComponent<BuildButton> ();
			buildButton.roomPrefab = currentRoomPrefab;
			buttonLocation.y -= 250.0079f;
		}
		ButtonInitialize = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
