using UnityEngine;
using System.Collections;

public class TreeMaker : MonoBehaviour {
	public GameObject TreeTexture;
	
	public Transform leftTreeInitPos;
	public Transform leftTreeEndPos;
	public Transform rightTreeInitPos;
	public Transform rightTreeEndPos;

	// Update is called once per frame
	void Update () {
	}



	public void treeMakerStart(){
		Invoke ("treeRightInit", 2f);
		Invoke ("treeLeftInit", 2f);
	}
	private void treeRightInit(){
		
		GameObject tree= GameObject.Instantiate (TreeTexture,rightTreeInitPos.position,Quaternion.identity) as GameObject;
		tree.transform.SetParent (this.transform);
		tree.transform.localScale = new Vector3 (1, 1, 1);
		TreeScript ts= tree.GetComponent<TreeScript> ();
		ts.duration = GameManager.Instanace.mainDuration;
		ts.start = rightTreeInitPos;
		ts.end = rightTreeEndPos;


		Invoke ("treeRightInit",Random.Range (0.8f, 1.5f) );
	}
	private void treeLeftInit(){
		GameObject tree= GameObject.Instantiate (TreeTexture,leftTreeInitPos.position,Quaternion.identity) as GameObject;
		tree.transform.SetParent (this.transform);
		tree.transform.localScale = new Vector3 (1, 1, 1);
		TreeScript ts= tree.GetComponent<TreeScript> ();
		ts.duration = GameManager.Instanace.mainDuration;
		ts.start = leftTreeInitPos;
		ts.end = leftTreeEndPos;

		Invoke ("treeLeftInit",Random.Range (0.8f, 1.5f) );
	}


}
