using UnityEngine;
using System.Collections;

public class EnemyWalk : MonoBehaviour {
	
	public bool alife = true;
	bool aggroLeft;
	bool aggroRight;
	int speed = 50;
	
	public	bool gravityIsUp;
	tk2dSpriteAnimator animator;
	tk2dSprite sprite;
	
	void Start(){
		animator = GetComponent<tk2dSpriteAnimator>();
		sprite = GetComponent<tk2dSprite>();
		animator.AnimationEventTriggered += AnimationEventHandler;
	} 
	
	void walk(bool left,float speedMultiplayer = 1){
		float walkSpeed = speed*speedMultiplayer;
		if (gravityIsUp){
			if(left){
				rigidbody.AddForce(walkSpeed,0,0);
			}else{
				rigidbody.AddForce(-walkSpeed,0,0);
			}
		}else{
			if(left){
				rigidbody.AddForce(-walkSpeed,0,0);	
			}else{
				rigidbody.AddForce(walkSpeed,0,0);	
			}
		}
	}
	
	void switchDirection(){
		if(sprite.FlipX){
			sprite.FlipX = false;	
		}else{
			sprite.FlipX = true;
		}
	}
	
	void FixedUpdate () {
		if(alife){
			//follow player
			if(aggroLeft){
				walk (true,2.0f);	
			}else if (aggroRight){
				walk (false,2.0f);	
			}
			//normal walk
			if(!aggroLeft&&!aggroRight){
				if(sprite.FlipX){
					walk (true);
				}else{
					walk(false);
				}
			}
		
			Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));
			RaycastHit hit;
			Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);	
	
			if (Physics.Raycast(ray, out hit, 20))
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					aggroLeft = true;
					sprite.FlipX = true;
				}else{
					aggroLeft = false;	
				}
			}else{
				aggroLeft = false;	
			}
			
			Ray ray2 = new Ray(transform.position, transform.TransformDirection(Vector3.right));
			RaycastHit hit2;
			Debug.DrawRay(ray2.origin, ray2.direction * 20, Color.red);	
	
			if (Physics.Raycast(ray2, out hit2, 20))
			{
				if (hit2.collider.gameObject.tag == "Player")
				{
					aggroRight = true;
					sprite.FlipX = false;
				}else{
					aggroRight = false;	
				}
			}else{
				aggroRight = false;	
			}	
		}
	}
	
	void AnimationEventHandler(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNum)
	{
		Destroy(gameObject);
	}
	
	void OnCollisionEnter (Collision col)
    {
		if(col.collider.tag == "Player"){
			if(col.contacts[0].normal.y > 0.8 ||col.contacts[0].normal.y < -0.8){ 
				animator.Play("Die");
				alife = false;
				audio.Play();
			}
			float xforce = col.contacts[0].normal.x ;
			float yforce = col.contacts[0].normal.y ;
			if(yforce>0.8){
				yforce = 1;
			}else if(yforce<-0.8){
				yforce = -1;
			}else{yforce = 0 ;}
			if(xforce>0.8){
				xforce = 1;
			}else if(xforce<-0.8){
				xforce = -1;
			}else{xforce = 0 ;}
			
			Vector3 playerForceDirection = new Vector3(-xforce*30,-yforce*30,0);
			col.collider.rigidbody.AddForce(playerForceDirection,ForceMode.Impulse);
			rigidbody.AddForce(new Vector3(xforce*60,0,0),ForceMode.Impulse);
		}else{
			switchDirection();	
		}
	}
	
	void OnCollisionStay ( Collision col ){
		for(int co = 0;co<col.contacts.Length;co++){
				
		}
		//switchDirection();
	}
}
