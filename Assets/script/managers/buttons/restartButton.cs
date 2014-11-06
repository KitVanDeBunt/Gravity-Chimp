using UnityEngine;
using System.Collections;

public class restartButton : MonoBehaviour {
	tk2dSprite sprite;
	int frameID = 0;
	public AudioClip Buttonclick;
		void Start () {
		sprite = GetComponent<tk2dSprite>();
	}
	 void OnMouseUp() {
		frameID = 0;
         Application.LoadLevel("level1");
		sprite.spriteId = frameID;
    }
	    void OnMouseDown() {
		gameData.restart();
		frameID = 1;
		sprite.spriteId = frameID;
		tk2dUIAudioManager.Instance.Play(Buttonclick);
    }
	
}
