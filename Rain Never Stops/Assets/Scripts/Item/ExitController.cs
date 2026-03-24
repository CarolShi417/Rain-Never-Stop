using UnityEngine;

public class ExitController : MonoBehaviour
{
    public enum ExitMode
    {
        RainExit,
        ShelterExit
    }

    public ExitMode mode;

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
    //        return;

    //    if (mode == ExitMode.RainExit)
    //    {
    //        HandleRainExit();
    //    }
    //    else if (mode == ExitMode.ShelterExit)
    //    {
    //        SceneFlowManager.GoToScene("Scene_rain");
    //    }
    //}

    //void HandleRainExit()
    //{
    //    string last = SceneFlowManager.LastScene;

    //    if (last == "Scene1_hospital")
    //    {
    //        SceneFlowManager.GoToScene("Scene2_shelter");
    //    }
    //    else if (last == "Scene_shelter")
    //    {
    //        SceneFlowManager.GoToScene("Scene_ending_be1");
    //    }
    //    //else if (last == "Scene_shelter2")
    //    //{
    //    //    SceneFlowManager.GoToScene("Scene_shelter3");
    //    //}
    //}
}
