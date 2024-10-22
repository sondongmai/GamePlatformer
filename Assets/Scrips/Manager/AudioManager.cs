using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    // Start is called before the first frame update

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private int bgmIndex;
    private void Awake()
    {
       DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;

        }    
        else
        {
            Destroy(this.gameObject);
        }

        if (bgm.Length <= 0)
            return;

        InvokeRepeating(nameof(PlayMusicIfNeeded), 0, 1);
       
    }

    public void PlaySFX(int sfxToPlay, bool randomBitch = true)
    {
        if (sfxToPlay >= sfx.Length)
            return;
        if (randomBitch)
            sfx[sfxToPlay].pitch = Random.Range(.9f, 1.1f);

        sfx[sfxToPlay].Play();
    }
    
    public void StopSFX(int sfxToStop)
    {
        if (sfxToStop >= sfx.Length)
            return;
        sfx[sfxToStop].Stop();
    } 
    
    public void PlayMusicIfNeeded()
    {
        if (bgm[bgmIndex].isPlaying == false)
            PlayBgmRandom();
    }    
    public void PlayBgmRandom()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBgm(bgmIndex);
        
    }    

    public void PlayBgm(int bgmToPlay)
    {
        if (bgm.Length <= 0)
            return;

        for (int i = 0;i< bgm.Length; i++)
        {
            bgm[i].Stop();
        }
        bgmIndex = bgmToPlay;
        bgm[bgmToPlay].Play();
    }    
}
