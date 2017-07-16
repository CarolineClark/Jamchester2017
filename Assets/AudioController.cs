using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlaySound : MonoBehaviour {


	[SerializeField]	private AudioClip lever;

	private AudioSource audioSource;
	public PlayerController targetScript;

	private void Awake()
	{
		Assert.IsNotNull(lever);
	}

    void Start()
    {
				targetScript = GetComponent<PlayerController>();
				audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
			if(targetScript.flipped == true)
        {
            audioSource.PlayOneShot(lever);

        }
    }
}
