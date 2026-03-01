using UnityEngine;
using UnityEngine.InputSystem;

public class ClickSound : MonoBehaviour
{
    public AudioClip clickSound;

    [Range(0f, 1f)]
    public float volume = 0.8f;

    private AudioSource source;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 0f; // 2D
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (clickSound != null)
                source.PlayOneShot(clickSound, volume);
        }
    }
}
