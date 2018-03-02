using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	//public values for changing within in inspector
	public string RoomName;
	public string FlavorText;
	public float productivityCapability;
	public float tourismCapability;
	public float roomCost;
	public float powerGeneration;
	public float powerUse;
	public bool isActive;
	public static float totalMiningCapability;
	public static float totalProductivityCapability;
	public static float totalTourismCapability;
	public static int totalLivingSpaces;
	public static int totaloccupiedLivingSpaces;
	public static int totalOpenWorkingSpaces;
	public static int totalFilledWorkingSpaces;
	public static int totalOpenTourismSpaces;
	public static int totalFilledTourismSpaces;
	public int LivingSpaces;
	public int occupiedLivingSpaces;
	public int openWorkingSpaces;
	public int filledWorkingSpaces;
	public int openTourismSpaces;
	public int filledTourismSpaces;
	public int roomLevel;
	public int roomID;
	public int miningCapability;
	public static int currentRoomID;
	private DigAndDebug digLevel;
	private PowerSystem powersystem;
	private MoonMan[] moonManArray;
	private Tourist[] touristArray;
	private Animator anim;
	
	//Backup Values for shutdown
	private float productivityCapabilitybackup;
	private float tourismCapabilitybackup;
	private float powerGenerationbackup;
	private float powerUsebackup;
	private int miningCapabilitybackup;
	private int LivingSpacesbackup;
	private int openWorkingSpacesbackup;
	private int openTourismSpacesbackup;
	
	
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		powersystem = FindObjectOfType<PowerSystem>();
	//Set the level and the ID for the room
		digLevel = FindObjectOfType<DigAndDebug>();
		roomID = currentRoomID;
		currentRoomID += 1;
		roomLevel = digLevel.currentLevel;
	//Set up the backups
		productivityCapabilitybackup = productivityCapability;
		tourismCapabilitybackup = tourismCapability;
		powerGenerationbackup = powerGeneration;
		powerUsebackup = powerUse;
		LivingSpacesbackup = LivingSpaces;
		openWorkingSpacesbackup = openWorkingSpaces;
		openTourismSpacesbackup = openTourismSpaces;
		miningCapabilitybackup = miningCapability;
		powerGenerationbackup = powerGeneration;	
		
	//Some other totaling stuff that will probably be overwritten	
		totalMiningCapability += miningCapability;
		totalTourismCapability += tourismCapability;
		totalProductivityCapability += productivityCapability;
		if (powerGeneration != 0){
			powersystem.totalPowerAvailable += powerGeneration;
		}
		if (powersystem.totalPowerUse+powerUse > powersystem.totalPowerAvailable && powerUse != 0 ) {
			shutDownRoom();
			
		}
		else{
			isActive = true;
		}
		
	}
	
	public void shutDownRoom(){
		isActive = false;
		anim.SetBool ("OutOfPower", true);
		productivityCapability = 0;
		tourismCapability = 0;
		LivingSpaces = 0;
		occupiedLivingSpaces = 0;
		openWorkingSpaces = 0;
		filledWorkingSpaces = 0;
		miningCapability = 0;
		powerUse = 0;
		openTourismSpaces = 0;
		filledTourismSpaces = 0;
		//Kick the moonmen out
		moonManArray = GameObject.FindObjectsOfType<MoonMan>();
			foreach (MoonMan moonMan in moonManArray){
				if (moonMan.homeLocation == this.gameObject){
					moonMan.homeLocation = null;
					moonMan.homeID = 0;
					moonMan.triggerFindLivingSpace();
				 } 
				 if (moonMan.workLocation == this.gameObject){
				 	moonMan.workLocation = null;
				 	moonMan.jobID = 0;
				 	moonMan.triggerFindWorkLocation();
				 }
			}
		touristArray = GameObject.FindObjectsOfType<Tourist> ();
		foreach (Tourist tourist in touristArray) {
			if (tourist.vacationAttraction == this.gameObject) {
				tourist.vacationDuration = 0;
				tourist.vacationAttraction = null;
				tourist.findNewAttraction();
			}
		}
			
	}
	
	public void reOpenRoom(){
		anim.SetBool ("OutOfPower", false);
		isActive = true;
		productivityCapability = productivityCapabilitybackup;
		tourismCapability = tourismCapabilitybackup;
		LivingSpaces = LivingSpacesbackup;
		openWorkingSpaces = openWorkingSpacesbackup;
		miningCapability = miningCapabilitybackup;
		powerUse = powerUsebackup;
		openTourismSpaces = openTourismSpacesbackup;
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
