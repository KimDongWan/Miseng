using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {
	public GameObject target;

	public bool _x,_y,_z;

	// Use this for initialization

	// Update is called once per frame
	void Update () {
		Vector3 _tpos = target.transform.localPosition;
		Vector3 _cpos = transform.localPosition;
		if (_x) {
			
			transform.localPosition=new Vector3(_tpos.x,_cpos.y,_cpos.z);
		}
		if (_y) {
			transform.localPosition=new Vector3(_cpos.x,_tpos.y,_cpos.z);

		}
		if (_z) {
			transform.localPosition=new Vector3(_cpos.x,_cpos.y,_tpos.z);

		}


	}
}
