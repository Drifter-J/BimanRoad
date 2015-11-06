using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoverAction : MonoBehaviour {

	public GameObject StartGame;
	public GameObject Ranking;
	public GameObject HoverStartGame;
	public GameObject HoverRanking;
	public GameObject rankCanvas;
	
	static bool isDataArrive = false;
	static string[] RankDataArray;

	// Use this for initialization
	public void OnClickRanking(){
		HoverRanking.SetActive(true);
		Ranking.SetActive(false);
		rankCanvas.SetActive(true);
	}

	public void OnClickStartGame(){	
		HoverStartGame.SetActive(true);	
		StartGame.SetActive(false);
		Application.LoadLevel("MainGame");
	}

	//when rank button is pressed, call ShowData once using an isDataArrive flag
	public void Update(){
		if (isDataArrive) {
			ShowData ();
		}
	}
	
	//parse receieve data with ':' and put them into a RankDataArray
	public static void ParseData (string rankData) {
		RankDataArray = rankData.Split(':');
		isDataArrive = true;
	}
	
	//each string in a RankDataArray will be shown on the screen
	public void ShowData(){
		int j = 0;
		int cnt = 0;
		int index = 1;
		int index2 = 1;
		
		foreach (string s in RankDataArray){
			isDataArrive = false;
			/*last string of a RankDataArray must be ignored
			 * if not it will find a text object that doesn't exist
			 * and cuase nullreferenceexception
			 */ 
			if(j==15) break;
			else j++;
			
			if(cnt==0){
				cnt++;
				continue;
			}
			else if(cnt==1){
				string Text = string.Concat("userId (", index.ToString(), ")");
				GameObject text = GameObject.Find (Text);
				Text textUpdate = text.GetComponent<Text> ();
				textUpdate.text = s;
				index++;
				cnt++;
			}
			else if(cnt==2){
				string Text = string.Concat("winRate (", index2.ToString(), ")");
				GameObject text = GameObject.Find (Text);
				Text textUpdate = text.GetComponent<Text> ();
				textUpdate.text = s;
				index2++;
				cnt=1;
			}			
		}		
	}
}
