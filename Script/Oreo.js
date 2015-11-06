#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif
function OnTriggerEnter (other : Collider){
	other.gameObject.SendMessage("CatchOreo", 10);
	other.gameObject.SendMessage("setWalkSpeed", -0.2f);
	other.gameObject.SendMessage("setAniSpeed", -0.05f);
	Destroy(gameObject);
}