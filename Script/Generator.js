#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif
var SpawnObject : GameObject;
var SpawnPoint : GameObject;

var SpawnCounter : int = 0;
var SpawnCounterMax : int = 0;

private var timer : float = 0.0f;

function Update(){
	timer += Time.deltaTime;
	//if(isCounting && timer > 4){
	if(timer > 4){
	this.SpawnCounter++;
	if(this.SpawnCounter <= this.SpawnCounterMax){
		var rndPosWithin : Vector3;
        rndPosWithin = Vector3(Random.Range(-10f, 10f), Random.Range(10f, 30f), Random.Range(0f, 100f));
		rndPosWithin = transform.TransformPoint(rndPosWithin);
		if(this.SpawnCounter%8==0){
			Instantiate(this.SpawnObject, rndPosWithin, transform.rotation);
			}
	}
	else {SpawnCounter=0;}
	}
}