using UnityEngine;
using UnityEngine.UI;

public class HeightBar : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundPoint;
   private float _distance;
   [SerializeField] private Slider heightSlider;
    private void Start()
    {
        _distance = GetDistance() *2;
       
    }

    // Update is called once per frame
    void Update()
    {
        heightSlider.value = Mathf.InverseLerp(0, _distance, GetDistance());
    }

    float GetDistance()
    {
        return Vector3.Distance(player.position, groundPoint.position);
    }
}
