using UnityEngine;

public class GameSoundManager : MonoBehaviour
{

    public AudioSource soundFXSource;
    public AudioSource musicSource;
    public AudioClip angryCatMeow;
    public AudioClip cannonFire;
    public AudioClip catMeowShort;
    public AudioClip hitSound;
    public AudioClip zoom;


    private static GameSoundManager m_instance = null;
    public static GameSoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                var prefab = Resources.Load<GameObject>("GameSoundManager");
                if (prefab == null) Debug.LogError("Can't load GameSoundManager from Resources");
                var instance = Instantiate(prefab);
                if (instance == null) Debug.LogError("Instance of GameSoundManager prefab is null");
                m_instance = instance.GetComponent<GameSoundManager>();
                if (m_instance == null) Debug.LogError("No GameSoundManager found on prefab instance.");
            }
            return m_instance;
        }
    }

    public void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            DestroyObject(gameObject);
            return;
        }
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip, float scale)
    {
        if (clip != null) soundFXSource.PlayOneShot(clip, scale);
    }

    public void PlayAngryCatMeow()
    {
        Play(angryCatMeow, 1);
    }

    public void PlayCannonFire()
    {
        Play(cannonFire, 1);
    }

    public void PlayCatMeowShort()
    {
        Play(catMeowShort, 1);
    }

    public void PlayHitSound()
    {
        Play(hitSound, 1);

    }

    public void PlayZoom()
    {
        Play(zoom, 1);
    }
}