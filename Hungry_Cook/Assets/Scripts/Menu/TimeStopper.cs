using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopper : MonoBehaviour
{
     private  AudioSource[] _audioSoures;
     [SerializeField] private AudioSource _music;
     [SerializeField] private GameObject _information;
    private bool _isStopped = false;
    private void Start() {
        Invoke(nameof(Find),0.3f);
    }
    private void Find()
    {
        _audioSoures = FindObjectsOfType<AudioSource>();
    }
    public void ChangeTime()
    {
        _isStopped =! _isStopped;
        Time.timeScale = _isStopped ? 0 : 1;
        if (_isStopped)
        {
            _information.SetActive(true);
            _music.Pause();
        }
        else
        {
            _information.SetActive(false);
            _music.Play();
        }
        foreach (var item in _audioSoures)
        {
            if (_isStopped)
            {
                item.Pause();
            }
        }
    }
}
