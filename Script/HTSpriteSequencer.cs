using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HTSpriteSequencer : MonoBehaviour {
	
	#region Public members
	[SerializeField]
	public List<HTSequence> sequences = new List<HTSequence>();
	public HTSpriteSheet.CameraFacingMode billboarding = HTSpriteSheet.CameraFacingMode.BillBoard;
	public bool autoDestruct=true;
	public bool editorMode=false;
	#endregion
	
	#region Private members
	private float startTime;
	private List<GameObject> effects = new List<GameObject>();
	private Transform myTransform;
	private Transform mainCamTransform;
	private int inPlayingCount=0;
	#endregion
	
	#region Monobehaviors methods
	void Awake(){
		mainCamTransform = Camera.main.transform;
	}

	void Start(){
		startTime = Time.time;
		myTransform = transform;
	}

	void Update(){

		foreach (HTSequence seq in sequences){
			if (Time.time - startTime> seq.waittingTime && !seq.play){
				if (seq.spriteSheet!=null){
					GameObject effet = (GameObject)Instantiate( seq.spriteSheet,myTransform.position,myTransform.rotation);	
					effet.transform.parent = myTransform;
					effet.transform.localPosition= new Vector3(seq.offset.x*-1, seq.offset.y, seq.offset.z);
					effects.Add( effet);
					seq.play=true;
					inPlayingCount++;
					// Inspector copy
					if (Application.isEditor && Application.isPlaying && editorMode){
						HTSpriteSheet ss = seq.spriteSheet.GetComponent<HTSpriteSheet>();
						ss.offset = effet.transform.position-myTransform.position;
						ss.waittingTime = seq.waittingTime;
						ss.copy=true;
					}
				}
			}
		}
		
		
		// Destroy 
		if (autoDestruct){
			int endCount=0;
			if (inPlayingCount==sequences.Count){
				foreach( GameObject effect in effects){
					if (effect==null){
					endCount++;	
					}
				}
				if (endCount==inPlayingCount){
					Destroy(gameObject);	
				}
			}
		}
		
		Camera_BillboardingMode();
		 
	}
	
	void OnDrawGizmos(){
		
		bool childSeq=false;
		
		foreach (HTSequence seq in sequences){
			if (seq.spriteSheet!=null){
				HTSpriteSheet sprite =  seq.spriteSheet.GetComponent<HTSpriteSheet>();
				Gizmos.color= seq.color;
				Gizmos.DrawWireCube (transform.position + seq.offset, new Vector3 (sprite.sizeEnd.x/2,sprite.sizeEnd.y/2,0) );
				childSeq=true;
			}
		}
		
		if(!childSeq){
			Gizmos.DrawWireCube (transform.position, new Vector3 (1,1,0) );
		}
		
	}
	
	#endregion
	
	#region private methods
	void Camera_BillboardingMode(){
		
		
		Vector3 lookAtVector =   myTransform.position-mainCamTransform.position ;
         
		switch (billboarding){
			case HTSpriteSheet.CameraFacingMode.BillBoard:
				myTransform.LookAt( mainCamTransform.position - lookAtVector); 
				break;
			case HTSpriteSheet.CameraFacingMode.Horizontal:
				lookAtVector.x = lookAtVector.z =0 ;
				myTransform.LookAt(mainCamTransform.position - lookAtVector);
				break;
			case HTSpriteSheet.CameraFacingMode.Vertical:
				lookAtVector.y=lookAtVector.z =0;
				myTransform.LookAt(mainCamTransform.position - lookAtVector);
				break;
		}	
		
		
	}
	#endregion
	
	#region public methods
	public void KillAllSequences(){
		
		for(int i=0;i<effects.Count;i++){
			Destroy(effects[i]);
		}
		effects.Clear();
		startTime = Time.time;
		foreach (HTSequence seq in sequences){
			seq.play=false;
		}
		
	}
	#endregion
	
}
