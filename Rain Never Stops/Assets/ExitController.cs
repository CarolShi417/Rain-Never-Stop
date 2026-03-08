using UnityEngine;

public class ExitController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("玩家来到出口");
            SceneFlowManager.GoToNextScene();
        }
    }
}
