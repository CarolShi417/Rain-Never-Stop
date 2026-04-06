using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string nextScene;      // 正常跳转
    private void OnEnable()
    {
        Events.OnPlayerStateDead += GoToEnding1;
        Events.OnPlayerEatedByMonster += GoToEnding2;
    }

    private void OnDisable()
    {
        Events.OnPlayerStateDead -= GoToEnding1;
        Events.OnPlayerEatedByMonster -= GoToEnding2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 湿度达20持续2秒，出逃失败
    public void GoToEnding1()
    {
        SceneManager.LoadScene("Scene_ending_be1");
        
    }

    // 被Monster吃掉
    public void GoToEnding2()
    {
        SceneManager.LoadScene("Scene_ending_be2");
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene1_ward");
    }
}
