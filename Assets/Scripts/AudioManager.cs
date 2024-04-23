using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----Audio Sources----")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SfxSource;

    [Header("----Audio Clips----")]
    public AudioClip Background;
    public AudioClip Shoot;
    public AudioClip Monster;
    public AudioClip Victory;
    public AudioClip Death;
    public AudioClip Button;
    public AudioClip Walking;


    //play between scenes
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //Function to start music one
    private void Start()
    {
        MusicSource.clip = Background;
        MusicSource.Play();
    }

    //Function to play sound effects
    public void PlaySFX(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
}
