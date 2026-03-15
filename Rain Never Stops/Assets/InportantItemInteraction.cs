using UnityEngine;

public class InportantItemInteraction : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject uiPanel;

    private bool playerInside = false;

    void Start()
    {
        highlight.SetActive(false);
    }

    void Update()
    {
        if (playerInside && Input.GetMouseButtonDown(0))
        {
            uiPanel.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInside = true;
            highlight.SetActive(true);
            Debug.Log("player进入关键道具区");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInside = false;
            highlight.SetActive(false);
        }
    }
}
