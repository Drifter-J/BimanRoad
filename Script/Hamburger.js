#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif
function OnTriggerEnter (other : Collider){
	other.gameObject.SendMessage("Catchburger", 20);
	other.gameObject.SendMessage("setWalkSpeed", -0.3f);
	other.gameObject.SendMessage("setAniSpeed", -0.08f);
	Destroy(gameObject);
}