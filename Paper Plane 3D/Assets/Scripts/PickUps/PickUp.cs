using System;
using Managers;
using UnityEngine;


    public class PickUp : MonoBehaviour
    {
        [SerializeField] private PickUpType pickUpType;

        public void TakeAction()
        {
            switch (pickUpType)
            {
                case PickUpType.Fan:
                    EventsManager.CollisionWithFan();
                    break;
                case PickUpType.Hoop:
                    EventsManager.PassThroughHoop();
                    break;
                case PickUpType.WindSpin:
                    break;
                case PickUpType.Spring:
                    EventsManager.CollisionWithSpring();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
