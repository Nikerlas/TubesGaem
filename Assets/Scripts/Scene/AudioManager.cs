using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuAndLevelMusic;
    private AudioClip otherSceneMusic;

    private AudioSource audioSource;
    private static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (sceneName == "Menu" || sceneName == "Level" || sceneName == "LevelGame2" || sceneName == "Credit")
        {
            if (audioSource.clip != menuAndLevelMusic)
            {
                audioSource.clip = menuAndLevelMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != otherSceneMusic)
            {
                audioSource.clip = otherSceneMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}
