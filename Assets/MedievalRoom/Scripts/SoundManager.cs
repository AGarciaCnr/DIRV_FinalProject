using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioClip backgroundMusicSource;

    public AudioClip[] audioSources; // Array of AudioSources for playing different sounds

    // AudioSource reference
    public AudioSource backgroundAudioSource;
    // AudioSource reference
    public AudioSource audioSource;

    private bool isAudioPlaying = false;
    public int audioCounterPosition = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        if (backgroundAudioSource != null)
            PlayBackgroundMusic();
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform)
    {
        // Spawn in gameObject
        //AudioSource audioSource = Instantiate();
    }

    public void PlayBackgroundMusic()
    {
        backgroundAudioSource.volume = 1.1f;
        backgroundAudioSource.PlayOneShot(backgroundMusicSource);
    }

    public IEnumerator PlayClip(int indexToPlay, float WaitTimeToStart)
    {
        foreach (AudioClip a in audioSources)
        {
            int indexAudioSources = Array.IndexOf(audioSources, a);
            if (audioSource.isPlaying)
            {
                Debug.Log("Is Playing");
            }
            else
            {
                isAudioPlaying = false;
                yield return new WaitForSeconds(WaitTimeToStart);
                Debug.Log("It is not playing");
            }

            if (indexToPlay == indexAudioSources && !isAudioPlaying && audioCounterPosition == indexToPlay)
            {
                audioSource.volume = 1f;
                audioSource.PlayOneShot(a);
                isAudioPlaying = true;
                audioCounterPosition++;
            }
        }
    }
}
