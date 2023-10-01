using UnityEngine;
public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 0.5f)
    {
        audioSrc.pitch = 0.8f;
        audioSrc.PlayOneShot(clip);
    }
}