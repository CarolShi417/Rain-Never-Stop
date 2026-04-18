using UnityEngine;
using UnityEngine.UI;

public class BubbleItemInteraction : NormalItemInteraction
{
    [Header("气泡UI")]
    [SerializeField] private BubblePanel bubblePanel;
    [SerializeField] private Transform bubblePanelAnchor;
    [SerializeField] private string contentText;
    [SerializeField] private Sprite panelSprite;

    [SerializeField] private Button cancelButton;

    protected override void Start()
    {
        base.Start();
        bubblePanel.gameObject.SetActive(false);
        cancelButton.onClick.AddListener(HideBubblePanel);

        SetBubbelText(contentText);
    }

    protected override void Update()
    {
        base.Update();

        
    }

    // 点击物品，打开bubblePanel
    protected override void OnItemClicked()
    {
        ShowBubblePanel();
    }
    protected override bool IsChildPanelOpen() => bubblePanel.gameObject.activeSelf;

    protected override void HideChildPanel()
    {
        HideBubblePanel();
    }

    void ShowBubblePanel()
    {
        if (!bubblePanel.gameObject.activeSelf)
        {
            bubblePanel.Setup(contentText, panelSprite, bubblePanelAnchor, this);
            bubblePanel.gameObject.SetActive(true);
            Debug.Log("BubblePanel已显示, activeSelf: " + bubblePanel.gameObject.activeSelf); // ✅


        }
    }

    public void HideBubblePanel()
    {
        bubblePanel.gameObject.SetActive(false);
        UnlockPlayerMovement();
    }

    //用于本地化
    public void SetBubbelText(string value)
    {
        contentText = value;
    }

}
