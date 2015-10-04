using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour {
	
	
	Transform pos;
	public Transform end,start;
	public float duration;
	TweenAlpha ta;
	TweenTransform flagTT;
	// Use this for initialization
	void Start () {
		
		GameManager gm = GameManager.Instanace;
		pos = gm.baymax.transform;
		transform.parent = this.transform;
		
		flagTT = GetComponent<TweenTransform> ();
		flagTT.from = start;
		flagTT.to = end;
		flagTT.duration = duration;
		flagTT.enabled = true;
		ta = GetComponent<TweenAlpha> ();
		
		gm.tweenscript.Add (flagTT);
		GetComponent<UI2DSprite> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!ta.enabled) {
			if (pos.position.y < transform.position.y) {
				
				ta.duration = flagTT.duration - 6;
				GameManager.Instanace.tweenscript.Add(ta);
				ta.from = 1.0f;
				ta.to = 0.4f;
				ta.enabled = true;
				GetComponent<UIWidget>().depth=2;
			}
		}
	}
	
	
	public void destroy_obj(){
		
		Destroy(gameObject);
	}
}
