using UnityEngine;
using static System.Net.WebRequestMethods;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundInstance : MonoBehaviour
{
    private AudioSource _audioSource;

    public string ClipName { get { return _audioSource.clip.name; } }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioMixerGroup audioMixer, float delay, bool isLoop)
    {
        _audioSource.outputAudioMixerGroup = audioMixer;
        _audioSource.loop = isLoop;
        _audioSource.Play();

        if (!isLoop) { StartCoroutine(DestroyWhenFinishCoroutine(_audioSource.clip.length)); }
    }

    public void InitSound2D(AudioClip clip)
    {
        _audioSource.clip = clip;
    }

    public void InitSound3D(AudioClip clip, float minDistance, float maxDistance)
    {
        _audioSource.clip = clip;
        _audioSource.spatialBlend = 1.0f;
        _audioSource.rolloffMode = AudioRolloffMode.Linear;
        _audioSource.minDistance = minDistance;
        _audioSource.maxDistance = maxDistance;
    }

    private IEnumerator DestroyWhenFinishCoroutine(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        Destroy(gameObject);
    }
}
