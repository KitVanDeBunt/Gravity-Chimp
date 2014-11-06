using UnityEngine;
using System.Collections;

public class levelSpawnPoint : MonoBehaviour {
	public bool active = false;
	public int Number;
	
	GameObject player;
	private tk2dSprite sprite;
	
	void Start(){
		sprite = GetComponent<tk2dSprite>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void OnTriggerEnter(Collider col){
		gameData.setAllSpawnsInactive();
		active = true;
		gameData.updateActiveSpawn();
	}
	
	public void setFrame(){
		if(active&&sprite){
			sprite.SetSprite("checkpoint/1");
		}else if(sprite){
			sprite.SetSprite("checkpoint/0");
		}
	}
}
