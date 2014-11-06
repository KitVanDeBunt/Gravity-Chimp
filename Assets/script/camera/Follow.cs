using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follow : MonoBehaviour {
	public GameObject player;
	public GameObject[] backGrounds;
	
	private int backgroundLenght;
	private float[] backParaValua; //background paralex valua
	private float moveDelay = 0.25f;
	private float moveSpeed;
	private GameObject CameraArea;
	
	public void init(){
		backgroundLenght = backGrounds.Length;
		backParaValua = new float[backgroundLenght];
		for(int i = 0; i<backgroundLenght;i++){
			//backParaValua[i] = (float)0.5f;
			backParaValua[i] = backGrounds[i].GetComponent<paralex>().paralexValua;
		}
	}
	void LateUpdate (){
		moveSpeed = moveDelay*Mathf.Pow( Time.deltaTime , 0.2f);
		
		//get new point to move camera to
		float tagetX;
		float tagetY;
		if(CameraArea){
			if(CameraArea.GetComponent<CameraZone>().freezXCamera){
				tagetX = CameraArea.GetComponent<CameraZone>().xPos;
			}else{
				tagetX = player.transform.position.x;
			}
			if(CameraArea.GetComponent<CameraZone>().freezYCamera){
				tagetY = CameraArea.GetComponent<CameraZone>().yPos;
			}else{
				tagetY = player.transform.position.y;
			}
		}else{
			tagetX = player.transform.position.x;
			tagetY = player.transform.position.y;
		}
		//calculate new camera x position
		float thisX = transform.position.x;
		float DeltaX = tagetX - thisX;
		float newXP = thisX + (DeltaX*moveSpeed) ;
		//calculate new camera y position
		float thisY = transform.position.y;
		float DeltaY = tagetY - thisY;
		float newYP = thisY + (DeltaY*moveSpeed) ;
		//camera move
		transform.position = new Vector3( newXP, newYP, -10 );
		
		//background move
		for(int j = 0; j<backgroundLenght;j++){
			float currentXBack = backGrounds[j].transform.position.x;
			float currentYBack = backGrounds[j].transform.position.y;
			float currentZBack = backGrounds[j].transform.position.z;
			float newXBack = currentXBack + (DeltaX*moveSpeed) - (DeltaX * backParaValua[j]*moveSpeed);
			float newYBack = currentYBack + (DeltaY*moveSpeed) - (DeltaY * backParaValua[j]*moveSpeed);
			backGrounds[j].transform.position = new Vector3(newXBack,newYBack,currentZBack);	
		}
	}
	
	public void setBackground(){
		Vector3 playerOrgin = player.GetComponent<PlayerMove>().playerOrgin;
		Vector3 playerPosition = player.transform.position;
		Debug.Log ("orgin: "+playerOrgin+" - position: "+playerPosition);
		for(int j = 0; j<backgroundLenght;j++){
			float currentXBack = backGrounds[j].transform.position.x;
			float currentYBack = backGrounds[j].transform.position.y;
			float currentZBack = backGrounds[j].transform.position.z;
			float newXBack = currentXBack + (playerPosition.x - playerOrgin.x)* (1-backParaValua[j]);
			float newYBack = currentYBack + (playerPosition.y - playerOrgin.y)* (1-backParaValua[j]);
			Debug.Log(newXBack+"-"+newYBack);
			backGrounds[j].transform.position = new Vector3(newXBack,newYBack,currentZBack);	
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.collider.tag == "CameraArea"){
			CameraArea = other.collider.gameObject;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.collider.tag == "CameraArea"){
			CameraArea = null;
		}
	}
}
