using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {
	public Camera cam;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Player") {
					hit.collider.gameObject.transform.parent.parent.GetComponent<PlayerMovement> ().SelectPlayer ();
				}
			}
		}
	}
}
