using UnityEngine;
using System.Collections;

public class GateSound : MonoBehaviour 
{
	public AudioClip dooropen;
	public AudioClip doorclose;

	AudioSource audio;

	void Start() 
	{
		audio = GetComponent<AudioSource>();
	}

	void DoorOpenSound() 
	{
		audio.PlayOneShot(dooropen);
	}

	void DoorCloseSound() 
	{
		audio.PlayOneShot(doorclose);
	}

}

