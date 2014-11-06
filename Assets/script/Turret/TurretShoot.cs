using UnityEngine;
using System.Collections;

public class TurretShoot : MonoBehaviour {
	//public GameObject Player;
	public GameObject button;
	public bool canshoot;
	public float bulletSpeed = 0.4f;
	public int shootSpeed = 7;
	public GameObject[] spawnPoints;
	
	private int timer;
	private Vector3 bulletSpawnPosition;
	private int frameID = 0;
	private tk2dSprite sprite;
	
	
	
	void Start(){
		sprite = GetComponent<tk2dSprite>();
		
		bulletSpawnPosition = transform.position;
		if(sprite.FlipY){
			bulletSpawnPosition.y-=0.3f;
		}else{
			bulletSpawnPosition.y+=0.3f;	
		}
		
		if(sprite.FlipX){
			bulletSpawnPosition.x-=1.7f;
		}else{
			bulletSpawnPosition.x+=1.7f;	
		}
		
		canshoot = true;
	}
	void FixedUpdate () {
		if(button!=null){
			if(!button.GetComponent<ButtonShoot>().canshoot){
				canshoot = false;
			}
		}
		//Debug.Log (Player.transform.position.x);
		//sprite.SetSprite(FrameName);
		//if(Player.transform.position.x > transform.position.x){
		//	sprite.FlipX = false;
		//}else{
		//	sprite.FlipX = true;
		//}
		
		timer+=1;
		
		
		
		if (canshoot == true){
			//animate
			if(timer%shootSpeed == 0){
				frameID++;
				if(frameID ==3){frameID = 0;};
				sprite.spriteId = frameID;
			}
			//shoot
			if(timer%(shootSpeed*3) == 0){
				audio.Play();
				GameObject bullet = Instantiate(Resources.Load("Bullet"), bulletSpawnPosition, Quaternion.identity) as GameObject;
				bullet.GetComponent<BulletScript>().speed = bulletSpeed;
				if(sprite.FlipY){
					
				}
				if(sprite.FlipX){
					Quaternion bulrot = new Quaternion(0,0,180,0);
					bullet.transform.rotation = bulrot;
				}
			}
		}
	}
}