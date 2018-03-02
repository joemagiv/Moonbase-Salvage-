using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {

	public GameObject roomPrefab;
	private SpriteRenderer roomSpriteRenderer;
	private Sprite roomSprite;
	private Image previewImage;
	private Room roomScript;
	private Text roomStats;
	private BlankRoom blankRoom;
	private GameObject blankRoomObject;
	private Room roomReplacementRoom;
	private Animator anim;
	private ValueTracker valueTracker;

	// Use this for initialization
	void Start () {
		valueTracker = FindObjectOfType<ValueTracker> ().GetComponent<ValueTracker> ();
		anim = FindObjectOfType<BuildUI>().GetComponent<Animator> ();
		GameObject TempObject = Instantiate (roomPrefab);
		previewImage = GetComponentInChildren<Image> ();
		roomScript = TempObject.GetComponent<Room> ();
		roomStats = GetComponentInChildren<Text> ();
		roomSpriteRenderer = TempObject.GetComponentInChildren<SpriteRenderer> ();
		roomSprite = roomSpriteRenderer.sprite;
		previewImage.sprite = roomSprite;
		SetRoomStats ();
		Destroy (TempObject);
	
	}

	public void BuildRoom(){
		blankRoom = BlankRoom.selectedRoomSlot.GetComponent<BlankRoom>();
		blankRoomObject = BlankRoom.selectedRoomSlot;
		GameObject roomReplacement = Instantiate(roomPrefab, blankRoomObject.transform.position,Quaternion.identity) as GameObject;
		roomReplacementRoom = roomReplacement.GetComponent<Room>();
		roomReplacementRoom.roomID = BlankRoom.selectedRoomID;
		roomReplacementRoom.roomLevel = BlankRoom.selectedRoomLevel;
		Destroy (BlankRoom.selectedRoomSlot);
		valueTracker.totalCash -= roomReplacementRoom.roomCost;
		anim.SetTrigger ("Menu Dismissed");

	} 

	void SetRoomStats(){
		string roomName = roomScript.RoomName;
		// Find out what stats the room has, find its name and then it's value
		string primaryStatName = "test";
		float primaryStatValue = 0;
		string secondaryStatName = "test";
		float secondaryStatValue = 0;
		if (roomScript.productivityCapability > 0) {
			primaryStatName = "Productivity: ";
			primaryStatValue = roomScript.productivityCapability;
			secondaryStatName = "Positions: ";
			secondaryStatValue = roomScript.openWorkingSpaces;
		}
		if (roomScript.tourismCapability > 0) {
			primaryStatName = "Tourism: ";
			primaryStatValue = roomScript.tourismCapability;
			secondaryStatName = "Capacity: ";
			secondaryStatValue = roomScript.openTourismSpaces;
		}
		if (roomScript.miningCapability > 0) {
			primaryStatName = "Mining: ";
			primaryStatValue = roomScript.miningCapability;
			secondaryStatName = "Positions: ";
			secondaryStatValue = roomScript.openWorkingSpaces;
		}
		if (roomScript.LivingSpaces > 0) {
			primaryStatName = "LivingSpaces: ";
			primaryStatValue = roomScript.LivingSpaces;
		}
		if (roomScript.powerGeneration > 0) {
			primaryStatName = "Power Generation: ";
			primaryStatValue = roomScript.powerGeneration;
		}

		if (secondaryStatName != "test") {
			roomStats.text = roomName + "\n" + primaryStatName + primaryStatValue + "\t" + secondaryStatName + secondaryStatValue +
			"\nPower Use = " + roomScript.powerUse + "\tCost: $" + roomScript.roomCost;
		} else {
			roomStats.text = roomName + "\n" + primaryStatName + primaryStatValue + "\t\t" +
				"\nPower Use = " + roomScript.powerUse + "\tCost: $" + roomScript.roomCost;
		}
	

	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
