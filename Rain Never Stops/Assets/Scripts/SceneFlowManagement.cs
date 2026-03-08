using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneFlowManager
{
    private static int step = 0;

    private static string[] sceneFlow =
    {
        //"Scene1_hospital",
        "Scene_rain",
        "Scene2_shelter",
        "Scene_rain",
    };

    public static void GoToNextScene()
    {
        if (step >= sceneFlow.Length)
            return;

        SceneManager.LoadScene(sceneFlow[step]);
        step++;

        Debug.Log("step: " + step);
    }
}
