using UnityEngine;
using System.Collections;

public class BayMax : MonoBehaviour {

	public Vector3 velocity;
	public Vector3 accel;
	const float ACCELMAX = 1f;
	// Use this for initialization
	void Start () {
		velocity = new Vector3 (0, 0, 0);

	}

	public void MoveLeft(){
		if (velocity.x < -3)
			return;
		velocity.x = velocity.x - 0.3f;
		
		float scaleX = transform.localScale.x;

		if (scaleX  < 0) {
			transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
		}
	}
	public void MoveRight(){
		if (velocity.x > 3)
			return;
		velocity.x = velocity.x + 0.3f;
		
		float scaleX = transform.localScale.x;

		if (scaleX > 0) {
			transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
		}
	}
	public TweenTransform JumpAnimation;
	public void Jump(){
		if (!JumpAnimation.enabled) {
			JumpAnimation.ResetToBeginning();
			JumpAnimation.Play ();
		}

	}

	void Update(){
		float posX = transform.localPosition.x;
		float posY = transform.localPosition.y;
		Vector3 oldPos;

		if (posX < -300 || posX > 300) {
			velocity.x=0;
			if (posX < -300) {
				
				oldPos = new Vector3 (-295, posY, 0);
			} else {
				oldPos = new Vector3 (295, posY, 0);

			}
		} else {
			oldPos = transform.localPosition + velocity;
		}

		transform.localPosition = oldPos;
	}
}
