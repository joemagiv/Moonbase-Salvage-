using UnityEngine;
using System.Collections;

public class ValueTracker : MonoBehaviour {

	
	public float heartbeatTimer;
	private float cashFlow;
	public float totalCash;
	public float totalMoonRocks;
	public float currentMoonRockDemand;
	public static int gameClock = 1;
	public int satisfiedTourists;
	private Room[] roomArray;
	private Tourist[] touristArray;
	private PowerSystem powersystem;
	private MoonManSpawner moonManSpawner;


	
	// Use this for initialization
	void Start () {
		totalCash = 1000;
		InvokeRepeating("Heartbeat", 2, heartbeatTimer);
		powersystem = FindObjectOfType<PowerSystem>();
		moonManSpawner = FindObjectOfType<MoonManSpawner> ().GetComponent<MoonManSpawner> ();
	}
	
	void Heartbeat(){
		
		if (gameClock == 24) {
			gameClock = 1;
		} else {
			gameClock += 1;
		}
		miningHeartBeat();
		productionHeartBeat();
		touristMoonManHeartbeat ();
		tourismEconomyHeartbeat ();
		powersystem.checkPowerUse();
		moonManSpawner.CalculateMoonManSpawn ();
		moonManSpawner.CalculateTouristSpawn ();
	}

	void tourismEconomyHeartbeat(){
		if (Room.totalFilledTourismSpaces > 0) {
			cashFlow = ((Room.totalTourismCapability/Room.totalOpenTourismSpaces) * Room.totalFilledTourismSpaces);
			Debug.Log ("Cash flow from tourism: " + cashFlow.ToString()); 
			totalCash += cashFlow;
		}
	}

	void touristMoonManHeartbeat(){
		touristArray = FindObjectsOfType<Tourist> ();
		//reduce all tourist durations, see who needs to go home
		if (Room.totalFilledTourismSpaces != Room.totalOpenTourismSpaces) {
			foreach (Tourist thisTourist in touristArray) {
				thisTourist.attractionDuration -= 1;
				thisTourist.vacationDuration -= 1;
				if (thisTourist.attractionDuration <= 0) {
					thisTourist.findNewAttraction ();
				}
				if (thisTourist.vacationDuration <= 0) {
					Debug.Log ("Vacation over");
					thisTourist.leaveMoonBase ();
				}
			}
		} else {
			foreach (Tourist thisTourist in touristArray) {
				if (thisTourist.vacationDuration <= 0) {
					Debug.Log ("Vacation over");
					thisTourist.leaveMoonBase ();
				} else {
					thisTourist.vacationDuration -= 1;
				}
			}
		}
	}
		

	void findLivingSpaces ()
	{
		roomArray = GameObject.FindObjectsOfType<Room> ();
		Room.totalLivingSpaces = 0;
		Room.totaloccupiedLivingSpaces = 0;
		Room.totalOpenWorkingSpaces = 0;
		Room.totalFilledWorkingSpaces = 0;
		Room.totalOpenTourismSpaces = 0;
		Room.totalFilledTourismSpaces = 0;
		foreach (Room thisRoom in roomArray) {
			Room.totalLivingSpaces += thisRoom.LivingSpaces;
			Room.totaloccupiedLivingSpaces += thisRoom.occupiedLivingSpaces;
			Room.totalOpenWorkingSpaces += thisRoom.openWorkingSpaces;
			Room.totalFilledWorkingSpaces += thisRoom.filledWorkingSpaces;
			Room.totalOpenTourismSpaces += thisRoom.openTourismSpaces;
			Room.totalFilledTourismSpaces += thisRoom.filledTourismSpaces;
		}
		//Debug.Log ("Total living spaces: " + Room.totalLivingSpaces + " Total Occupied: " + Room.totaloccupiedLivingSpaces);
	}
	
	void EconomicHeartbeat ()
	{
		Debug.Log ("Heartbeat detected");
		totalMoonRocks += Room.totalMiningCapability;
		if (totalMoonRocks > currentMoonRockDemand) {
			cashFlow = (currentMoonRockDemand * Room.totalProductivityCapability / 100) + (Room.totalTourismCapability*Room.totalFilledTourismSpaces);
			totalMoonRocks = totalMoonRocks - currentMoonRockDemand;
			Debug.Log ("Enough rocks, reducing rocks: " + totalMoonRocks + "&" + currentMoonRockDemand);
		}
		else {
			cashFlow = Room.totalTourismCapability;
			Debug.Log ("Not enough rocks");
		}
		totalCash += cashFlow;
		Debug.Log ("Total Moon Rocks is: " + totalMoonRocks);
		Debug.Log ("Total Cashflow is: " + cashFlow);
		Debug.Log ("Total Cash is: " + totalCash);
		Debug.Log ("Total Mining Capability is: " + Room.totalMiningCapability);
		Debug.Log ("Total Tourism Capability is: " + Room.totalTourismCapability);
	}
	
	void miningHeartBeat(){
		roomArray = GameObject.FindObjectsOfType<Room> ();
		float moonRockGeneration = 0;
		foreach (Room thisRoom in roomArray) {
			if (thisRoom.miningCapability > 0){
			float roomProductivityEfficacy = (thisRoom.filledWorkingSpaces*1.0f/thisRoom.openWorkingSpaces);
			//Debug.Log (thisRoom + "Filled/Open Positions: " + thisRoom.filledWorkingSpaces + "/" + thisRoom.openWorkingSpaces + "=" + roomProductivityEfficacy);
			//Debug.Log ("Efficacy is" + roomProductivityEfficacy);
			moonRockGeneration += thisRoom.miningCapability * roomProductivityEfficacy;
			//Debug.Log ("Moon rock generation is: " + moonRockGeneration);
			}
			else {
			continue;
		}
		}
		totalMoonRocks += moonRockGeneration;
	}
	
	void productionHeartBeat(){
		roomArray = GameObject.FindObjectsOfType<Room> ();
		cashFlow = 0;
		foreach (Room thisRoom in roomArray) {
			if (thisRoom.productivityCapability > 0){
				if (totalMoonRocks > currentMoonRockDemand) {
					float roomProductivityEfficacy = (thisRoom.filledWorkingSpaces*1.0f/thisRoom.openWorkingSpaces);
					cashFlow += (thisRoom.productivityCapability * roomProductivityEfficacy * currentMoonRockDemand)/currentMoonRockDemand;
					totalMoonRocks = totalMoonRocks - (currentMoonRockDemand * roomProductivityEfficacy);
				}
				else {
					Debug.Log ("Not enough moon rock demand");
				}
			}
			else {
				continue;
			}
		}
		totalCash += cashFlow;
	}
	
	// Update is called once per frame
	void Update () {
		findLivingSpaces ();
		
	}
}
