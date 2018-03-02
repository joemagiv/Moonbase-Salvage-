using UnityEngine;
using System.Collections;

public class Tourist : MonoBehaviour {

	public string Name;
	public float happiness;
	public float moveSpeed;
	private Room[] roomArray;
	public GameObject vacationAttraction;
	public int attractionDuration;
	public int vacationDuration;
	private Room attractionsRoom;
	private ValueTracker valuetracker;

	// Use this for initialization
	void Start () {
		vacationDuration = Random.Range (10, 48);
		findAttraction ();
		valuetracker = FindObjectOfType<ValueTracker> ().GetComponent <ValueTracker>();
	}

	void findAttraction(){
		roomArray = GameObject.FindObjectsOfType<Room> ();
		if (Room.totalOpenTourismSpaces != Room.totalFilledTourismSpaces) {
			foreach (Room thisRoom in roomArray) {
				if (thisRoom.filledTourismSpaces < thisRoom.openTourismSpaces) {
					vacationAttraction = thisRoom.gameObject;
					thisRoom.filledTourismSpaces += 1;
					attractionsRoom = thisRoom.GetComponent<Room>();
					attractionDuration = Random.Range (1, vacationDuration);
					break;
				}
			}
		} else {
			Debug.Log ("No where for tourist to vacation");
		}

	}

	public void leaveMoonBase(){
		attractionsRoom.filledTourismSpaces -= 1;
		Debug.Log (this + " is leaving Moonbase");
		valuetracker.satisfiedTourists += 1;
		Destroy (this.gameObject);
	}

	public void findNewAttraction(){
		roomArray = GameObject.FindObjectsOfType<Room> ();
		if (Room.totalOpenTourismSpaces != Room.totalFilledTourismSpaces) {
			attractionsRoom.filledTourismSpaces -= 1;
			vacationAttraction = null;
			foreach (Room thisRoom in roomArray) {
				if (vacationAttraction != thisRoom.gameObject) {
					if (thisRoom.filledTourismSpaces < thisRoom.openTourismSpaces) {
						vacationAttraction = thisRoom.gameObject;
						thisRoom.filledTourismSpaces += 1;
						attractionsRoom = thisRoom.GetComponent<Room>();
						attractionDuration = Random.Range (1, vacationDuration);
						break;
					}
				} else {
					return;
				}
			}
		}

		} 


	void goToAttraction(){
		float step = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, vacationAttraction.transform.position, step);
	}
	
	// Update is called once per frame
	void Update () {
		if (vacationAttraction != null) {
			goToAttraction ();
		}
	}
}
