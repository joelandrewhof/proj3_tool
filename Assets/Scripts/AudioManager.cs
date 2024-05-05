using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        GameObject audioObj = new GameObject("2DSound");
        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;

        source.Play();
        Object.Destroy(audioObj, clip.length);
        return source;
    }
}