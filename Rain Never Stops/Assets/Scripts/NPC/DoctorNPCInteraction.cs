using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoctorNPCInteraction : NormalItemInteraction
{
    [Header("对话系统")]
    [SerializeField] private DialogueSystem dialogueSystem;

    [Header("根据交互进度切换对话")]
    [SerializeField] private ItemInteractionManagement interManager;
    [SerializeField] private TextAsset[] dialogueByProgress;

    [Header("结局相关按钮")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button makeChoiceButton;
    public GameObject ChoicePanel;
    private bool waitingForClicktoEnding = false; // 是否在等待点击触发结局
    private System.Action pendingEndingAction;

    protected override void Start()
    {
        base.Start();
        ChoicePanel.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        makeChoiceButton.gameObject.SetActive(false);

        dialogueSystem.OnLastLineShown += OnLastLineShown; //监听最后一句
    }

    //处理最后点击，切换结局场景
    protected override void Update()
    {
        base.Update();

        // 等待点击触发结局
        if (waitingForClicktoEnding && Input.GetMouseButtonDown(0))
        {
            waitingForClicktoEnding = false;
            pendingEndingAction?.Invoke();
            pendingEndingAction = null;
        }
    }
    //触发对话的逻辑
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
            else // count >= 3，触发对话paper_2
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
        // 判断当前是否播放的是最后一段对话（idx = 2）
        bool isLastDialogue = (interManager.currentCount >= 2);
        // 判断场景里所有物品是否都已交互完毕
        bool allCompleted = interManager.hasInteractAllItems;
        Debug.Log("isLastDialogue = " + isLastDialogue);
        Debug.Log("allCompleted = " + allCompleted);

        if (allCompleted && isLastDialogue)
        {
            // 隐藏对话文字，显示按钮
            dialogueSystem.dialogueText.gameObject.SetActive(false);
            dialogueSystem.speakerText.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            makeChoiceButton.gameObject.SetActive(true);
        }
    }

    public void OnCloseButton()
    {
        dialogueSystem.gameObject.SetActive(false);

    }

    public void onMakeChoiceButton()
    {
        dialogueSystem.gameObject.SetActive(false);
        ChoicePanel.gameObject.SetActive(true);

    }

    public void ShowStayWordToBE3()
    {
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.Setup(dialogueByProgress[3]);

        pendingEndingAction = () => Events.TriggerBadEnding3?.Invoke();
        waitingForClicktoEnding = true;
        //StartCoroutine(WaitThenTrigger(() => Events.TriggerBadEnding3?.Invoke()));
    }

    public void ShowLeaveWordToHE()
    {
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.Setup(dialogueByProgress[4]);

        pendingEndingAction = () => Events.TriggerHappyEnding1?.Invoke();
        waitingForClicktoEnding = true;
        //StartCoroutine(WaitThenTrigger(() => Events.TriggerHappyEnding1?.Invoke()));
    }

    IEnumerator WaitThenTrigger(System.Action onComplete)
    {
        yield return new WaitForSeconds(3f);
        onComplete?.Invoke();
        // 2秒后执行的代码
    }
}