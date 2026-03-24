using UnityEngine;

public class InportantItemInteraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer highlightSprRen;
    [SerializeField] private GameObject uiPanel;

    private bool playerInside = false;
        

    void Start()
    {        
        uiPanel.SetActive(false);
        spriteRenderer.enabled = true;
        highlightSprRen.enabled = false;
    }

    void Update()
    {
        //判断鼠标是否在物体上
        if (playerInside && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null &&
                hit.collider.transform.parent == transform &&
                hit.collider.gameObject.layer == LayerMask.NameToLayer("ClickCollider"))
            {
                uiPanel.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInside = true;
            spriteRenderer.enabled = false;
            highlightSprRen.enabled = true;
            //Debug.Log("player进入关键道具区");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInside = false;
            spriteRenderer.enabled = true;
            highlightSprRen.enabled = false;
        }
    }
    //
}
