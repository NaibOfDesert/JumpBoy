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




    // Start is called before the first frame update
    void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // if()
    }

    public void PlayAudioEffect(string value, bool able)
    {
        switch (value)
        {
            case "move":
                {
                    // PlayClip(moveClip, moveVolume);
                    audioSource.clip = moveClip;
                    audioSource.enabled = able;

                    break;
                }
            case "jump":
                {
                    audioSource.clip = jumpClip;
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
