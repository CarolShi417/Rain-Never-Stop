using UnityEngine;

public class BubbleItemInteraction : NormalItemInteraction
{
    [Header("气泡UI")]
    [SerializeField] private BubblePanel bubblePanel;
    [SerializeField] private Transform bubblePanelAnchor;
    [SerializeField] private string contentText;
    [SerializeField] private Sprite panelSprite;


    protected override void Start()
    {
        base.Start();
        bubblePanel.gameObject.SetActive(false);
    }

    protected override void OnItemClicked()
    {
        if (!bubblePanel.gameObject.activeSelf)
        {
            bubblePanel.Setup(contentText, panelSprite, bubblePanelAnchor, this);
            bubblePanel.gameObject.SetActive(true);

            CompleteInteraction();
        }
    }

    protected override void OnPlayerExit()
    {
        bubblePanel.gameObject.SetActive(false);
    }

}
