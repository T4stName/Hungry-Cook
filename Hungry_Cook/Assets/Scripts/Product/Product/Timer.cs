using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Timer : MonoBehaviour
{
    public event UnityAction OnLose;
  [SerializeField] private Image _timer;
  private float _time;
  private float _fillValue;
   private float _currentTime;
   [SerializeField] private float _timeForPrepare;
   private void Start() {
    StartCoroutine(Reload());
   }
   private IEnumerator Reload()
   {
            _fillValue = _currentTime;
        _fillValue = _fillValue / _timeForPrepare;
        _timer.fillAmount = _fillValue;
    yield return new WaitForSeconds(1);
        _currentTime++;
        if (_currentTime >= _timeForPrepare)
        {
            OnLose?.Invoke();
            Settings settings = FindObjectOfType<Settings>();
            settings.OpenMenu();
        }
        else
        {
         StartCoroutine(Reload());
        }
   }
}
