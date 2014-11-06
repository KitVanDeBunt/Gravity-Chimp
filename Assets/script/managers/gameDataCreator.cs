using UnityEngine;
using System.Collections;

public class gameDataCreator : MonoBehaviour {

	void Start () {
		if(GameObject.FindGameObjectWithTag("gameDataHolder") == null){
			Instantiate(Resources.Load("GameDataHolder"),transform.position,Quaternion.identity);
		}
	}
}
