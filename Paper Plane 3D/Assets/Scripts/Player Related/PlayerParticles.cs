using System;
using Managers;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem fastSpeedVFX;
    [SerializeField] private GameObject crashVFX;
    [SerializeField] private GameObject headStartVFX;
    [SerializeField] private ParticleSystem fanVFX;
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private ParticleSystem coinVFX;

   
    private void Start()
    {
        EventsManager.ONSpeedBoosted += EnableWindLine;
        EventsManager.ONCollisionWithFan+=EnableFanVFX;
        EventsManager.ONCollisionWithSpring+=EnableFanVFX;
        EventsManager.ONCollisionWithObstacle += EnableHitVFX;
        EventsManager.ONPlaneCrashed += EnableCrashVFX;
        EventsManager.ONPassThroughHoop += EnableWindLine;
        EventsManager.ONHeadStart += EnableHeadStartVFX;
        EventsManager.ONCoinPicked += EnableCoinPickEffect;
    }

    #region Event Call Backs

    private void EnableWindLine()
    {
        fastSpeedVFX.Play();
    }

    

    private void EnableCrashVFX()
    {
        crashVFX.SetActive(true);
    }

    private void EnableHeadStartVFX()
    {
        headStartVFX.SetActive(true);
    }

    private void EnableFanVFX() => fanVFX.Play();
    private void EnableHitVFX() => hitVFX.Play();

    private void EnableCoinPickEffect() => coinVFX.Play();
    #endregion

    private void OnDestroy()
    {
        EventsManager.ONSpeedBoosted -= EnableWindLine;
        EventsManager.ONCollisionWithFan-=EnableFanVFX;
        EventsManager.ONCollisionWithSpring-=EnableFanVFX;
        EventsManager.ONCollisionWithObstacle -= EnableHitVFX;
        EventsManager.ONPlaneCrashed -= EnableCrashVFX;
        EventsManager.ONPassThroughHoop -= EnableWindLine;
        EventsManager.ONHeadStart -= EnableHeadStartVFX;
        EventsManager.ONCoinPicked -= EnableCoinPickEffect;
    }
}
