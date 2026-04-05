using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _predatorCount;
        [SerializeField] private TextMeshProUGUI _workerCount;

        public void SetWorkerCount(int count)
        {
            _workerCount.text = string.Format(_workerCount.text, count.ToString());
        }

        public void SetPredatorCount(int count)
        {
            _predatorCount.text = string.Format(_predatorCount.text, count.ToString());
        }
    }
}