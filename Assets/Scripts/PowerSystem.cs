using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PowerSystem : MonoBehaviour {

	private Room[] roomArray;
	public float totalPowerAvailable;
	public float totalPowerUse;
	public float acceptablePowerDifference = 100f;
	private bool powerON = true;



	// Use this for initialization
	void Start () {
	
	}
	
	public void checkPowerUse(){
		if (powerON){
			RecalculatePowerUse ();
			//Debug.Log(roomIDList);
			if (totalPowerUse <= totalPowerAvailable){
					Debug.Log ("Less power use than Power Available, System Normal");
				}
				else {
					powerDown();
			}
				
			if ((totalPowerAvailable - totalPowerUse) >= acceptablePowerDifference){
				powerUp();	
				}
				else{
					Debug.Log ("Enough power, exiting power down/up loop");	
				}
			}
// This turns off everything with the power ON/Off Variable
		 else{
		 totalPowerAvailable = 0;
			if (totalPowerUse >= totalPowerAvailable){
				powerDown();
		 }
	}
	}

	void RecalculatePowerUse ()
	{
		roomArray = GameObject.FindObjectsOfType<Room> ();
		totalPowerAvailable = 0;
		totalPowerUse = 0;
		foreach (Room thisRoom in roomArray) {
			totalPowerAvailable += thisRoom.powerGeneration;
			totalPowerUse += thisRoom.powerUse;
		}
	}
	
	public void turnThePowerOnOff(){
		if (powerON){
		powerON = false;}
		else {
		powerON = true;
		}
	}
	
	
	public void powerDown(){
		//int mostRecentlyBuiltRoom = Mathf.Max (roomIDList.ToArray());
		//Debug.Log("Most recently build room is " + mostRecentlyBuiltRoom);
			foreach(Room thisRoom in roomArray){
				if(thisRoom.isActive == true && thisRoom.powerUse != 0){
					thisRoom.shutDownRoom();
					RecalculatePowerUse();
						if (totalPowerUse <= totalPowerAvailable){
							break; 
						}
					}
		}
	}
	
	void powerUp(){
	//	int leastRecentlyBuiltRoom = Mathf.Min(roomIDList.ToArray());
		foreach(Room thisRoom in roomArray){
			if(thisRoom.isActive == false){
				thisRoom.reOpenRoom();
				RecalculatePowerUse();
					if ((totalPowerAvailable - totalPowerUse) <= acceptablePowerDifference){
						break;
					}
				}
			}
		
	}
	
	// Update is called once per frame
	void Update () {
		
			
	}
}
