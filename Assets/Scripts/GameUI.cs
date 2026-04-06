using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _predatorCount;
    [SerializeField] private TextMeshProUGUI _workerCount;

    private string _predatorFormat;
    private string _workerFormat;

    private void Awake()
    {
        _predatorFormat = _predatorCount.text;
        _workerFormat = _workerCount.text;
    }

    public void SetWorkerCount(int count)
    {
        _workerCount.text = string.Format(_workerFormat, count);
    }

    public void SetPredatorCount(int count)
    {
        _predatorCount.text = string.Format(_predatorFormat, count);
    }
}