using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public AudioMixer generalAudioMixer;
    public AudioMixer musicAudioMixer;
    public AudioMixer SFXAudioMixer;
    public bool dynamic;
    public Sound[] sounds;

    List<Sound> soundsOnAwake = new List<Sound>();


    // Method that sets sound list
    private void SetSoundList()
    {
        // Configs for audioclips                    
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.outputAudioMixerGroup = s.outputMixer;

            // Setting sound to play on awake
            if (s.playOnAwake)
            {
                soundsOnAwake.Add(s);
            }
        }
    }


    // Singleton method
    private void SetUpSingleton()
    {
        int _numberOfAudioManagers = 0;
        foreach (AudioManager manager in FindObjectsOfType<AudioManager>())
        {
            if (manager.dynamic)
            {
                _numberOfAudioManagers++;
            }
        }

        // Checks if menu music was already instantiated and destroy the new one if it was
        if (_numberOfAudioManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }

    }


    // Start is called before the first frame update
    void Awake()
    {
        // Maybe we'll use singletons between hubs
        if (dynamic)
        {
            SetUpSingleton();
        }

        // Setting sound list
        SetSoundList();

        // Playing sound on awake
        foreach (Sound s in soundsOnAwake)
        {
            PlaySound(s.name);
        }
    }


    // Method that sets master volume to value passed
    public void SetMasterVolume(float volume)
    {
        generalAudioMixer.SetFloat("MasterVolume", 20 * (float)Math.Log10(volume));
    }


    // Method that sets music volume to value passed
    public void SetMusicVolume(float volume)
    {
        generalAudioMixer.SetFloat("MusicVolume", 20 * (float)Math.Log10(volume));
        //PlayerPrefsController.SetMusicVolume(volume);
    }


    // Method that sets music volume to value passed
    public void SetSFXVolume(float volume)
    {
        generalAudioMixer.SetFloat("SFXVolume", 20 * (float)Math.Log10(volume));
        //PlayerPrefsController.SetSFXVolume(volume);
    }


    // Method that plays a sound given its name
    public void PlaySound(string soundName)
    {
        // Find sound by name
        Sound _sound = Array.Find(sounds, s => s.name == soundName);

        // Play sound if found and logs a error message if not
        if (_sound == null)
        {
            Debug.LogError("Couldn't find sound named " + soundName);
        }
        else
        {
            _sound.source.Play();
        }
    }


    // Method that stops playing a sound given its name
    public void StopSound(string soundName)
    {
        // Find sound by name
        Sound _sound = Array.Find(sounds, s => s.name == soundName);

        // Play sound if found and logs a error message if not
        if (_sound == null)
        {
            Debug.LogError("Couldn't find sound named " + soundName);
        }
        else
        {
            _sound.source.Stop();
        }
    }


    // Method that stops playing all sounds
    public void StopAllSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Stop();
        }
    }


    // Method that stops playing all sounds
    public void StopAllSFX()
    {
        foreach (Sound sound in sounds)
        {
            if (!sound.playOnAwake)
            {
                sound.source.Stop();
            }
        }
    }


    // Method that mutes a soundtrack given its name
    public void MuteSoundtrack(string soundtrackName)
    {
        musicAudioMixer.SetFloat(soundtrackName + "Volume", -80);
    }


    // Method that unmutes a soundtrack given it's name
    public void UnmuteSoundtrack(string soundtrackName)
    {
        musicAudioMixer.SetFloat(soundtrackName + "Volume", 0);
    }


    // Method that sets volume of a soundtrack given it's name and volume
    public void SetSoundtrackVolume(string soundtrackName, float volume)
    {
        musicAudioMixer.SetFloat(soundtrackName + "Volume", volume);
    }


    // Method that sets default volumes
    public void SetDefaultMixerVolumes()
    {
        musicAudioMixer.SetFloat("BaseVolume", 0);
        musicAudioMixer.SetFloat("HarpaVolume", -5);
        musicAudioMixer.SetFloat("OrquestraBaseVolume", -5);
        musicAudioMixer.SetFloat("RiffOrquestraVolume", -80);
        musicAudioMixer.SetFloat("TemaAnnVolume", -5);
        musicAudioMixer.SetFloat("VinilVolume", -5);
    }


    // Method that sets game over volumes
    public void SetGameOverVolumes()
    {
        musicAudioMixer.SetFloat("BaseVolume", 0);
        musicAudioMixer.SetFloat("HarpaVolume", -5);
        musicAudioMixer.SetFloat("OrquestraBaseVolume", -5);
        musicAudioMixer.SetFloat("RiffOrquestraVolume", -80);
        musicAudioMixer.SetFloat("TemaAnnVolume", -80);
        musicAudioMixer.SetFloat("TemaThomasVolume", -80);
    }
}
