using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField]
    private float pitchChange = 0.05f;

    [SerializeField]
    private float volumeChange = 0.025f;

    [SerializeField]
    private AudioSource source_engine;

    [SerializeField]
    private List<AudioSource> sources_effect_loading = new List<AudioSource>();

    [SerializeField]
    private List<AudioSource> sources_effect_unloading = new List<AudioSource>();

    private float defaultPitch;

    private float defaultVolume;


    public enum SoundEffects
    {
        unloading,
        loading,
        crashing,
    }

    void Start()
    {
        defaultPitch = source_engine.pitch;
        defaultVolume = source_engine.volume;
    }

    // engine sound gets louder and higher pitched when user input for movement is true
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && source_engine.pitch == defaultPitch && source_engine.volume == defaultVolume)
        {
            source_engine.pitch += pitchChange;
            source_engine.volume += volumeChange;
        } 
        else if (Input.GetAxis("Horizontal") == 0 && source_engine.pitch != defaultPitch && source_engine.volume != defaultVolume)
        {
            source_engine.pitch = defaultPitch;
            source_engine.volume = defaultVolume;
        }
    }

    public void PlaySound_Engine()
    {
        if (!source_engine.isPlaying)
        {
            source_engine.Play();
        }
    }


    public void PlaySoundEffect(SoundEffects effect)
    {
        switch (effect)
        {

            case SoundEffects.loading:
                foreach (AudioSource source in sources_effect_loading)
                {
                    source.Play();
                }
                return;

            case SoundEffects.unloading:
                foreach (AudioSource source in sources_effect_unloading)
                {
                    source.Play();
                }
                return;

            default:
                return;
        }

    }
}
