#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif

public var currentEffects : GameObject;

private var life : int = 0;
public var score : int = 30;
private var OnTrackCnt : int = 0; 
private var timer : float = 0.0f;
private var timerFloat : float = 0;
private var loseCal : float = 0.0f;
private var timeEnd : float = 0f;
var style : GUIStyle;
var style2 : GUIStyle;
var style3 : GUIStyle;
public var finishScreen : GameObject;


private var isGameOver : boolean = false;
public var textGameOver : Texture2D;
var animator : Animator;
animator = GetComponent(Animator);

var nextUsage : float;

function Start(){	
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	StartCoroutine(ChangeFontColor(5));
}

function ChangeFontColor(time:float){
	var elapsedTime : float = 0;
	//lerp col
	while(elapsedTime < time){
	Debug.Log("changeColor");
		style3.normal.textColor = Color.Lerp(Color.yellow, Color.red, elapsedTime/time); 
		elapsedTime += Time.deltaTime;
	}
	yield WaitForSeconds(time);
}

function Catchburger(amount : int) {
	score += amount;
	checkScore();
	//Destroy(transform.parent.gameObject);
}

function CatchCake(amount : int) {
	score += amount;
	checkScore();
	//Destroy(transform.parent.gameObject);
}

function CatchOreo(amount : int) {
	score += amount;
	checkScore();
	//Destroy(transform.parent.gameObject);
}

function OnTrack(amount : int) {
	OnTrackCnt++;
	if(OnTrackCnt==3 && score >= 1){
		score -= amount;
		loseCal += amount;
		if(loseCal%5==0){
			transform.localScale -= new Vector3 (0.03,0.01,0.01);
			setAniSpeedPerCal(score);
		}
		OnTrackCnt = 0;
	}
}

function Update() {
	timer += Time.deltaTime;
	//if(isCounting && timer > 4){
	if(timer > 4 && !isGameOver && !(finishScreen.GetComponent(GUITexture)as GUITexture).enabled){
      timerFloat = System.Math.Round(timer-3,1);
  	}	
  	if (score >= 150){
		isGameOver = true;
		Debug.Log(isGameOver);
	}
	if(isGameOver && (Input.GetKey(KeyCode.Mouse0))){
		//Application.LoadLevel("MainMenu");
	} 
}

function OnGUI() {
	var rect : Rect = Rect(0, 0, Screen.width, Screen.height);
	if(score>=100){	
		GUI.Label(rect, "Calorie: " + score.ToString(), style3);
	}
	else
		GUI.Label(rect, "Calorie: " + score.ToString(), style);
		
		GUI.Label(rect, "Score: " + timerFloat.ToString(), style2);
	if(isGameOver){
		GUI.Label(Rect(Screen.width/2,Screen.height,800,159),textGameOver);
		nextUsage = Time.time + 100;
	}
}

function setAniSpeed(amount : float){
	if(animator.speed>0.5){	
		Debug.Log("setAniSpeed:"+animator.speed);
	animator.speed += amount;
	}
}

function loseScore(amount : int ){	
	score -= amount;
}

function setAniSpeedPerCal(amount : int){
Debug.Log("setAniSpeedPerCal:"+animator.speed);
	if(score <= 30){
		animator.speed = 1.3;
	}
	else if(score <=50){
		animator.speed = 0.9;
	}
	else if(score <= 70){
		animator.speed = 0.7;
	}
	else if(score <= 85){
		animator.speed = 0.5;
	}
	else if(score <= 100){
		animator.speed = 0.4;
	}
}
function checkScore(){
	if(score <= 50){
		transform.localScale += new Vector3 (0.05,0.01,0.01);
		setAniSpeed(-0.1);
		Instantiate(currentEffects,transform.position,Quaternion.identity);
	}
	else if(score <= 80){
		transform.localScale += new Vector3 (0.08,0.02,0.02);
		setAniSpeed(-0.3);
		Instantiate(currentEffects,transform.position,Quaternion.identity);
	}
	else if(score <= 100){
		transform.localScale += new Vector3 (0.12,0.02,0.02);
		setAniSpeed(-0.5);
		Instantiate(currentEffects,transform.position,Quaternion.identity);
	}
	else if(score <= 130){
		transform.localScale += new Vector3 (0.18,0.05,0.05);
		setAniSpeed(-0.7);
		Instantiate(currentEffects,transform.position,Quaternion.identity);
	}
	else if(score <= 160){
		transform.localScale += new Vector3 (0.21,0.08,0.08);
		setAniSpeed(-0.9);
		Instantiate(currentEffects,transform.position,Quaternion.identity);
	}
	else if(score <= 190){
		transform.localScale += new Vector3 (0.25,0.1,0.1);
		setAniSpeed(-1.0);
		Instantiate(currentEffects,transform.position,Quaternion.identity);
	}
}