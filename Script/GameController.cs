using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GUITexture startGame;
	public GUITexture Finish;
	
	public GUITexture count1;
	public GUITexture count2;
	public GUITexture count3;

	public float switchInterval;
	public bool isGameFinish;

	public int score;


	// Use this for initialization
	void Start () {
		startGame.enabled = false;		
		count1.enabled = false;
		count2.enabled = false;
		count3.enabled = false;
		Finish.enabled = false;
		isGameFinish = false;

		StartCoroutine(getReady());
	}
	
	IEnumerator getReady(){

		count3.enabled = true;
		yield return new WaitForSeconds(1f);
		count3.enabled = false;
		count2.enabled = true;
		yield return new WaitForSeconds(1f);  
		count2.enabled = false;
		count1.enabled = true;;
		yield return new WaitForSeconds(1f);  
		count1.enabled = false;
		startGame.enabled = true;
		yield return new WaitForSeconds (1f);
		startGame.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (isGameFinish == true) {

			GameFinish();
		}
		if(isGameFinish && (Input.GetKey(KeyCode.Mouse0))){
			Application.LoadLevel("MainMenu");
		}	
	}

	void GameFinish() {
		Finish.enabled = true;
		StartCoroutine(waitAndDestroy(3f));
		//Finish.enabled = false;
	}
	IEnumerator waitAndDestroy(float duration){
		yield return new WaitForSeconds(duration);
		Application.LoadLevel("MainMenu");
	}
}
