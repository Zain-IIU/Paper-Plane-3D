using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] levels;

    [SerializeField] private bool startDisabled;
    private static int CurrentLevel
    {
        get => PlayerPrefs.GetInt("CurrentLevel", 0);
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (!startDisabled) return;
        foreach (var level in levels)  level.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        LoadGame();   
    }
    

    private void LoadGame()
    {
        CurrentLevel = GetLevelIndex();
        print(CurrentLevel);
        levels[CurrentLevel].SetActive(true);
        //TinySauce.OnGameStarted($"CurrentLevel");
    }

    int GetLevelIndex()
    {
        return CurrentLevel % levels.Length;
    }
    
    [ContextMenu("Load Next Level")]
    public void IncrementLevelIndex()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
        Debug.Log("Loading Next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    [ContextMenu("Restart Level")]
    public void ReplayLevel()
    {
        Debug.Log("Loading same level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}