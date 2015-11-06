#if UNITY_EDITOR
private var RemoveUnusedNameSpaceWarningsq:Queue;
private var RemoveUnusedNameSpaceWarningsh:Help;
private var RemoveUnusedNameSpaceWarningsg:GUI;
#endif

function OnTriggerEnter (other : Collider){
	other.gameObject.SendMessage("OnTrack", 2);
	other.gameObject.SendMessage("setWalkSpeed", 0.04f);
}