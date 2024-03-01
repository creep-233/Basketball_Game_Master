using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    
    [Header("-----Audio Clip-----")]

    public AudioClip background;
    public AudioClip clickBtn;
    public AudioClip walk1;
    public AudioClip walk2;
    public AudioClip jump;
    public AudioClip shot;
    public AudioClip shotIn;
    public AudioClip wallTouch;
    public AudioClip timeUp;
    public AudioClip countDownTime;
    

    public static AudioManager instance;
    public delegate void AudioManagerInitialized(AudioManager manager);
    public static event AudioManagerInitialized OnAudioManagerInitialized;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            OnAudioManagerInitialized?.Invoke(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start(){
        musicSource.clip=background;
        musicSource.Play();
    }   
    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}
