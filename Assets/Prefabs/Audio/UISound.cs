using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{

    // not really a UI sound class, but only a simple sound trigger

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip clip;

    void Start()
    {
        source.playOnAwake = false;
        source.clip = clip;
    }

    public void PlaySound()
    {
        source.Play();
    }
}
