using TMPro;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    [SerializeField] private float _maxTimeInSeconds;
    [SerializeField] private TextMeshProUGUI _timerTextMesh;
    [SerializeField] private DeathLine _deathLineObject;
    [SerializeField] private float _deathLineReductionValue = 3.0f;
    [SerializeField] private GameManager _gameManager;
    private float _currTimeInSeconds = 0.0f;
    private bool _isStartTimer = false;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (!_isStartTimer)
        {
            return;
        }

        if (_currTimeInSeconds < _maxTimeInSeconds)
        {
            _currTimeInSeconds += Time.deltaTime;
            _timerTextMesh.text = GetRemainingTimeAsString();
        }
        else
        {
            StopTimer();
            TimesUp();
        }
    }

    private void TimesUp()
    {
        _gameManager.GameOver();
    }

    public void StartTimer()
    {
        _isStartTimer = true;
    }

    private void StopTimer()
    {
        _isStartTimer = false;
    }

    private string GetRemainingTimeAsString()
    {
        float remainingTime = _maxTimeInSeconds - _currTimeInSeconds;
        return TimeToString(remainingTime);
    }

    private string TimeToString(float time)
    {
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;
        return $"{minutes} : {Pad(seconds)}";
    }

    private string Pad(int time)
    {
        if (time.ToString().Length == 1)
        {
            return $"0{time}";
        }
        return time.ToString();
    }

    private void MoveDeathLineObject()
    {
        Vector3 _reductionTransform = new Vector3(_deathLineObject.transform.localPosition.x,
            _deathLineObject.transform.localPosition.y - _deathLineReductionValue, _deathLineObject.transform.localPosition.z);

        _deathLineObject.transform.localPosition = _reductionTransform;
    }
}
