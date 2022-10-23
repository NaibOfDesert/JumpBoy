using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    AudioSource audioSource;
    PlayerController playerController;

    [Header("Idle")]
    [SerializeField] AudioClip idleClip;
    [SerializeField] [Range(0f, 1f)] float idleVolume = 1f;

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
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        /*
        if (playerController.GetMovementStatusChange())
        {
            audioSource.enabled = false;
            PlayAudioEffect(playerController.GetMovementStatus());
            Debug.Log(playerController.GetMovementStatus());
        }
        */

    }

    public void PlayAudioEffect(string value)
    {
        audioSource.enabled = false;
        switch (value)
        {
            case "Idle":
                {
                    audioSource.clip = idleClip;
                    audioSource.loop = false;
                    audioSource.volume = idleVolume;
                    break;
                }

            case "Move":
                {
                    audioSource.clip = moveClip;
                    audioSource.loop = true;
                    audioSource.volume = moveVolume;
                    break;
                }

            case "Jump":
                {
                    audioSource.clip = jumpClip;
                    audioSource.loop = false;
                    audioSource.volume = jumpVolume;
                    break;
                }

            case "JumpEnd":
                {
                    audioSource.clip = jumpClip;
                    audioSource.loop = false;
                    audioSource.volume = jumpVolume;
                    break;
                }

            case "Slide":
                {
                    audioSource.clip = slideClip;
                    audioSource.loop = true;
                    audioSource.volume = slideVolume;
                    break;
                }

            case "Dead":
                {
                    audioSource.clip = deadClip;
                    audioSource.loop = false;
                    audioSource.volume = deadVolume;
                    break;
                }

            default:
                {
                    break;
                }
        }

        audioSource.enabled = true;

    }

    void PlayClip(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
