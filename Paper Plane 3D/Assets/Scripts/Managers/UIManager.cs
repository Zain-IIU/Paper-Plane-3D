using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        private void Awake()
        {
            Instance=this;
        }

        [SerializeField] private RectTransform mainPanel;
        [SerializeField] private Image powerMeter;
        [SerializeField] private RectTransform awesomeText;
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private CanvasGroup losePanel;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private RectTransform coinBar;
        [SerializeField] private int curCoins;
        [SerializeField] private float tweenTime;
        [SerializeField] private RectTransform scoreDiamond;
        [SerializeField] private RectTransform tweenDiamond;
        [SerializeField] private RectTransform heightBar;

        private void Start()
        {
            coinsText.text = curCoins.ToString();
            winPanel.gameObject.SetActive(false);
            losePanel.gameObject.SetActive(false);
            RandomizePowerMeter();
            EventsManager.ONGameStart += HideMainPanel;
            EventsManager.ONGameStart += CheckForPowerMeter;
            EventsManager.ONGameWin += EnableWinPanel;
            EventsManager.ONGameLose += EnableLosePanel;
            EventsManager.ONCoinPicked += CoinPickEffect;
            EventsManager.ONCoinPicked += IncreaseCoins;
        }

        #region Event CallBacks

        private void HideMainPanel()
        {
            mainPanel.DOScale(Vector2.zero, 0.25f);
            heightBar.DOScale(Vector2.one, .25f);
        }

        private void CheckForPowerMeter()
        {
            if (!(powerMeter.fillAmount >= .85f)) return;
            EventsManager.HeadStart();
            awesomeText.DOAnchorPos(new Vector2(0,-366), .25f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                StartCoroutine(nameof(ResendText));
            });

        }

        private void EnableWinPanel()
        {
           
            winPanel.gameObject.SetActive(true);
            DOTween.To(()=> winPanel.alpha, x=> winPanel.alpha = x, 1, 1f);
        }
        private void EnableLosePanel()
        {
           
            losePanel.gameObject.SetActive(true);
            DOTween.To(()=> losePanel.alpha, x=> losePanel.alpha = x, 1, .5f);
        }

        private void CoinPickEffect()
        {
         
            coinBar.DOScale(new Vector2(1.2f, 1.2f), tweenTime).SetLoops(0, LoopType.Yoyo);
            RectTransform diamondImage = Instantiate(scoreDiamond,tweenDiamond);
            diamondImage.DOMove(scoreDiamond.position, tweenTime).OnComplete(() =>
            {

                diamondImage.gameObject.SetActive(false);
            });
        }

        private void IncreaseCoins()
        {
            curCoins++;
            coinsText.text = curCoins.ToString();
        }
        #endregion

        private void RandomizePowerMeter()
        {
            DOTween.To(() => powerMeter.fillAmount, x => powerMeter.fillAmount = x, 1, .5f).SetLoops(-1, LoopType.Yoyo);
        }

        public float GETPowerValue() => powerMeter.fillAmount;


        IEnumerator ResendText()
        {
            yield return new WaitForSeconds(.5f);
            awesomeText.DOScale(Vector2.zero, .35f).SetEase(Ease.InBack);
        }

        private void OnDestroy()
        {
            EventsManager.ONGameStart -= HideMainPanel;
            EventsManager.ONGameStart -= CheckForPowerMeter;
            EventsManager.ONGameWin -= EnableWinPanel;
            EventsManager.ONGameLose -= EnableLosePanel;
            EventsManager.ONCoinPicked -= CoinPickEffect;
            EventsManager.ONCoinPicked -= IncreaseCoins;
        }

        [SerializeField] private TextMeshProUGUI multipliedText;
        private bool _hasMultiplied = false;
        public void MultiplyCoins(int multiplier)
        {
            if(_hasMultiplied) return;

            _hasMultiplied = true;
            print(multiplier);
            curCoins = curCoins * multiplier;
            coinBar.DOScale(Vector2.zero, .25f);
            multipliedText.text = curCoins.ToString();
            //TinySauce.OnGameFinished(curCoins);
        }
    }
}