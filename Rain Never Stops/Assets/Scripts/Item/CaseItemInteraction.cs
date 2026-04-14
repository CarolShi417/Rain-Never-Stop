using UnityEngine;

public class CaseItemInteraction : NormalItemInteraction
{
    [Header("Panel和字母")]
    [SerializeField] private CasePanel casePanel; // 
    [SerializeField] private string[] myWords;  // 填单词
    [SerializeField] private TextAsset myTextFile;  // 这个Item自己的txt

    protected override void Start()
    {
        base.Start();
        casePanel.gameObject.SetActive(false);
    }

    protected override void OnItemClicked()
    {
        if (!casePanel.gameObject.activeSelf)
        {
            casePanel.gameObject.SetActive(true); // 先激活，让Start执行
            casePanel.Setup(myWords, myTextFile); // 激活后再Setup

            CompleteInteraction(); //判定此物品已完成交互
        }
    }

    protected override void OnPlayerExit()
    {
        casePanel.gameObject.SetActive(false);
    }
}
