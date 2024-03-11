using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSe : MonoBehaviour
{
    [SerializeField] AudioSource source;
    
    public void Play(AudioClip clip, float volumn)
    {
        gameObject.SetActive(true);
        source.clip = clip;
        source.volume = volumn;
        source.Play();
    }

    private void Update()
    {
        if (!source.isPlaying)
            Destroy(gameObject);    //오디오의 재생이 끝나면 자동으로 오브젝트를 삭제
    }
}
