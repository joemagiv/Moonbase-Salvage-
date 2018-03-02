using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	public float dragSpeed = 2;
	private Vector3 dragOrigin;
	public static bool UIactive = false;
	
	public bool cameraDragging = true;
	
	public float outerTop = -10f;
	public float outerBottom = 10f;
	
	
	void Update()
	{
		if (!UIactive) {
		
		
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		
			float top = Screen.height * 0.2f;
			float bottom = Screen.height - (Screen.height * 0.2f);
		
			if (mousePosition.y < top) {
				cameraDragging = true;
			} else if (mousePosition.y > bottom) {
				cameraDragging = true;
			}
		
		
		
		
		
		
			if (cameraDragging) {
			
				if (Input.GetMouseButtonDown (0)) {
					dragOrigin = Input.mousePosition;
					return;
				}
			
				if (!Input.GetMouseButton (0))
					return;
			
				Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - dragOrigin);
				Vector3 move = new Vector3 (0, -pos.y * dragSpeed, 0);
			
				if (move.y > 0f) {
					if (this.transform.position.y < outerBottom) {
						transform.Translate (move, Space.World);
					}
				} else {
					if (this.transform.position.y > outerTop) {
						transform.Translate (move, Space.World);
					}
				}
			}
		}
	
	}
}