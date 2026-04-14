using UnityEngine;

public class NPCInteraction : NormalItemInteraction
{
    [Header("对话系统")]
    [SerializeField] private DialogueSystem dialogueSystem;

    [Header("根据交互进度切换对话")]
    [SerializeField] private ItemInteractionManagement interManager;
    [SerializeField] private TextAsset[] dialogueByProgress;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnItemClicked()
    {
        if (!dialogueSystem.gameObject.activeSelf)
        {
            int idx = Mathf.Clamp(interManager.currentCount, 0, dialogueByProgress.Length - 1);
            dialogueSystem.gameObject.SetActive(true);
            dialogueSystem.Setup(dialogueByProgress[idx]);
        }
    }

    protected override void OnPlayerExit()
    {
        dialogueSystem.gameObject.SetActive(false);
    }
}