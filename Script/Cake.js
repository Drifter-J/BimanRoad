#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif
function OnTriggerEnter (other : Collider){
	other.gameObject.SendMessage("CatchCake", 30);
	other.gameObject.SendMessage("setWalkSpeed", -0.5f);
	other.gameObject.SendMessage("setAniSpeed", -0.15f);
	Destroy(gameObject);
}