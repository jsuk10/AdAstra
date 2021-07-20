using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : UnitySingleton<SoundManager>
{
    [SerializeField] private AudioSource backGroundSound;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource pancleSound;
    private AudioClip[] clipFiles;
    private Dictionary<string, AudioClip> audioClipDirctionary;

    public override void OnCreated()
    {
    }

    public override void OnInitiate()
    {
        GetSoundsFromResources();
    }


    /// <summary>
    /// 모든 리소스를 파싱해주는 함수
    /// </summary>
    private void GetSoundsFromResources()
    {
        clipFiles = Resources.LoadAll<AudioClip>("Sound");

        audioClipDirctionary = new Dictionary<string, AudioClip>();
        for (int i = 0; i < clipFiles.Length; i++)
        {
            Debug.Log(clipFiles[i].name);
            audioClipDirctionary.Add(clipFiles[i].name, clipFiles[i]);
        }
    }

    /// <summary>
    /// 배경음악의 사운드를 조절해 주는 함수
    /// </summary>
    /// <param name="val"></param>
    public void BackGroundSound(float val) {
        audioMixer.SetFloat("BackGround",Mathf.Log10(val) * 20);
    }

    /// <summary>
    /// 효과음 사운드를 조절해주는 함수
    /// </summary>
    /// <param name="val"></param>
    public void SFXSound(float val)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(val) * 20);
    }

    /// <summary>
    /// 효과음 파일의 이름을 넣어주면 재생해주는 스크립트
    /// 파일은 Resources/Sound/FX에 있어야 됨.
    /// </summary>
    /// <param name="soundName"></param>
    public void SFXPlayer(string soundName) {
        GameObject sound = new GameObject(soundName + "Sound");
        AudioSource audiosource = sound.AddComponent<AudioSource>();
        AudioClip clip = audioClipDirctionary[soundName];

        audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = clip;
        audiosource.Play();
        Destroy(sound, clip.length);
    }

    public void PlayDrawingSound()
    {
        pancleSound.mute = false;
        pancleSound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];

    }

    public void StopDrawingSound()
    {
        pancleSound.mute = true;
    }

    /// <summary>
    /// 배경음악을 재생해주는 함수
    /// 게임 시작전에 0 게임 시작시 1
    /// </summary>
    public void PlayBackGroundSound(int index) {
        Debug.Log($"BackGround{index}");
        AudioClip clip = audioClipDirctionary[$"BackGround{index}"];
        backGroundSound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BackGround")[0];
        backGroundSound.clip = clip;
        backGroundSound.loop = true;
        backGroundSound.volume = 0.1f;
        backGroundSound.Play();
    }
}
