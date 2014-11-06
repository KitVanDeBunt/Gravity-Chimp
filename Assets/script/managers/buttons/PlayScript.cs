using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour {
	tk2dSprite sprite;
	int frameID = 0;
	public AudioClip Buttonclick;
		void Start () {
		sprite = GetComponent<tk2dSprite>();
		//sprite.MakePixelPerfect();
	}
	 void OnMouseUp() {
		frameID = 1;
         Application.LoadLevel("level1");
		sprite.spriteId = frameID;
    }
	    void OnMouseDown() {
		frameID = 0;
		sprite.spriteId = frameID;
		tk2dUIAudioManager.Instance.Play(Buttonclick);
    }
	
}
