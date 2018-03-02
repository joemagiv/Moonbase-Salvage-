using UnityEngine;
using System.Collections;

public class MoonManSpawner : MonoBehaviour {

	public GameObject moonmanPrefab;
	public GameObject MoonmanParent;
	public GameObject touristPrefab;
	private GameObject moonManSpawner;
	public float moonmanSpawnRate;

	// Use this for initialization
	void Start () {
		moonManSpawner = GameObject.Find ("MoonManSpawner");
	}
	
	public void spawnMoonMan(){
		GameObject moonMan = Instantiate (moonmanPrefab, moonManSpawner.transform.position, Quaternion.identity) as GameObject;
		moonMan.transform.parent = MoonmanParent.transform;
	}

	public void spawnTourist(){
		GameObject tourist = Instantiate (touristPrefab, moonManSpawner.transform.position, Quaternion.identity) as GameObject;
		tourist.transform.parent = MoonmanParent.transform;
	}

	public void CalculateMoonManSpawn(){
		if (Room.totaloccupiedLivingSpaces < Room.totalLivingSpaces && Room.totalFilledWorkingSpaces < Room.totalOpenWorkingSpaces) {
			float randomValue = Random.value;
			if (moonmanSpawnRate > randomValue) {
				Debug.Log ("Open spaces available in both living and working, roll passed " + moonmanSpawnRate.ToString() + "/" + randomValue.ToString() +  " spawn moonman");
				spawnMoonMan ();

			} else {
				Debug.Log ("Open spaces available in both living and working, roll failed " + moonmanSpawnRate.ToString() + "/" + randomValue.ToString() +  " do not spawn moonman");
			}
		}
		else {
			Debug.Log ("No space available. Not Spawning moonman.");
		}

	}

	public void CalculateTouristSpawn(){
		if (Room.totalFilledTourismSpaces < Room.totalOpenTourismSpaces) {
			float touristSpawnRate = (0.5f * Room.totalOpenTourismSpaces / (Room.totalFilledTourismSpaces + 1));
			float randomValue = Random.value;
			if (touristSpawnRate > randomValue) {
				Debug.Log ("Open spaces available in Tourism, roll passed " + touristSpawnRate.ToString() + "/" + randomValue.ToString() +  " spawn tourist");
				spawnTourist ();

			} else {
				Debug.Log ("Open spaces available in Tourism, roll failed " + touristSpawnRate.ToString() + "/" + randomValue.ToString() +  " do not spawn tourist");
			}
		}
		else {
			Debug.Log ("No space available. Not Spawning Tourist.");
		}

	}



	
	// Update is called once per frame
	void Update () {
	
	}
}
