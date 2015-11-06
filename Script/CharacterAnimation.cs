using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {
	Animator anim;
	//int walkHash = Animator.StringToHash("walk");
	// Use this for initialization
	void Start(){
		anim = GetComponent<Animator>();
	}
	void Update(){
		StartCoroutine(Wait());
		//float move = Input.GetAxis("Vertical");
		//anim.SetFloat("speed", move);
	}
	public IEnumerator Wait(){
		yield return new WaitForSeconds(3);
	}
}
