using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private float lightTimer;
    [SerializeField] private float lightDuration_stateToHalfDry = 5f;
    private bool playerInLight = false;

    private void Start()
    {
        lightTimer = 0f;
    }

    private void Update()
    {
        if(playerInLight)
        {
            lightTimer += Time.deltaTime;
        }
        if(lightTimer >= lightDuration_stateToHalfDry && PlayerStateManager.currentState == PlayerState.HalfWet)
        {
            PlayerStateManager.ChangeState(PlayerState.Normal);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("玩家来到光下");
            playerInLight = true;
            
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInLight = false;
        }
    }
}
