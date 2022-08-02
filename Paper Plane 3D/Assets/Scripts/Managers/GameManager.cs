using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private  bool _hasGameStarted;
        [SerializeField] private GameObject grassShader;
        public void StartGame()
        {
            _hasGameStarted = true;
            EventsManager.GameStart();
        }

        public void EndGame()
        {
            EventsManager.GameWin();
        }

        public void SetShader()
        {
            grassShader.SetActive(!grassShader.activeInHierarchy);
        }
        

       
    }
}
