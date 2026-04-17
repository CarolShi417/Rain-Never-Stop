using UnityEngine;

public class NPCInteraction : NormalItemInteraction
{
    [Header("对话系统")]
    [SerializeField] private DialogueSystem dialogueSystem;
    public GameObject leaveOrStay;

    [Header("根据交互进度切换对话")]
    [SerializeField] private ItemInteractionManagement interManager;
    [SerializeField] private TextAsset[] dialogueByProgress;

    protected override void Start()
    {
        base.Start();
        leaveOrStay.SetActive(false);

        dialogueSystem.OnLastLineShown += OnLastLineShown; //监听最后一句
    }

    protected override void OnItemClicked()
    {
        if (!dialogueSystem.gameObject.activeSelf)
        {
            int count = interManager.currentCount;
            int idx;

            if (count == 0)
                idx = 0;
            else if (count >= 1 && count <= 2)
                idx = 1;
            else // count >= 3
                idx = 2;

            dialogueSystem.gameObject.SetActive(true);
            dialogueSystem.Setup(dialogueByProgress[idx]);
        }
    }

    protected override void OnPlayerExit()
    {
        dialogueSystem.gameObject.SetActive(false);
    }

    //最后一句出现，且完成所有交互时，出现选项leave or stay
    private void OnLastLineShown()
    {
        bool isLastDialogue = (interManager.currentCount >= dialogueByProgress.Length - 1);
        bool allCompleted = interManager.hasInteractAllItems;
        Debug.Log("isLastDialogue = " + isLastDialogue);
        Debug.Log("allCompleted = " + allCompleted);
        if (allCompleted && isLastDialogue)
        {
            leaveOrStay.SetActive(true);
        }
    }
}