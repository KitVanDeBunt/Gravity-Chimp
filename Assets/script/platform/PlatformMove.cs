using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour {
	//public
	public Transform Origin;
	public Transform Destination;
	public float Speed = 10;
	//private
	private GameObject player;
	private bool direction = false;
	private bool playerOn = false;
	private float deltaX = 0;
	private float OldX;
	private float deltaY = 0;
	private float OldY;
	private tk2dSprite sprite;
	
	void Start(){
		sprite = GetComponent<tk2dSprite>();
		player = GameObject.FindGameObjectWithTag("Player");
		OldX = transform.position.x;	
		OldY = transform.position.y;
	}
	
	void FixedUpdate () {
		//get delta position platform
		float currentX = transform.position.x;
		deltaX = OldX -currentX;
		OldX = currentX;
		float currentY = transform.position.y;
		deltaY = OldY -currentY;
		OldY = currentY;
		// switch direction
		if(transform.position == Destination.position){	direction = true;	}
		if(transform.position == Origin.position){		direction = false;	}
		
		//move platform
		if(direction){
			transform.position = Vector3.MoveTowards(transform.position, Origin.position, Speed);
		}else{
			transform.position = Vector3.MoveTowards(transform.position, Destination.position, Speed);
		}
		// stick player to platform
		if(playerOn){
			player.transform.Translate(new Vector3(-deltaX,-deltaY,0));
		}
	}
	
	void OnCollisionEnter (Collision col)
    {
		if(col.collider.tag == "Player"){
			playerOn = true;
		}
    }
	
	void OnCollisionStay ( Collision col ){
		if(col.collider.tag == "Player"){
			playerOn = true;
		}
	}
	
	void OnCollisionExit(Collision col) {
        if(col.collider.tag == "Player"){
			playerOn = false;
		}
    }
}