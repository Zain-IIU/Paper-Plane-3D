using UnityEngine;


    public class Multiplier : MonoBehaviour
    {
        [SerializeField] private int multiplyValue;


        public int GetMultiplierValue() => multiplyValue;
        public void SetMultiplier(int val) => multiplyValue = val;
    }
