using UnityEngine;
using System.Collections;

public class ToMenu : MonoBehaviour {
	tk2dSprite sprite;
	int frameID = 0;
	public AudioClip Buttonclick;
		void Start () {
		sprite = GetComponent<tk2dSprite>();
	}
	 void OnMouseUp() {
		frameID = 0;
         Application.LoadLevel("StartMenu");
		sprite.spriteId = frameID;
    }
	    void OnMouseDown() {
		frameID = 1;
		sprite.spriteId = frameID;
		tk2dUIAudioManager.Instance.Play(Buttonclick);
    }
	
}
