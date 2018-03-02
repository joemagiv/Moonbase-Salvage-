using UnityEngine;
using System.Collections;

public class DigAndDebug : MonoBehaviour {

	public int currentLevel = 1;
	private int nextLevel = 2;
	private Vector3 nextLevelPlacement;
	public GameObject emptylevelPrefab;
	public GameObject initialLevel;
	

	// Use this for initialization
	void Start () {
		nextLevelPlacement = initialLevel.transform.position;
		nextLevelPlacement.y -= 1;
	}
	
	public void DigDown(){
		Instantiate(emptylevelPrefab, nextLevelPlacement, Quaternion.identity);
		currentLevel += 1;
		nextLevel += 1;
		nextLevelPlacement.y -= 1;
	}
	
	

	
	// Update is called once per frame
	void Update () {
	
	}
}
