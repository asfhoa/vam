using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AUDIO_STATE
{
    PLAY,
    PAUSE,
    UNPAUSE,
    STOP,
}
public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSe sePrefab;
    [SerializeField] AudioClip[] clips;

    public void PlayBGM(AUDIO_STATE state = 0)
    {
        switch (state)
        {
            case AUDIO_STATE.PLAY:
                bgmSource.Play();   //오디오 실행    //Play는 무조건 처음부터 실행시킨다
                break;
            case AUDIO_STATE.PAUSE:
                bgmSource.Pause();  //오디오 정지
                break;
            case AUDIO_STATE.UNPAUSE:
                bgmSource.UnPause();//오디오 재실행   //멈춘 부분부터 다시 시작한다
                break;
            case AUDIO_STATE.STOP:
                bgmSource.Stop();   //오디오 정지
                break;
        }
    }
    public void PlaySe(string name, float volumn = 1.0f)
    {
        //실행할 오디오를 찾는다
        AudioClip clip = System.Array.Find(clips, clip => clip.name.ToLower() == name.ToLower());
        if (clip != null)
            Instantiate(sePrefab, transform).Play(clip, volumn);    //새로운 오브젝트를 생성시켜서 오디오를 재생
    }

}
