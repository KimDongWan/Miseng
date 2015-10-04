using UnityEngine;
using System.Collections;



public class FlagMaker : MonoBehaviour {
	public GameObject flagTexture;
	public Transform[] flagInitialPosition;
	public Transform[] flagEndPosition;
	public ArrayList durations;

	public void flagMakerStart(){
		durations=new ArrayList();
		Invoke ("flagGeneration", 2f);
	}

	void flagGeneration(){
		
		float duration = 10f;
		float range=Random.Range (-305f, 305f);
	
		int num=Random.Range (0, 20);




		GameObject flagBlock=GameObject.Instantiate(flagTexture,flagInitialPosition[num%3].localPosition,Quaternion.identity) as GameObject;
		flagBlock.transform.parent = transform;
		flagBlock.transform.localScale = new Vector3 (1, 1, 1);
		FlagScript fs= flagBlock.GetComponent<FlagScript> ();
		fs.duration = GameManager.Instanace.mainDuration;
		fs.start = flagInitialPosition[num%3];
		fs.end = flagEndPosition[num%3];

		Invoke ("flagGeneration",4-(float)GameManager.Instanace.gameTimer/30);
	}
	
	public void destroy_obj(){
		
		Destroy(gameObject);
	}

}
