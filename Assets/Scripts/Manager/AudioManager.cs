using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips; // ����� Ŭ�� �迭

    private AudioSource bgmSource = null; // BGM�� ����� �ҽ�
    private List<AudioSource> sfxSources = new List<AudioSource>(); // ȿ������ ����� �ҽ� ���

    //Audio
    //1: BGM, 2: Block ON, 3: Block OFF
    //4: Tower ON, 5: Tower OFF
    //6: 

    private void Awake()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = audioClips[0]; 
        // ȿ������ AudioSource ������Ʈ�� �ʱ�ȭ�մϴ�.
        for (int i = 0; i < 10; i++) // �ʱ⿡�� 10���� AudioSource�� �غ��մϴ�.
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSources.Add(sfxSource);
        }
    }


    // ��� ������ AudioSource�� ã�� �Լ�
    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        // ��� AudioSource�� ��� ���� ��� ���ο� AudioSource�� �߰��մϴ�.
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        sfxSources.Add(newSource);
        return newSource;
    }

    // ȿ������ ����ϴ� �Լ�
    public void PlaySFX(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= audioClips.Length)
        {
            Debug.LogWarning("�߸��� Ŭ�� �ε����Դϴ�.");
            return;
        }

        AudioSource sfxSource = GetAvailableSFXSource();
        if(PlayerPrefs.HasKey("Volume_Effect")) //ȿ���� ���� ���� 
        {
            sfxSource.volume = PlayerPrefs.GetFloat("Volume_Effect") / 10f; 
        }
        sfxSource.PlayOneShot(audioClips[clipIndex]);
    }

    public void StopSFX(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= audioClips.Length)
        {
            Debug.LogWarning("�߸��� Ŭ�� �ε����Դϴ�.");
            return;
        }
        AudioSource sfxSource = GetAvailableSFXSource();
        sfxSource.Stop(); 
    }

    public void PlayBGM()
    {
        bgmSource = GetComponentsInChildren<AudioSource>()[0]; 
        if (PlayerPrefs.HasKey("Volume_BGM")) //BGM ���� ���� 
        {
            bgmSource.volume = PlayerPrefs.GetFloat("Volume_BGM") / 10f; 
        }
        bgmSource.loop = true; // �ݺ� ��� ����
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource = GetComponentsInChildren<AudioSource>()[0];
        bgmSource.Stop(); 
    }
}
