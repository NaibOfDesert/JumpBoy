using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] AudioClip moveClip;
    [SerializeField] [Range(0f, 1f)] float moveVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioEffect(string value)
    {
        switch (value)
        {
            case "move":
                {
                    PlayClip(moveClip, moveVolume);
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
