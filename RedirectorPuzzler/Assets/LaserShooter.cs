using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour {

	public Vector3 LaserDirection;

	public float MaxDistance = 10000f;
	public int MaxCount = 10;

	private LineRenderer liner;
	private Transform tf;

	// Use this for initialization
	void Start () {
		liner = gameObject.GetComponent<LineRenderer>();
		liner.startWidth = 0.1f;
		liner.endWidth = 0.1f;
		liner.startColor = Color.blue;
		liner.endColor = Color.blue;
		tf = gameObject.transform;
	}
	
	// Update is called once per frame
	private bool toggle = true;
	List<Vector3> points = new List<Vector3> ();
	void Update () {

		//Do it every second frame
		if (toggle) {
			points.Clear ();
			points.Add (tf.position);

			RaycastHit hit;
			RaycastHit lastHit;
			int lastLayer;
			Physics.Raycast(tf.TransformPoint (Vector3.zero), tf.TransformDirection(LaserDirection), out hit, MaxDistance);
			int count = 0;
			Vector3 direction = tf.up;
			bool end = false;
			while (hit.collider != null && count < MaxCount) {
				points.Add (hit.point);
				Redirector redir = hit.collider.gameObject.GetComponent<Redirector> ();
				if (redir != null) {
					direction = redir.Redirect(Vector3.Normalize(hit.point - points[points.Count-2]));
					points.Add (hit.collider.transform.position);
				} else {
					//direction = hit.normal;
					end  = true;
					break;
				}

				lastHit = hit;
				lastLayer = lastHit.collider.gameObject.layer;
				hit.collider.gameObject.layer = 2; //Ignore raycast
				Physics.Raycast(hit.point, direction, out hit, MaxDistance);
				lastHit.collider.gameObject.layer = lastLayer;
				++count;
			}
			if (count < MaxCount && !end) {
				points.Add (points [points.Count - 1] + direction * MaxDistance);
			}

			liner.SetPositions (points.ToArray ());
			liner.positionCount = points.Count;
		}
		//toggle = !toggle;
	}
}
