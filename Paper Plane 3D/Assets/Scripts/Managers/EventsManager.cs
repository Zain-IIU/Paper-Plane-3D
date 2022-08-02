using System;
using UnityEngine;

namespace Managers
{
    public class EventsManager : MonoBehaviour
    {
        public static event Action ONGameStart;
        public static event Action ONSpeedBoosted;
        public static event Action ONPassThroughHoop;
        public static event Action ONCollisionWithObstacle;
        public static event Action ONCollisionWithFan;
        public static event Action ONCollisionWithSpring;
        public static event Action ONGameWin;
        public static event Action ONGameLose;
        public static event Action ONPlaneCrashed;
        public static event Action ONHeadStart;
        public static event Action ONCoinPicked;

        public static event Action ONReachedEnd;
        public static void GameStart()
        {
            ONGameStart?.Invoke();
        }


        public static void GameWin()
        {
            ONGameWin?.Invoke();
        }

        public static void GameLose()
        {
            ONGameLose?.Invoke();
        }

        public static void SpeedBoosted()
        {
            ONSpeedBoosted?.Invoke();
        }

        public static void CollisionWithObstacle()
        {
            ONCollisionWithObstacle?.Invoke();
        }

        public static void CollisionWithFan()
        {
            ONCollisionWithFan?.Invoke();
        }

        public static void PassThroughHoop()
        {
            ONPassThroughHoop?.Invoke();
        }

        public static void CollisionWithSpring()
        {
            ONCollisionWithSpring?.Invoke();
        }

        public static void PlaneCrashed()
        {
            ONPlaneCrashed?.Invoke();
        }

        public static void HeadStart()
        {
            ONHeadStart?.Invoke();
        }

        public static void CoinPicked()
        {
            ONCoinPicked?.Invoke();
        }

        public static void ReachedEnd()
        {
            ONReachedEnd?.Invoke();
        }
    }
}