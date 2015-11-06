using UnityEngine;
using System.Collections;

[System.Serializable]
public class HTSequence {
	
	[SerializeField]
	public GameObject spriteSheet;
	[SerializeField]
	public Vector3 offset;
	[SerializeField]
	public float waittingTime;
	[SerializeField]
	public bool foldOut=true;
	[SerializeField]
	public Color color;
	[SerializeField]
	public GameObject spriteSheetRef;
	[SerializeField]
	public GameObject oldSpriteSheetRef;
	[SerializeField]
	public bool play=false;

}
