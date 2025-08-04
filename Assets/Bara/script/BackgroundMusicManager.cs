using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Background Music Settings")]
    public AudioSource musicAudioSource;
    public AudioClip backgroundMusic;
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    public bool playOnStart = true;
    public bool loopMusic = true;
    public bool persistBetweenScenes = false;
    
    [Header("Dialogue Integration")]
    public bool lowerVolumeInDialogue = true;
    [Range(0f, 1f)]
    public float dialogueVolumeMultiplier = 0.3f;

    private static BackgroundMusicManager instance;

    void Awake()
    {
        // Singleton pattern - only one music manager at a time
        if (persistBetweenScenes)
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    void Start()
    {
        // Initialize audio source if not assigned
        if (musicAudioSource == null)
        {
            musicAudioSource = GetComponent<AudioSource>();
            if (musicAudioSource == null)
            {
                musicAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Setup audio source
        if (backgroundMusic != null)
        {
            musicAudioSource.clip = backgroundMusic;
            musicAudioSource.loop = loopMusic;
            musicAudioSource.volume = musicVolume;
            musicAudioSource.playOnAwake = false;

            if (playOnStart)
            {
                PlayMusic();
            }
        }
    }

    void Update()
    {
        // Handle volume based on dialogue state
        if (musicAudioSource != null)
        {
            if (lowerVolumeInDialogue && IsInDialogue())
            {
                // Lower volume during dialogue
                musicAudioSource.volume = musicVolume * dialogueVolumeMultiplier;
            }
            else
            {
                // Normal volume when not in dialogue
                musicAudioSource.volume = musicVolume;
            }
        }
    }
    
    // Check if player is in dialogue
    bool IsInDialogue()
    {
        if (DialogueManager.Instance != null)
        {
            return DialogueManager.Instance.IsDialogueActive;
        }
        return false;
    }

    public void PlayMusic()
    {
        if (musicAudioSource != null && backgroundMusic != null)
        {
            musicAudioSource.Play();
            Debug.Log("Background music started playing");
        }
    }

    public void StopMusic()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            Debug.Log("Background music stopped");
        }
    }

    public void PauseMusic()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
            Debug.Log("Background music paused");
        }
    }

    public void ResumeMusic()
    {
        if (musicAudioSource != null && !musicAudioSource.isPlaying)
        {
            musicAudioSource.UnPause();
            Debug.Log("Background music resumed");
        }
    }

    public void SetVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = musicVolume;
        }
    }

    public void ChangeMusic(AudioClip newMusic)
    {
        if (musicAudioSource != null)
        {
            StopMusic();
            backgroundMusic = newMusic;
            musicAudioSource.clip = newMusic;
            if (playOnStart)
            {
                PlayMusic();
            }
        }
    }

    // Call this from PauseManager when game is paused
    public void OnGamePaused()
    {
        if (musicAudioSource != null)
        {
            PauseMusic();
        }
    }

    // Call this from PauseManager when game is resumed
    public void OnGameResumed()
    {
        if (musicAudioSource != null)
        {
            ResumeMusic();
        }
    }
}
