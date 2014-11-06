using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public GUIText livesdisplay;
	public GameObject liveDisplay;
	public int lives = 3;
	public bool canDie = true;
	public AudioClip hit;
	
	tk2dSpriteAnimator animator;
	public Vector3 spawnLocation;
	int startLive;
	GameObject camera;
	GameObject restartScreen;
	
	
	void Start () {
		camera = GameObject.FindGameObjectWithTag("camera");
		animator = liveDisplay.GetComponent<tk2dSpriteAnimator>();
		startLive = lives;
		SetLiveText();
		gameData.init();
		camera.GetComponent<Follow>().init();
		camera.GetComponent<Follow>().setBackground();
	}
	
	public void movePlayerToSpawn(){
		
		//movePlayer+camera to active spawn
		spawnLocation.z = transform.position.z;
		transform.position = spawnLocation;
		Vector3 newCamPos = new Vector3(spawnLocation.x,spawnLocation.y,camera.transform.position.z);
		camera.transform.position = newCamPos;
	}
	
	void playerDie(){
		Application.LoadLevel("RestartScreen");	
	}
	
	void GameOverCheck() {
		if (lives <= 0)
		{
			if(canDie){
				playerDie();
			}
			Debug.Log ("gameOver");
			//aplication.load("losescreen")
		}
	}
	
	void SetLiveText() {
		if(lives>=0&&lives<=3){
			animator.SetFrame(lives);
		}
	 	livesdisplay.text = "lives " + lives;
		GameOverCheck();
	}
	
	void OnTriggerEnter(Collider col){
		if(col.collider.tag == "bullet" ){
			audio.PlayOneShot(hit);
			lives -= 1;
			SetLiveText();
		}
	}
	
	void OnCollisionEnter (Collision col)
    {
		//Debug.Log(col.contacts[0].normal.y);
		if(col.collider.tag == "KillObject" ){
				lives -= startLive;
				SetLiveText();
				audio.PlayOneShot(hit);
		}
		if(col.collider.tag == "Enemy"){
			if(col.contacts[0].normal.x > 0.9 || col.contacts[0].normal.x < -0.9){
				if(col.collider.GetComponent<EnemyWalk>().alife){
					lives -= 1;
					SetLiveText();
					audio.PlayOneShot(hit);
				}
			}
		}
	}
}
