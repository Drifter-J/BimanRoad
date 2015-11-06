using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour {	
	static string[] LoginDataArray;
	public static bool flag = false;

	string uid;
	string pw;	
	// Update is called once per frame
	public void UsernameUpdate () {
		GameObject inputFieldGo = GameObject.Find("Username");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField> ();
		uid = inputFieldCo.text;
		Server.setUid (uid);
	}

	public void PasswordUpdate () {
		GameObject inputFieldGo = GameObject.Find("Password");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField> ();
		pw = inputFieldCo.text;
		Server.setPw (pw);
	}

	public static void OnClickLogin(string returnData){
		LoginDataArray = returnData.Split(':');
	}
}
