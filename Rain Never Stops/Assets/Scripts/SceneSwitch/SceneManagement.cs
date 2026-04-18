using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string nextScene;      // 正常跳转
    public static int enterRainSceneCount = 0;//计算进入雨场景的次数

    private void OnEnable()
    {
        Events.OnPlayerStateDead += GoToEnding1;
        Events.OnPlayerEatedByMonster += GoToEnding2;
        Events.TriggerBadEnding3 += GoToEnding3;
        Events.TriggerHappyEnding1 += GoToHappyEnding1;
    }

    private void OnDisable()
    {
        Events.OnPlayerStateDead -= GoToEnding1;
        Events.OnPlayerEatedByMonster -= GoToEnding2;
        Events.TriggerBadEnding3 -= GoToEnding3;
        Events.TriggerHappyEnding1 += GoToHappyEnding1;
    }

    //void Start()
    //{
    //    string currentScene = SceneManager.GetActiveScene().name;
    //}

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

    public void GoToHappyEnding1()
    {
        SceneManager.LoadScene("Scene_ending_he1");
    }

    public void GoToEnding3()
    {
        SceneManager.LoadScene("Scene_ending_be3");
    }

    public void GoToNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Scene1_ward")
        {
            enterRainSceneCount = 0; // 回到病房重置
        }
        else if (currentScene == "Scene0_rain")
        {
            enterRainSceneCount++;
            //Debug.Log("进入雨场景次数为 = " + enterRainSceneCount);

            if (enterRainSceneCount == 1)
                SceneManager.LoadScene("Scene2_childroom");
            else if (enterRainSceneCount == 2)
                SceneManager.LoadScene("Scene5_clinic");
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
            
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene1_ward");
    }
}
