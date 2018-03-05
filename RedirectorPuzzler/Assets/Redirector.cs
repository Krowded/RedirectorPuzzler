using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirector : MonoBehaviour {

	public Vector3 RedirectDirection;

	void Start() {
	}

	public virtual Vector3 Redirect(Vector3 InDirection) {
		return gameObject.transform.up;
	}
}
