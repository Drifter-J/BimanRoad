using UnityEngine;
using System.Collections;

public class FinishGame : MonoBehaviour {
	private GameController otherScript;
	public GameObject finishText;
	public GameObject currentEffects;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			Instantiate(currentEffects,new Vector3(1357.69f, 23.53f, 878.05f),Quaternion.identity);
			otherScript = (GameController)finishText.GetComponent(typeof(GameController));
			otherScript.isGameFinish = true;			
		}
	}
}