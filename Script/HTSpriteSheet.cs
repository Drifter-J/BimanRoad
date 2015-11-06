using UnityEngine;
using System.Collections;

public class HTSpriteSheet : MonoBehaviour {
	
	#region enumeration
	public enum CameraFacingMode{ 
		BillBoard, 
		Horizontal,
		Vertical,
		Never 
	};
	#endregion
	
	#region public properties

	public Material spriteSheetMaterial;	
	public int spriteCount;
	public int uvAnimationTileX;
	public int uvAnimationTileY;
	public int framesPerSecond;
	public Vector3 sizeStart = new Vector3(1,1,1);
	public Vector3 sizeEnd = new Vector3(1,1,1);
	public bool randomRotation;
	public float rotationStart;
	public float rotationEnd;
	public bool isOneShot=true;
	public float life=0;
	public CameraFacingMode billboarding; 
	public bool addLightEffect=false;
	public float lightRange;
	public Color lightColor;
	public float lightFadeSpeed=1;
	public bool addColorEffect;
	public Color colorStart = new Color(1f,1f,1f,1f);
	public Color colorEnd = new Color(1f,1f,1f,1f);
	public bool foldOut=false;
	

	public Vector3 offset;
	public float waittingTime;
	public bool copy=false;
	#endregion
	
	#region private properties
	private Mesh mesh;
	private MeshRenderer meshRender;
	private AudioSource soundEffect;
	private float startTime;
	private Transform mainCamTransform;
	private bool effectEnd=false;
	private float randomZAngle;
	private Color colorStep;
	private Color currentColor;
	private Vector3 sizeStep;
	private Vector3 currentSize;
	private float currentRotation;
	private float rotationStep;
	private float lifeStart;
	private Transform myTransform;
	#endregion
	
	#region MonoBehaviour methods
	void Awake(){
		
		// Creation of the particle
		CreateParticle();
		
		// We search the main camera
		mainCamTransform = Camera.main.transform;
		
		// do we have sound effect ?
		soundEffect = GetComponent<AudioSource>();
		
		// Add light
		if (addLightEffect){
			gameObject.AddComponent<Light>();
			gameObject.GetComponent<Light>().color = lightColor;
			gameObject.GetComponent<Light>().range = lightRange;
		}
		
		GetComponent<Renderer>().enabled = false;
		

	}
	
	// Use this for initialization
	void Start () {
	
		InitSpriteSheet();
	}

	void Update () {
		
		bool end=false;
		
		Camera_BillboardingMode();

    	float index = (Time.time-startTime) * framesPerSecond;
		

		if (!isOneShot && life>0 && (Time.time -lifeStart)> life){
			effectEnd=true;
		}
		
		if ((index<=spriteCount || !isOneShot ) && !effectEnd ){

		   		
			if (index >= spriteCount){
				startTime = Time.time;	
				index=0;
				if (addColorEffect){
					currentColor = colorStart;
					meshRender.material.SetColor("_Color", currentColor);
				}
				currentSize = sizeStart;
				myTransform.localScale = currentSize;
				
				if (randomRotation){
					currentRotation = Random.Range(-180.0f,180.0f);
				}
				else{
					currentRotation = rotationStart;
				}
			}
			// repeat when exhausting all frames
		    index = index % (uvAnimationTileX * uvAnimationTileY);
			
			
		    // Size of every tile
		    Vector2 size = new Vector2 (1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);
		   
		    // split into horizontal and vertical index
		    float uIndex = Mathf.Floor(index % uvAnimationTileX);
		    float vIndex = Mathf.Floor(index / uvAnimationTileX);
		
		    // build offset
		    Vector2 offset = new Vector2 (uIndex * size.x , 1.0f - size.y - vIndex * size.y);
			
		   	GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
		   	GetComponent<Renderer>().material.SetTextureScale ("_MainTex", size);
		    
			 GetComponent<Renderer>().enabled = true;
		}			
		else{
	 		effectEnd = true;
			GetComponent<Renderer>().enabled = false;
			end = true;		

			if (soundEffect){
				if (soundEffect.isPlaying){
					end = false;
				}
			}
		
			if (addLightEffect && end){
				if (gameObject.GetComponent<Light>().intensity>0){
					end = false;
				}
			}
			
			if (end){
				Destroy(gameObject);	
 			}
		}
		
		// Size
		if (sizeStart != sizeEnd){
	    	myTransform.localScale += sizeStep * Time.deltaTime ;
		}
		   
		
		// Light effect
	 	if (addLightEffect && lightFadeSpeed!=0){
			gameObject.GetComponent<Light>().intensity -= lightFadeSpeed*Time.deltaTime;
		}
		
		// Color Effect
		if (addColorEffect){
			currentColor = new Color(currentColor.r + colorStep.r * Time.deltaTime,currentColor.g + colorStep.g* Time.deltaTime,currentColor.b + colorStep.b* Time.deltaTime , currentColor.a + colorStep. a*Time.deltaTime);
			meshRender.material.SetColor("_TintColor", currentColor);
		}
	}
	
	#endregion
	
	#region private methods

	void CreateParticle(){
		
		mesh = gameObject.AddComponent<MeshFilter>().mesh; 
		meshRender = gameObject.AddComponent<MeshRenderer>(); 		
		
		mesh.vertices = new Vector3[] { new Vector3(-0.5f,-0.5f,0f),new Vector3(-0.5f,0.5f,0f), new Vector3(0.5f,0.5f,0f), new Vector3(0.5f,-0.5f,0f) };
		mesh.triangles = new int[] {0,1,2, 2,3,0 };
		mesh.uv = new Vector2[] { new Vector2(1f,0f),  new Vector2 (1f, 1f),  new Vector2 (0f, 1f), new Vector2 (0f, 0f)};

		//meshRender.castShadows = false;
		meshRender.receiveShadows = false;
		mesh.RecalculateNormals();		
		
		GetComponent<Renderer>().material= spriteSheetMaterial;
	}

	void Camera_BillboardingMode(){
		
		Vector3 lookAtVector =   myTransform.position-mainCamTransform.position  ;
		
		switch (billboarding){
			case CameraFacingMode.BillBoard:
				myTransform.LookAt(  mainCamTransform.position - lookAtVector); 
				break;
			case CameraFacingMode.Horizontal:
				lookAtVector.x = lookAtVector.z =0 ;
				myTransform.LookAt(mainCamTransform.position - lookAtVector);
				break;
			case CameraFacingMode.Vertical:
				lookAtVector.y=lookAtVector.z =0;
				myTransform.LookAt(mainCamTransform.position - lookAtVector);
				break;
		}
		
		if (rotationStart!=rotationEnd){
			currentRotation+=rotationStep*Time.deltaTime;
		}
		
		myTransform.eulerAngles = new Vector3(myTransform.eulerAngles.x,myTransform.eulerAngles.y,currentRotation);
	}
	
	#endregion
	
	#region public methos
	public void InitSpriteSheet(){
		startTime = Time.time;
		lifeStart = Time.time;
		myTransform = transform;
				
		// time divider
		float divider = (float)spriteCount/(float)framesPerSecond;
			
		// size
		sizeStep = new Vector3( (sizeEnd.x - sizeStart.x)/divider, (sizeEnd.y - sizeStart.y)/divider,(sizeEnd.z - sizeStart.z)/divider);
		currentSize = sizeStart;
		myTransform.localScale = currentSize;
		
		//rotation
		rotationStep = (rotationEnd-rotationStart)/divider;
		// Random start rotation
		if (randomRotation){
			currentRotation = Random.Range(-180.0f,180.0f);
		}
		else{
			currentRotation = rotationStart;
		}
		
		// Add color effect
		if (addColorEffect){
			colorStep = new Color( (colorEnd.r - colorStart.r)/divider,(colorEnd.g - colorStart.g)/divider,(colorEnd.b - colorStart.b)/divider, (colorEnd.a - colorStart.a)/divider);
			currentColor = colorStart;
			meshRender.material.SetColor("_TintColor", currentColor);
		}		
	}
	#endregion
}
