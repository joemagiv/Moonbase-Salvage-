using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BlankRoom : MonoBehaviour {


	public static int selectedRoomLevel;
	public static int selectedRoomID;
	public static GameObject selectedRoomSlot;
	private Room room;
	private Animator anim;
	private BuildMenu buildmenu;
	
	// Use this for initialization
	void Start () {
		anim = FindObjectOfType<BuildUI> ().GetComponent<Animator> ();
		buildmenu = FindObjectOfType<BuildMenu> ().GetComponent<BuildMenu> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void BuildMenuCalled(){
		//buildmenu.MakeUIActivetoDisableCamera ();
		anim.SetTrigger ("Menu Called");
		room = GetComponent<Room>();
		selectedRoomLevel = room.roomLevel;
		selectedRoomID = room.roomID;
		selectedRoomSlot = this.gameObject;
		Debug.Log ("Selected Room is: Level: " + selectedRoomLevel + " ID: " + selectedRoomID);
		Debug.Log ("Selected GameObject is" + selectedRoomSlot);
		}
	
	
}
