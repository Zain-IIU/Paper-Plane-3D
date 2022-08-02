using Cinemachine;
using Managers;
using UnityEngine;


    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameObject followCam;
        [SerializeField] private GameObject startCam;
        [SerializeField] private GameObject winCam;
        [SerializeField] private GameObject loseCam;

        private void Start()
        {
            
            EventsManager.ONGameStart += DisableStartCamera;
            EventsManager.ONGameWin += EnableEndCamera;
            EventsManager.ONGameLose += EnableLoseCam;
        }


        #region Event Call Backs

        private void DisableStartCamera()
        {
            followCam.SetActive(true);
            startCam.SetActive(false);
        }

        private void EnableEndCamera()
        {
            followCam.SetActive(false);
            winCam.SetActive(true);
        }

        private void EnableLoseCam()
        {
            followCam.SetActive(false);
            loseCam.SetActive(true);
        }
        
        #endregion

        private void OnDestroy()
        {
            EventsManager.ONGameStart -= DisableStartCamera;
            EventsManager.ONGameWin -= EnableEndCamera;
            EventsManager.ONGameLose -= EnableLoseCam;
        }
    }
