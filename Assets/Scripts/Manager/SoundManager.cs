using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonMonobehavior<SoundManager>
{
    [SerializeField] private AudioMixer _audioMixer;
    private float _bgmVolume;
    private float _effectVolume;

    [SerializeField] private List<AudioClip> _audioClipList = new List<AudioClip>();
    private Dictionary<string, AudioClip> _audioClipDict = new Dictionary<string, AudioClip>();
    private List<SoundInstance> _instanceList = new List<SoundInstance>();

    private void Start()
    {
        foreach(AudioClip clip in _audioClipList)
        {
            _audioClipDict.Add(clip.name, clip);
        }
    }

    private AudioClip GetClip(string clipName)
    {
        if(_audioClipDict.TryGetValue(clipName, out AudioClip clip))
        {
            return clip;
        }
        return null;
    }

    public void PlaySound2D(string clipName, float delay = 0f, bool isLoop = false, GameEnum.SoundType type = GameEnum.SoundType.Effect)
    {
        GameObject obj = new GameObject("TemporarySoundPlayer 2D");
        SoundInstance soundPlayer = obj.AddComponent<SoundInstance>();

        if (isLoop) { _instanceList.Add(soundPlayer); }

        soundPlayer.InitSound2D(GetClip(clipName));
        soundPlayer.Play(_audioMixer.FindMatchingGroups(type.ToString())[0], delay, isLoop);
    }

    public void PlaySound3D(string clipName, Transform audioTarget, float delay = 0f, bool isLoop = false, GameEnum.SoundType type = GameEnum.SoundType.Effect, bool attachToTarget = true, float minDistance = 0.0f, float maxDistance = 50.0f)
    {
        GameObject obj = new GameObject("TemporarySoundPlayer 3D");
        obj.transform.localPosition = audioTarget.transform.position;
        if (attachToTarget) { obj.transform.parent = audioTarget; }

        SoundInstance soundPlayer = obj.AddComponent<SoundInstance>();

        if (isLoop) { _instanceList.Add(soundPlayer); }

        soundPlayer.InitSound3D(GetClip(clipName), minDistance, maxDistance);
        soundPlayer.Play(_audioMixer.FindMatchingGroups(type.ToString())[0], delay, isLoop);
    }
    public void InitVolumes(float bgm, float effect)
    {
        SetVolume(GameEnum.SoundType.BGM, bgm);
        SetVolume(GameEnum.SoundType.Effect, effect);
    }

    public void SetVolume(GameEnum.SoundType type, float value)
    {
        _audioMixer.SetFloat(type.ToString(), value);
    }

}
