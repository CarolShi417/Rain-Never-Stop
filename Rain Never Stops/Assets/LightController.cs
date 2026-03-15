using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private float lightTimer;
    private bool playerInLight;

    private void Start()
    {
        lightTimer = 0f;
    }

    private void Update()
    {
        if (!playerInLight)
        {
            return;
        }

        lightTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInLight = false;
        }
    }
}
