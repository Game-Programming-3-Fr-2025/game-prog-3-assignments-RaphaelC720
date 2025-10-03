using System;
using TarodevGhost;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private Transform _recordTarget;
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private GameObject NewBestText;


    [SerializeField, Range(1, 10)] private int _captureEverNFrames = 2;
    [SerializeField] private float _maxRecordingTime = 180f;

    private ReplaySystem _replay;
    private GameObject _currentGhost;
    private bool _isRecording;
    private float _elapsedTime;
    private const string PREV_KEY_BEST = "BestGhostRun";

    [SerializeField] private Slime_Script player;
    private void Awake()
    {
        _replay = new ReplaySystem(this);
        Debug.Log("[DebugGhostManager] Awake: ReplaySystem created.");

        if (PlayerPrefs.HasKey(PREV_KEY_BEST))
        {
            try
            {
                var data = PlayerPrefs.GetString(PREV_KEY_BEST);
                var saved = new Recording(data);
                _replay.SetSavedRun(saved);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to load saved ghost: {e.Message}");
            }
        }
    }
    private void OnEnable()
    {
        EndScript.flagTouched += OnFlagTouched;
        Debug.Log("[DebugGhostManager] Subscribed to EndScript.flagTouched.");
    }
    private void OnDisable()
    {
        EndScript.flagTouched -= OnFlagTouched;
        Debug.Log("[DebugGhostManager] Unsubscribed from EndScript.flagTouched.");
    }

    private void OnFinishLineCrossed(bool runStarting)
    {
        if (runStarting)
        {
            _replay.StartRun(_recordTarget, _captureEverNFrames);
            _replay.PlayRecording(RecordingType.Best, Instantiate(_ghostPrefab));
        }
        else
        {
            _replay.FinishRun();
            _replay.StopReplay();
        }
    }

    private void Update()
    {
        if (_isRecording)
        {
            _elapsedTime += Time.deltaTime;
            if (_timerText != null) _timerText.text = FormatTime(_elapsedTime);
        }  
        
        
        if (_isRecording)
        {
            _elapsedTime += Time.deltaTime;
            if (_timerText != null) _timerText.text = $"{_elapsedTime:0.000}s";
        }
    }

    private void OnFlagTouched(bool runStarting)
    {
        if (runStarting) StartRun();
        else EndRun();
    }

    private void StartRun()
    {
        NewBestText.SetActive(false);
        _replay.StartRun(_recordTarget, _captureEverNFrames, _maxRecordingTime);
        _isRecording = true;
        _elapsedTime = 0f;
        if (_timerText != null) _timerText.text = FormatTime(0f);

        if (_replay.GetRun(RecordingType.Best, out var best))
        {
            if (_currentGhost != null) Destroy(_currentGhost);

            _currentGhost = Instantiate(_ghostPrefab);
            _replay.PlayRecording(RecordingType.Best, _currentGhost, true);
        }
        else
        {
            Debug.Log("[GhostManager] No best recording to play this run.");
        }
    }

    private void EndRun()
    {
        Debug.Log("[DebugGhostManager] EndRun called. Finishing run.");

        var isNewBest = _replay.FinishRun(true);
        _replay.StopReplay();

        _isRecording = false;

        // persistence: save the new best to PlayerPrefs if it's best
        if (isNewBest)
        {
            NewBestText.SetActive(true);
            if (_replay.GetRun(RecordingType.Best, out var bestRun))
            {
                try
                {
                    var serialized = bestRun.Serialize();
                    PlayerPrefs.SetString(PREV_KEY_BEST, serialized);
                    PlayerPrefs.Save();
                    Debug.Log("[GhostManager] New best saved to PlayerPrefs.");
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to save best run: {e.Message}");
                }

                if (_bestTimeText != null) 
                    _bestTimeText.text = FormatTime(_elapsedTime);

            }
        }

        if (_currentGhost != null)
        {
            Destroy(_currentGhost);
            _currentGhost = null;
        }

        if (_timerText != null) _timerText.text = FormatTime(_elapsedTime);
        Debug.Log($"Run ended. Time: {_elapsedTime:F3}s. NewBest: {isNewBest}");
    }

    private string FormatTime(float t)
    {
        var minutes = Mathf.FloorToInt(t / 60f);
        var seconds = t % 60f;
        return minutes > 0 ? $"{minutes:0}:{seconds:00.000}" : $"{seconds:0.000}s";
    }

}
