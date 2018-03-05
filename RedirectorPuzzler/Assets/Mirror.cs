using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Redirector {
	public override Vector3 Redirect(Vector3 InDirection) {
		Debug.Log (InDirection);
		if (Vector3.Dot (InDirection, gameObject.transform.right) < 0) {
			return Vector3.Reflect (InDirection, gameObject.transform.right);
		} else {
			return Vector3.Reflect (InDirection, -gameObject.transform.right);
		}

	}
}
