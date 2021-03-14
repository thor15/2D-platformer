﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    private AudioSource musicPlayer;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public int musicClip = 0;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!musicPlayer.isPlaying)
        {
            PlayMusic();
            musicClip++;
        }
    }

    private void PlayMusic()
    {
        musicPlayer.PlayOneShot(audioClips[musicClip]);
        if(musicClip > audioClips.Count)
        {
            musicClip = 0;
        }
    }
}
