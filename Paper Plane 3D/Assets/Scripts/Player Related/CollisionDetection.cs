using System.Collections;
using DG.Tweening;
using Managers;

using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private string obstacleTag;
    
    IEnumerator WallLose()
    {
        yield return new WaitForSeconds(1f);
        EventsManager.PlaneCrashed();
        EventsManager.GameLose();
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(nameof(WallLose));
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*20f,ForceMode.Impulse);
        }
        if (other.gameObject.CompareTag(obstacleTag))
        {
            other.enabled = false;
            EventsManager.CollisionWithObstacle();
        }
        if (other.gameObject.CompareTag("Ground") )
        {
            EventsManager.PlaneCrashed();
            EventsManager.GameLose();
        }
        if (other.TryGetComponent(out PickUp pickUp))
        {
            pickUp.TakeAction();
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            other.enabled = false;
            other.transform.parent=transform;
            other.transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack);
            other.transform.DOScale(Vector3.zero, .25f);
            EventsManager.CoinPicked();
        }

        if (other.gameObject.TryGetComponent(out Multiplier multiplier))
        {
            other.enabled = false;
            UIManager.Instance.MultiplyCoins(multiplier.GetMultiplierValue());
            EventsManager.GameWin();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            EventsManager.ReachedEnd();
        }
    }

}

