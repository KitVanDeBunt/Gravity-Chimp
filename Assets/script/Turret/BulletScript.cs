using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public float speed = 0.5f;
	void FixedUpdate(){
		transform.Translate(new Vector3(speed,0,0));
	}
	void OnTriggerEnter(Collider col){
		Destroy(gameObject);
	}
}

