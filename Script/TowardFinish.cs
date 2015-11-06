using UnityEngine;
using System.Collections;

public class TowardFinish : MonoBehaviour {
	//where we want to travel to.
	private Vector3 finalPos;
	float x = 0f;
	float y = -7000f;
	float z = 100f;
	public GameObject finishLine;
	// Update is called once per frame
	void Update () {
		Rotate ();
	}
	void Rotate(){
		finalPos = this.transform.position;
		finalPos.x = x;
		finalPos.y = y;
		finalPos.z = finishLine.transform.position.z;
		this.transform.LookAt (finalPos);
	}
}
