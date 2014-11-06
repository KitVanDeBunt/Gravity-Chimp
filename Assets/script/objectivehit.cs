using UnityEngine;
using System.Collections;

public class objectivehit : MonoBehaviour {
	public AudioClip inPortal;
void OnCollisionEnter (Collision col)
    {
		tk2dUIAudioManager.Instance.Play(inPortal);
		Application.LoadLevel("WinScreen");
		
	}
}
