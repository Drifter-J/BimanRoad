using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {	
	static bool isDataArrive = false;
	static string[] RankDataArray;

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
		int index3 = 1;
		int index4 = 1;

		foreach (string s in RankDataArray){
			isDataArrive = false;
			/*last string of a RankDataArray must be ignored
			 * if not it will find a text object that doesn't exist
			 * and cuase nullreferenceexception
			 */ 
			if(j==25) break;
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
				cnt++;
			}
			else if(cnt==3){
				string Text = string.Concat("winGame (", index3.ToString(), ")");
				GameObject text = GameObject.Find (Text);
				Text textUpdate = text.GetComponent<Text> ();
				textUpdate.text = s;
				index3++;
				cnt++;
			}
			else if(cnt==4){
				int loseGame = int.Parse(s);
				string Text = string.Concat("winGame (", index4.ToString(), ")");
				GameObject text = GameObject.Find (Text);
				Text textUpdate = text.GetComponent<Text> ();
				int winGame = int.Parse(textUpdate.text);

				string Text2 = string.Concat("totalGame (", index4.ToString(), ")");
				GameObject text2 = GameObject.Find (Text2);
				Text textUpdate2 = text2.GetComponent<Text> ();
				textUpdate2.text = (loseGame+winGame).ToString();
				index4++;
				cnt=1;
			}			
		}		
	}
}
