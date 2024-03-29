﻿using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;
    public AudioMixerGroup outputMixer;
    public bool loop;
    public bool playOnAwake;


    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch = 1f;

    [HideInInspector] public AudioSource source;

}