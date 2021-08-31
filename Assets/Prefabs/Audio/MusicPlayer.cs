using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private bool active = false;

    [SerializeField]
    private bool startWithRandomSong = false;

    [SerializeField]
    private bool fadeOut = true;

    [SerializeField]
    private float fadeOutLength = 8;

    [SerializeField]
    private bool fadeIn = true;

    [SerializeField]
    private float fadeInLength = 8;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();

    // possibly add volumes for each clip as dictionary
    private float defaultVolume = 1;

    private int currentSongIndex = 0;

    private bool fadingOutFlag = false;

    private bool fadingInFlag = false;

    private float fadeOutTimer = 0;

    private float fadeInTimer = 0;

    private float fadeOutInterval;

    private float fadeInInterval;

    void Start()
    {   
        if (clips.Count < 1) throw new UnassignedReferenceException("Background Clips cant be empty");

        defaultVolume = source.volume;

        if (startWithRandomSong)
        {
            currentSongIndex = Mathf.FloorToInt(Random.Range(0, clips.Count-1));
        }

        source.clip = clips[currentSongIndex];

        if (active)
        {
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            // when fadeout is actiavted, and fadingflag is not set and When song-time left reached fadeout seconds
            if(fadeOut && !fadingOutFlag && source.time >= source.clip.length - fadeOutLength)
            {
                // set fade out flag, init timer, calculate interval
                fadingOutFlag = true;
                fadeOutTimer = fadeOutLength;
                fadeOutInterval = source.volume / fadeOutLength;
            }
            if (fadingOutFlag && fadeOutTimer > 0)
            {
                // Do fadeout step until timer runs out
                FadeOutStep();
                fadeOutTimer -= Time.deltaTime;
            } 
            
            // clip should now be over, whether faded out or its just done
            if (fadeIn && !fadingInFlag && !source.isPlaying)
            {
                SelectNextSong();
                fadingInFlag = true;
                fadeInInterval = defaultVolume / fadeInLength;
                fadeInTimer = fadeInLength;
                source.volume = 0;
                source.Play();

            }
            else if (!source.isPlaying)
            {
                SelectNextSong();
                source.volume = defaultVolume;
                source.Play();
            }
            if(fadingInFlag && fadeInTimer > 0)
            {
                FadeInStep();
                fadeInTimer -= Time.deltaTime;
            }


        }

    }

    private void SelectNextSong()
    {
        currentSongIndex++;
        if (currentSongIndex >= clips.Count)
        {
            currentSongIndex = 0;
        }
        source.clip = clips[currentSongIndex];
    }

    public void SetVolume(float newVolume)
    {
        source.volume = newVolume;
    }

    public float GetVolume()
    {
        return source.volume;
    }

    private void FadeOutStep()
    {
        source.volume -= defaultVolume * Time.deltaTime / fadeOutLength;
        // last fadeout step
        if(source.volume <= 0)
        {
            source.Stop();
            source.volume = defaultVolume;
            fadingOutFlag = false;
        }
    }

    private void FadeInStep()
    {
        //Debug.Log("Current Volume while Fading In: " + source.volume);
        source.volume += defaultVolume * Time.deltaTime / fadeInLength;
        if (source.volume >= defaultVolume)
        {
            source.volume = defaultVolume; // make sure there are not float fragments
            fadingInFlag = false;
        }
    }

    public bool IsActive()
    {
        return active;
    }

    public void Deactivate()
    {
        if (!active)
        {
            Debug.LogWarning(this.gameObject.name + " (Background Sound) is already deactivated");
            return;
        }

        active = false;
        source.Stop();
    }

    public void Activate()
    {
        if (active)
        {
            Debug.LogWarning(this.gameObject.name + " (Background Sound) is already activated");
            return;
        }

        active = true;
        source.Play();
    }

 
}
