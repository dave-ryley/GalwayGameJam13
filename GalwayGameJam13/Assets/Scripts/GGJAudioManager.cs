using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGJAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _assetLoaded;
    private bool _playOnAssetLoad;

    public void Setup()
    {
        string assetName = "RoughMusic";
        _audioSource = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load(assetName) as AudioClip;
        if (clip == null)
        {
            _audioSource.clip = null;
            Debug.LogWarningFormat("Loaded asset is null when setting asset: {0}", assetName);
        }
        else
        {
            _assetLoaded = true;
            if (_audioSource.isActiveAndEnabled)
            {
                _audioSource.clip = clip;
                if (_playOnAssetLoad)
                {
                    _playOnAssetLoad = false;
                    Play();
                }
            }
            else
            {
                Debug.LogWarningFormat("AudioSource not active or enabled when setting asset: {0}", assetName);
            }
        }

 
            DontDestroyOnLoad(transform.gameObject);
        

        if (FindObjectsOfType<GGJAudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

    }


    public void Mute()
    {
        if (_audioSource.isPlaying)
        { Stop(); }
        else { Play(); }
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}
