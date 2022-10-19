using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Move")]
    [SerializeField] AudioClip moveClip;
    [SerializeField] [Range(0f, 1f)] float moveVolume = 1f;

    [Header("Jump")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField] [Range(0f, 1f)] float jumpVolume = 1f;


    [Header("Slide")]
    [SerializeField] AudioClip slideClip;
    [SerializeField] [Range(0f, 1f)] float slideVolume = 1f;

    [Header("Dead")]
    [SerializeField] AudioClip deadClip;
    [SerializeField] [Range(0f, 1f)] float deadVolume = 1f;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }



    public void PlayAudioEffect(string value, bool able)
    {
        switch (value)
        {   
            case "Move":
                {
                    audioSource.clip = moveClip;
                    audioSource.loop = true;
                    audioSource.volume = moveVolume;
                    audioSource.enabled = able;
                    break;
                }

            case "Jump":
                {
                    audioSource.clip = jumpClip;
                    audioSource.loop = false;
                    audioSource.volume = jumpVolume;
                    audioSource.enabled = true;
                    Debug.Log("jump sound on/off"); 
                    break;
                }

            case "Slide":
                {
                    audioSource.clip = slideClip;
                    audioSource.loop = true;
                    audioSource.volume = slideVolume;
                    audioSource.enabled = able;
                    break;
                }

            case "Dead":
                {
                    audioSource.clip = deadClip;
                    audioSource.loop = false;
                    audioSource.volume = deadVolume;
                    audioSource.enabled = able;
                    break;
                }

            default:
                {
                    break;
                }
        }

    }

    void PlayClip(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
