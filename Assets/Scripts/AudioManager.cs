using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource Musicsource;


    public AudioClip Background;


    //play between scenes
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //Function to start music one
    private void Start()
    {
        Musicsource.clip = Background;
        Musicsource.Play();
    }
}
