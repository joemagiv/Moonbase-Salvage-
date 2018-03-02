using UnityEngine;
using System.Collections;

public class BuildMenu : MonoBehaviour {

	public GameObject typeOfRoom;
	private BlankRoom blankRoom;
	private GameObject blankRoomObject;
	private Room roomReplacementRoom;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = FindObjectOfType<BuildUI>().GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MakeUIActivetoDisableCamera(){
		if (!cameraScript.UIactive) {
			cameraScript.UIactive = true;
		} else {
			cameraScript.UIactive = false;
		}

	}
	
	public void BuildRoom(){
		blankRoom = BlankRoom.selectedRoomSlot.GetComponent<BlankRoom>();
		blankRoomObject = BlankRoom.selectedRoomSlot;
		GameObject roomReplacement = Instantiate(typeOfRoom, blankRoomObject.transform.position,Quaternion.identity) as GameObject;
		roomReplacementRoom = roomReplacement.GetComponent<Room>();
		roomReplacementRoom.roomID = BlankRoom.selectedRoomID;
		roomReplacementRoom.roomLevel = BlankRoom.selectedRoomLevel;
		Destroy (BlankRoom.selectedRoomSlot);
		anim.SetTrigger ("Menu Dismissed");
		MakeUIActivetoDisableCamera ();
	}
	
	public void ExitMenu(){
		anim.SetTrigger ("Menu Dismissed");
		MakeUIActivetoDisableCamera ();

	}
}
