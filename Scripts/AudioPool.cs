using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    [SerializeField] private Transform sourcesParent;
    [SerializeField] private AudioSource sourcePrefab;
    
    [Space(20f)]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private int pollSize;
    
    private Queue<AudioSource> _pollAudioSources;
    
    #region AlmostSingleton
    public static AudioPool Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        CreatePoll();
    }

    private void CreatePoll()
    {
        _pollAudioSources = new Queue<AudioSource>();
        for (int i = 0; i < pollSize; i++)
        {
            _pollAudioSources.Enqueue(Instantiate(sourcePrefab, sourcesParent));
        }
    }
    
    public void PlaySound(AudioClip clip)
    {
        AudioSource pollAudioSource = _pollAudioSources.Dequeue();
        pollAudioSource.clip = clip;
        pollAudioSource.Play();
        _pollAudioSources.Enqueue(pollAudioSource);
    }
}
