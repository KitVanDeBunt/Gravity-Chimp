using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class touchData{
	internal Touch touch;
	internal bool used = false;
}
public class PlayerMove : MonoBehaviour {
	
	public float gravityForce = 80f;
	public float horForce =70.0f;
	public Vector3 playerOrgin;
	public float maxVelocity = 28; 
	public float slowDownGround = 1.1f; 
	public float slowDownAir = 1.1f;
	
	public AudioClip switchGrafity;
	
	bool grafityUpDown = false;
	bool gravitySwitchDone = false;
	bool gravityswitchHalfDone = false;
	bool gravityswitchTriggerd = false;
	float xAxis;
	bool onGround = false;
	int timer;
	int frameID = 0;
	tk2dSprite sprite;
	List<touchData> touches;
	
	
	void Awake(){
		playerOrgin = transform.position;
		touches = new List<touchData>();
	}
	
	void Start () {
		Physics.gravity = new Vector3(0f,-gravityForce,0f);
		rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		//rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		//get Sprite
		sprite = GetComponent<tk2dSprite>();
		sprite.MakePixelPerfect();
	}
	
	private float InputSpeed{
		get{
			#if UNITY_IPHONE || UNITY_ANDROID
			if(Input.touchCount > 0 && Input.touchCount < 2){
				return ((Input.GetTouch(0).position.x /Screen.width)*2)-1;
			}else{
				return 0;
			}
			#else
			return Input.GetAxis("Horizontal");
			#endif
			
		}
	}
	
	
	bool finger1Down = false;
	bool finger2Down = false;
	void Update(){
		bool jump = false;
		#if UNITY_IPHONE || UNITY_ANDROID
		for(int i = 0; i < Input.touchCount;i++){
			for(int j = 0; j < touches.Count;j++){
				//check if touch in list
				if(Input.touches[i].fingerId==touches[j].touch.fingerId){
				//check if touch ended
					if(Input.touches[i].phase == TouchPhase.Ended || Input.touches[i].phase == TouchPhase.Canceled ){
						//remove touch
						touches.Remove(touches[j]);
					}
				}
			}
			//add to list
			if(Input.touches[i].phase == TouchPhase.Began){
				touchData newTouch = new touchData();
				newTouch.touch = Input.touches[i];
				touches.Add(newTouch);
			}
			//udpate touches
			if(Input.touches[i].phase == TouchPhase.Moved ||Input.touches[i].phase == TouchPhase.Stationary){
				for(int j = 0; j < touches.Count;j++){
					if(Input.touches[i].fingerId==touches[j].touch.fingerId){
						touchData updateTouch = touches[j];
						updateTouch.touch = Input.touches[i];
						touches[j] = updateTouch;
					}
				}
			}
		}
		
		if(touches.Count==2){
			if(touches[0].used == false ||touches[1].used == false){
				jump = true;
				for(int j = 0; j < touches.Count;j++){
					touchData updateTouch = touches[j];
					updateTouch.used = true;
					touches[j] = updateTouch;
				}
			}
		}
		
		#else
		jump = Input.GetKeyUp(KeyCode.Space);
		#endif
		
		if(jump){
			if(onGround){
				audio.PlayOneShot(switchGrafity);
				if(grafityUpDown){
					Physics.gravity = new Vector3(0f,-gravityForce,0f);
					grafityUpDown = false;
					onGround = false;
				}else{
					Physics.gravity = new Vector3(0f,gravityForce,0f);
					grafityUpDown = true;
					onGround = false;
				}
				gravityswitchTriggerd = true;
			}
		}
	}
	
	//bool moveVelocityChange = false;
	void FixedUpdate () {
		
		xAxis = InputSpeed;
		
		//movement
		float currentVelosityX = rigidbody.velocity.x;
		int stopDistance; 
		if(xAxis>0){
			if(currentVelosityX < maxVelocity){
				rigidbody.AddForce(horForce,0,0,ForceMode.Force);
			}
		}else if(xAxis<0){
			if(currentVelosityX >- maxVelocity){
				rigidbody.AddForce(-horForce,0,0,ForceMode.Force);
			}	
		}else{ // if imput is null
			if(onGround){//slowdown
				if(currentVelosityX>0){
					rigidbody.AddForce(-Mathf.Pow(currentVelosityX,slowDownGround),0,0);
				}else if(currentVelosityX<0){
					rigidbody.AddForce(Mathf.Pow(-currentVelosityX,slowDownGround),0,0);
				}
			}else{//slowdown
				if(currentVelosityX>0){
					rigidbody.AddForce(-Mathf.Pow(currentVelosityX,slowDownAir),0,0);
				}else if(currentVelosityX<0){
					rigidbody.AddForce(Mathf.Pow(-currentVelosityX,slowDownAir),0,0);
				}
			}
		}
		
		
		//animate
		timer+=1;
		if((timer >= 3&&!onGround)||(onGround&&timer >= 7)){
			timer = 0;
			//flipX
			if (xAxis > 0){
				sprite.FlipX = true;	
			}else if( xAxis < 0){
				sprite.FlipX = false;	
			}
			//flipY
			if (gravityswitchHalfDone||onGround){
				if (grafityUpDown){
					sprite.FlipY = true;	
				}else{
				 	sprite.FlipY = false;	
				}
			}
			
			//change frame
			//Debug.Log(frameID+" "+gravityswitchTriggerd+" "+ gravityswitchHalfDone+" "+gravitySwitchDone);
			if (onGround == true){
				if(xAxis==0){
					//idle
					frameID++;
					if(frameID >=3){frameID = 0;};
				}else{
					frameID++;
					if(frameID <=8||frameID >=14){frameID = 9;};	
				}
			}else if(gravityswitchTriggerd){
				//switch
				if(frameID == 25){
					gravityswitchHalfDone = true;
				}
				if(gravityswitchHalfDone && frameID ==18){
					gravitySwitchDone = true;
				}
				if(!gravityswitchHalfDone){
					frameID++;	
				}else{
					if(!gravitySwitchDone){
						frameID--;
					}
				}
				if(frameID <=17){frameID = 18;};
			}
			sprite.spriteId = frameID;
		}
	}
	
	//check if thouching ground
	void onGroundCheck(Collision col){
		//Debug.Log( "Y: "+col.contacts[0].normal.y+" X:" +col.contacts[0].normal.x);
		for(int cols = 0; cols < col.contacts.Length; cols++){
			if (col.contacts[cols].normal.x < 0.1&&col.contacts[cols].normal.x > -0.1){
				onGround = true;	
				gravitySwitchDone = false;
				gravityswitchHalfDone = false;
			}
		}
	}
	
	//collision events 
	void OnCollisionEnter (Collision col)
    {
		onGroundCheck(col);
		gravityswitchTriggerd = false;
	}
	
	void OnCollisionStay ( Collision col ){
		onGroundCheck(col);
	}
	
	void OnCollisionExit(Collision col) {
        onGround = false;
    }
}
