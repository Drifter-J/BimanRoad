#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif


var burgerFx : AudioClip;
var cakeFx : AudioClip;	
var OreoFx : AudioClip;

function Catchburger(amount : int) {
	GetComponent.<AudioSource>().PlayOneShot(burgerFx);
}

function CatchCake(amount : int) {
	GetComponent.<AudioSource>().PlayOneShot(cakeFx);
}

function CatchOreo(amount : int) {
	GetComponent.<AudioSource>().PlayOneShot(OreoFx);
}	