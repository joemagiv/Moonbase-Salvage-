using UnityEngine;
using System.Collections;

public class MoonMan : MonoBehaviour {

	public string Name;
	public int homeID;
	public int jobID;
	public float happiness;
	public float moveSpeed;
	private Room[] roomArray;
	public GameObject homeLocation;
	public GameObject workLocation;
	public int jobStartTime;
	public int jobEndtime;
	
	// Use this for initialization
	void Start () {
		if (Room.totalLivingSpaces != Room.totaloccupiedLivingSpaces) {
			roomArray = GameObject.FindObjectsOfType<Room> ();
				if (homeLocation == null) {
					foreach (Room thisRoom in roomArray) {
						if (thisRoom.occupiedLivingSpaces < thisRoom.LivingSpaces) {
								homeID = thisRoom.roomID;
								thisRoom.occupiedLivingSpaces += 1;
								homeLocation = thisRoom.gameObject;
								Debug.Log ("Assigned to live:" + homeLocation);
								goHome();
								InvokeRepeating("findWorkLocationRetry",2,0.5f);
						}
						else{
							continue;
						}
					}
				}
			}
			else{
				//Debug.Log (this + " has nowhere to Live");
				InvokeRepeating("findLivingSpace",2,0.5f);
				return ;
		}
	}
	

	
	void findLivingSpace(){
		roomArray = GameObject.FindObjectsOfType<Room> ();
		if (Room.totalLivingSpaces != Room.totaloccupiedLivingSpaces){
			if (homeID == 0) {
				foreach (Room thisRoom in roomArray) {
					if (thisRoom.occupiedLivingSpaces < thisRoom.LivingSpaces) {
						homeID = thisRoom.roomID;
						thisRoom.occupiedLivingSpaces += 1;
						homeLocation = thisRoom.gameObject;
						Debug.Log ("Assigned to live:" + homeLocation);
						goHome();
						CancelInvoke ("findLivingSpace");
						if (workLocation == null){
						InvokeRepeating("findWorkLocationRetry",2,0.5f);
						}
					}
					else{
						continue;
					}
				}
				}
			}
			else{
				//Debug.Log (this + " has nowhere to Live");
				return ;
			}
		//Debug.Log ("Already living somewhere");
		//CancelInvoke ("findLivingSpace");
	}
	
	void findWorkLocationRetry(){
		roomArray = GameObject.FindObjectsOfType<Room> ();
		if (Room.totalOpenWorkingSpaces != Room.totalFilledWorkingSpaces){
			if (jobID == 0) {
				foreach (Room thisRoom in roomArray) {
					if (thisRoom.filledWorkingSpaces < thisRoom.openWorkingSpaces) {
						jobID = thisRoom.roomID;
						thisRoom.filledWorkingSpaces += 1;
						workLocation = thisRoom.gameObject;
						Debug.Log ("Assigned to work:" + workLocation);
						jobStartTime = Random.Range (1, 16);
						jobEndtime = jobStartTime + 8;
						CancelInvoke ("findWorkLocationRetry");
					}
					else{
						continue;
					}
				}
			}
		}
		else{
			return ;
		}
	}
	
	
	void goHome(){
		float step = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, homeLocation.transform.position, step);
	}
	
	void goToWork(){
		float step = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, workLocation.transform.position, step);
	}
	
	void clockListener(){
		
	}
	
	void Update(){
		if (homeLocation != null){
			if (ValueTracker.gameClock >= jobEndtime || ValueTracker.gameClock <= jobStartTime) {
				goHome ();
			}
			else
			if (ValueTracker.gameClock >= jobStartTime || ValueTracker.gameClock <= jobEndtime) {
				goToWork ();
			}
	}
	}

// These can be called outside of the main project
	public void triggerFindLivingSpace(){
		InvokeRepeating("findLivingSpace",2,0.5f);
	}
	
	public void triggerFindWorkLocation(){
		InvokeRepeating("findWorkLocationRetry",2,0.5f);
	}

}
