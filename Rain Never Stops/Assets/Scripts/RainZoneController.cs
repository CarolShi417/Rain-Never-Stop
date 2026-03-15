using UnityEngine;

public class RainZoneController : MonoBehaviour
{
    [SerializeField] private float wetnessPerSecond = 1f;

    private bool playerInRain;

    private void Update()
    {
        if (!playerInRain)
        {
            return;
        }

        PlayerStateManager.AddWetness(wetnessPerSecond * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRain = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRain = false;
        }
    }
}
