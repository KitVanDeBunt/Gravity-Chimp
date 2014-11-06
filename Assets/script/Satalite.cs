using UnityEngine;
using System.Collections;

public class Satalite : MonoBehaviour {
	
	float rotationSpeed;
	int randomdirection;
	
	void Start () {
		rotationSpeed = Random.Range(0.9f,1.3f);
		randomdirection = Random.Range(0,2);
	}
	
	void FixedUpdate () {
		float currentZRotation = transform.rotation.z;
		float newZRotation = currentZRotation+=rotationSpeed;
		if(randomdirection==1){
			transform.Rotate( new Vector3(0,0,newZRotation));
		}else{
			transform.Rotate( new Vector3(0,0,-newZRotation));
		}
	}
}
