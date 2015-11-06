using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {
	public GameObject rankButton;
	public void OnClick(){
		GameObject.Find("RankingCanvas").SetActive(false);
		GameObject.Find("HoverRanking").SetActive(false);
		rankButton.SetActive(true);
	}
}
