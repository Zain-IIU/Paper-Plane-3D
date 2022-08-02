using DG.Tweening;
using Managers;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] private Vector3 startOffset;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private Vector3 loseOffset;
    [SerializeField] private Vector3 winOffset;

    public float smoothSpeed = 0.1f;
    private Vector3 _curOffset;

    private void Awake()
    {
        _curOffset = startOffset;
    }

    private void Start()
    {
        EventsManager.ONGameStart += DisableStartCamera;
        EventsManager.ONGameWin += EnableEndCamera;
        EventsManager.ONGameLose += EnableLoseCam;
        EventsManager.ONReachedEnd += EnableEndCamera;
    }

    private void LateUpdate()
    {
        SmoothFollow();   
    }

    private void SmoothFollow()
    {
        Vector3 targetPos = target.position + _curOffset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
        transform.LookAt(target);
    }

    #region Event Callbacks

    private void DisableStartCamera()
    {
        DOTween.To(()=> _curOffset, x=> _curOffset = x, followOffset, .25f);
    }
    private void EnableEndCamera()
    {
        DOTween.To(()=> _curOffset, x=> _curOffset = x, winOffset, .25f);
    }
    private void EnableLoseCam()
    {
        DOTween.To(()=> _curOffset, x=> _curOffset = x, loseOffset, .25f);
    }


    #endregion
   
    private void OnDestroy()
    {
        EventsManager.ONGameStart -= DisableStartCamera;
        EventsManager.ONGameWin -= EnableEndCamera;
        EventsManager.ONGameLose -= EnableLoseCam;
        EventsManager.ONReachedEnd -= EnableEndCamera;
    }
}
