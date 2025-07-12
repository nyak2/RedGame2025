using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    private AudioSource _audioSrc;

    private void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySfx(AudioClip sfx)
    {
        _audioSrc.clip = sfx;
        _audioSrc.Play();
    }
}
