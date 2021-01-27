using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchForSingleton : MonoBehaviour
{
    public AudioManager audioManager;

    private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        }

        _slider = GetComponent<Slider>();

        _slider.onValueChanged.AddListener(delegate { ValueChangeCheck(_slider.value); });
    }

    public void ValueChangeCheck(float value) 
    {
        audioManager.SetMasterVolume(value);
        
    }
}
