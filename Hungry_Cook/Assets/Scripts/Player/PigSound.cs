using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _loseSoudns;
    [SerializeField] private AudioClip[] _winSoudns;
    [SerializeField] private AudioClip _pigSound;
    [SerializeField] private AudioClip[] _canDoItSounds;
    [SerializeField] private AudioClip[] _coughtSounds;
    [SerializeField] private AudioSource _audioSourse;
    private Timer _timer;
    private float _time;
    private int _currentTime;
    private int _waitTime;
    private void Start()
    {
       GetRandomTime();
        HarpoonTip harpoonTip = FindObjectOfType<HarpoonTip>();
        harpoonTip.OnCought += PlayCoughtSound;
        Utensils[] utensils = FindObjectsOfType<Utensils>();
        foreach (var item in utensils)
        {
            //item.OnError += PlayErrorSound;
        }
        Plate plate = FindObjectOfType<Plate>();
        plate.OnWin += PlayWinSound;
        _timer = FindObjectOfType<Timer>();
        _timer.OnLose += PlayLoseSound;
    }
    private void Update() 
    {
       _time += Time.deltaTime;
       if (_time >= 1)
       {
        _currentTime ++;
         if (_waitTime <= _currentTime)
         {
            GetRandomTime();
            PlayPigSound();
            _currentTime = 0;
         }
         _time = 0;
         
       }  
    }
    private void GetRandomTime()
    {
        _waitTime = Random.Range(30, 60);
    }
    private void PlayPigSound()
    {
        _audioSourse.clip = _pigSound;
        _audioSourse.Play();
    }
    private void PlayCoughtSound()
    {
        int random = Random.Range(0, _coughtSounds.Length);
        _audioSourse.clip =  _coughtSounds[random];
        _audioSourse.Play();
        Check();
    }
    private void Check()
    {
        if ((_waitTime - _currentTime) < 3)
        {
            _currentTime = 0;
            GetRandomTime();
        }
    }
    private void PlayLoseSound()
    {
        int random = Random.Range(0, _loseSoudns.Length);
        _audioSourse.clip =  _loseSoudns[random];
        _audioSourse.Play();
        Check();
    }
    private void PlayWinSound()
    {
        int random = Random.Range(0, _winSoudns.Length);
        _audioSourse.clip =  _winSoudns[random];
        _audioSourse.Play();
        Check();
    }
    private void PlayErrorSound()
    {
        int random = Random.Range(0, _canDoItSounds.Length);
        _audioSourse.clip =  _canDoItSounds[random];
        _audioSourse.Play();
        Check(); 
    }
}
