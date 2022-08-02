using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform lastWayPoint;
        [SerializeField] Image progressBar;
        [SerializeField] private TextMeshProUGUI startLevel;
        [SerializeField] private TextMeshProUGUI endLevel;
        float _fullDistance;
        [SerializeField] private string startLevelIndex;
        [SerializeField] private string endLevelIndex;
        private void Start()
        {
            _fullDistance = GetDistance();
            EventsManager.ONGameWin += HideBar;
            EventsManager.ONGameLose += HideBar;
            SetLevelText();
        }

        private void SetLevelText()
        {
            startLevel.text = startLevelIndex;
            endLevel.text = endLevelIndex;
        }

        private void HideBar()
        {
            GetComponent<RectTransform>().DOScale(Vector2.zero, .35f);
        }
        // Update is called once per frame
        void Update()
        {
            progressBar.fillAmount = Mathf.InverseLerp(_fullDistance, 0, GetDistance());
        }

        float GetDistance()
        {
            return Vector3.Distance(player.position, lastWayPoint.position);
        }

        private void OnDestroy()
        {
            EventsManager.ONGameWin -= HideBar;
            EventsManager.ONGameLose -= HideBar;
        }
    }
}