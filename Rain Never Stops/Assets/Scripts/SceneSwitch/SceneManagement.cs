using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string nextScene;      // 正常跳转

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // =========================
        // 死亡优先（全局）
        // =========================
        if (PlayerStateManagement.currentState == PlayerState.Dead)
        {
            GoToEnding1();
            return;
        }
    }

    public void GoToEnding1()
    {
        SceneManager.LoadScene("Scene_ending_be1");
        
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
