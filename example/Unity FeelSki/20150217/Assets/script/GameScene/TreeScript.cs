using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {



	Transform pos;
	public Transform end,start;
	public float duration;
	TweenAlpha ta;
	TweenTransform treeTT;
	// Use this for initialization
	void Start () {

		GameManager gm = GameManager.Instanace;
		pos = gm.baymax.transform;
		transform.parent = this.transform;

		treeTT = GetComponent<TweenTransform> ();
		treeTT.from = start;
		treeTT.to = end;
		treeTT.duration = duration;
		treeTT.enabled = true;
		ta = GetComponent<TweenAlpha> ();

		gm.tweenscript.Add (treeTT);
		GetComponent<UI2DSprite> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!ta.enabled) {
			if (pos.position.y < transform.position.y) {

				ta.duration = treeTT.duration - 6;
				GameManager.Instanace.tweenscript.Add(ta);
				ta.from = 1.0f;
				ta.to = 0.4f;
				ta.enabled = true;
			}
		}
	}


	public void destroy_obj(){

		Destroy(gameObject);
	}
}
