using UnityEngine;

public class ExitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }

        PlayerStateManager.ReachShelter();
        SceneFlowManager.GoToNextScene();
    }
}
