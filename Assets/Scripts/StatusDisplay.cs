using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour {

	private Text displayText;
	private ValueTracker valueTracker;
	private PowerSystem powersystem;
	
	// Use this for initialization
	void Start () {
		displayText = GetComponent<Text>();
		valueTracker = GameObject.FindObjectOfType<ValueTracker>();
		powersystem = FindObjectOfType<PowerSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		displayText.text = ("Population: " + Room.totaloccupiedLivingSpaces + "\t\t\t\tMoon Rocks: " + valueTracker.totalMoonRocks.ToString("F0") 
			+ "\nPower Use: " + powersystem.totalPowerUse + "/" + powersystem.totalPowerAvailable + "\t\t\tTourists: " + Room.totalFilledTourismSpaces
			+"\nCash: " + valueTracker.totalCash.ToString("F2") + "\t\t\t\t\tClock: " + ValueTracker.gameClock);
	}
}
