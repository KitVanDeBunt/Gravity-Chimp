using UnityEngine;
using System.Collections;

public class ButtonShoot : MonoBehaviour {
	public bool canshoot = true;
	private int spriteStartId;
	
	tk2dSprite sprite;
	
	void Start(){
		sprite = GetComponent<tk2dSprite>();
		spriteStartId = sprite.spriteId;
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.collider.tag == "Player"){
	 		//TurretShoot Turret= col.collider.GetComponent<TurretShoot>();
			//Turret.canshoot = false;
			canshoot = false;
			sprite.spriteId = spriteStartId+1;
			Debug.Log("Button Name: "+gameObject.name+" Button Status: "+canshoot);
		}
	}
}
