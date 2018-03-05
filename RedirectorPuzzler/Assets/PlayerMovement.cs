using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool Selected;

	private AnimationClip upAnimation;
	private AnimationClip leftAnimation;
	private AnimationClip downAnimation;
	private AnimationClip rightAnimation;

	private bool isPlaying;
	private Animation currentAnimation;

	private Transform animationTransform;
	private Transform modelTransform;

	private Vector3 startPosition;
	private Quaternion startRotation;

	// Use this for initialization
	void Start () {
		isPlaying = false;

		currentAnimation = gameObject.GetComponentInChildren<Animation>();
		upAnimation = currentAnimation.GetClip("MoveOneSquareUp");
		leftAnimation = currentAnimation.GetClip("MoveOneSquareLeft");
		downAnimation = currentAnimation.GetClip("MoveOneSquareDown");
		rightAnimation = currentAnimation.GetClip("MoveOneSquareRight");

		animationTransform = currentAnimation.gameObject.transform;
		startPosition = animationTransform.localPosition;
		startRotation = animationTransform.localRotation;

		modelTransform = transform.GetChild(0).GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying) {
			if (!currentAnimation.isPlaying) {
				isPlaying = false;
				UpdatePositionToAnimation();
			}
		} else if (Selected) {
			isPlaying = true;
			if (Input.GetKey (KeyCode.W)) {
				currentAnimation.Play (upAnimation.name);
			} else if (Input.GetKey (KeyCode.A)) {
				currentAnimation.Play (leftAnimation.name);
			} else if (Input.GetKey (KeyCode.S)) {
				currentAnimation.Play (downAnimation.name);
			} else if (Input.GetKey (KeyCode.D)) {
				currentAnimation.Play (rightAnimation.name);
			} else {
				isPlaying = false;
			}
		}
	}

	void UpdatePositionToAnimation() {
		gameObject.transform.localPosition += animationTransform.localPosition;
		modelTransform.localRotation = modelTransform.rotation;
		animationTransform.localPosition = startPosition;
		animationTransform.localRotation = startRotation;
	}


	public void SelectPlayer() {
		UnselectAllPlayers ();
		Selected = true;
	}

	private void UnselectAllPlayers() {
		foreach (PlayerMovement player in gameObject.transform.parent.GetComponentsInChildren<PlayerMovement>()) {
			player.Selected = false;
		}
	}
}