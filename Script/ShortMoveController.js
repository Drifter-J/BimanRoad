#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif
var velocity : float = 8.0;		
var moveDelay : float = 1.0;	
var sustainTime : float = 1.0;	

function Start() {
	yield WaitForSeconds(moveDelay);
	
	var player : GameObject = GameObject.FindWithTag("Player");
	if (player != null) {
		var direction : Vector2 = (player.transform.position - transform.position).normalized;
		GetComponent.<Rigidbody>().AddForce(direction * velocity, ForceMode.VelocityChange);
	}
	yield WaitForSeconds(sustainTime);
	Destroy(gameObject);
}
