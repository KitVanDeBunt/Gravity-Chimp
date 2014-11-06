using UnityEngine;
using System.Collections;

//this script animates a platform when it is in contact with the player and removes it when done

public class PlatformDissolve : MonoBehaviour {
	
	public int frames = 3;
	public int animateSpeed = 7;
	
	private int timer;
	private tk2dSprite sprite;
	private Vector3 thisPosition;
	private int frameID = 0;
	private int startFrameID = 0;
	private bool PlayerContact = false;
	
	void Start(){
		sprite = GetComponent<tk2dSprite>();
		frameID = sprite.spriteId;
		startFrameID = frameID;
		
	}
	void FixedUpdate () {
		if(PlayerContact){
			timer+=1;
			//animate
			if(timer == animateSpeed){
				frameID++;
				if(frameID >= startFrameID+frames){
					Destroy(gameObject);
				}else{
					sprite.spriteId = frameID;
					timer = 0;
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.collider.tag == "Player"){
			PlayerContact = true;
		}
	}
	
	void OnCollisionExit(Collision col)
	{
		if(col.collider.tag == "Player"){
			PlayerContact = false;
		}
	}
	
}
