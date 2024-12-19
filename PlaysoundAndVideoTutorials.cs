// Updated scripts for Unity (latest version)
// Includes implementation tutorials

// PlaySound.cs
using UnityEngine;

/// <summary>
/// This script plays a sound when the player enters a trigger zone.
/// Attach it to the GameObject with the trigger collider.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour {

    public AudioClip SoundToPlay;
    [Range(0f, 1f)] public float Volume = 1.0f;
    private AudioSource _audioSource;
    private bool _alreadyPlayed = false;

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (!_alreadyPlayed && other.CompareTag("Player")) {
            _audioSource.PlayOneShot(SoundToPlay, Volume);
            _alreadyPlayed = true;
        }
    }
}

/*
**Tutorial:**
1. Attach this script to a GameObject with a trigger collider.
2. Assign the AudioClip in the inspector.
3. Ensure the "Player" GameObject has the tag "Player."
4. Attach an AudioSource component to the GameObject and configure the settings as needed.
*/


// PlaySound1.cs
using UnityEngine;

/// <summary>
/// This script plays a sound when the player enters a trigger zone (variation 1).
/// Attach it to the GameObject with the trigger collider.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlaySound1 : MonoBehaviour {

    public AudioClip SoundToPlay;
    [Range(0f, 1f)] public float Volume = 1.0f;
    private AudioSource _audioSource;
    private bool _alreadyPlayed = false;

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (!_alreadyPlayed && other.CompareTag("Player")) {
            _audioSource.PlayOneShot(SoundToPlay, Volume);
            _alreadyPlayed = true;
        }
    }
}

/*
**Tutorial:**
1. Similar to PlaySound.cs, attach this script to a GameObject with a trigger collider.
2. Assign an AudioClip in the inspector.
3. Make sure the "Player" GameObject has the tag "Player."
4. Add an AudioSource component to the GameObject and configure it as needed.
*/


// PlayVideo.cs
using UnityEngine;

/// <summary>
/// This script activates a video player GameObject when the player enters a trigger zone.
/// Attach it to a GameObject with a trigger collider.
/// </summary>
public class PlayVideo : MonoBehaviour {

    public GameObject videoPlayer;
    public float timeToStop = 5.0f;

    void Start() {
        if (videoPlayer != null)
            videoPlayer.SetActive(false);
        else
            Debug.LogError("VideoPlayer GameObject not assigned.");
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && videoPlayer != null) {
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);
        }
    }
}

/*
**Tutorial:**
1. Attach this script to a GameObject with a trigger collider.
2. Assign a video player GameObject to the `videoPlayer` field in the inspector.
3. Make sure the "Player" GameObject has the tag "Player."
4. Set the `timeToStop` variable to define how long the video will play.
5. Ensure the video player GameObject is initially inactive.
*/
