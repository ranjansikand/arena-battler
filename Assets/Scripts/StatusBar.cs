using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 1f;
    private const float SHRINK_SPEED = 2f;

    public Image _frontBar, _backBar;
    private float damagedHealthShrinkTimer;
    private float _currentValue, _maxValue;

    public void InitializeStatusBar(float startingValue, float maxValue) {
        _currentValue = startingValue;
        _maxValue = maxValue;
        _frontBar.fillAmount = _backBar.fillAmount = _currentValue / _maxValue;
    }

    public void Decrease(float decreaseAmount) {
        StopAllCoroutines();

        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        _currentValue = Mathf.Clamp(_currentValue - decreaseAmount, 0, _maxValue);
        _frontBar.fillAmount = _currentValue / _maxValue;

        StartCoroutine(UpdateStatusBarRoutine());
    }

    public void Increase(float increaseAmount) {
        _currentValue = Mathf.Clamp(_currentValue + increaseAmount, 0, _maxValue);;
        _frontBar.fillAmount = _backBar.fillAmount = _currentValue / _maxValue;
    }

    IEnumerator UpdateStatusBarRoutine() {
        bool updated = false;

        while (!updated) {
            damagedHealthShrinkTimer -= Time.deltaTime;
            if (damagedHealthShrinkTimer < 0) {
                if (_frontBar.fillAmount < _backBar.fillAmount) {
                    _backBar.fillAmount -= SHRINK_SPEED * Time.deltaTime;
                } else {
                    updated = true;
                    StopAllCoroutines();
                }
            }

            yield return null;
        }
    }
}