using UnityEngine;

public class CaseItemInteraction : NormalItemInteraction
{
    [Header("全屏UI")]
    [SerializeField] private GameObject casePanel; // 

    protected override void Start()
    {
        base.Start();
        casePanel.SetActive(false);
    }

    protected override void OnItemClicked()
    {
        if (!casePanel.activeSelf)
        {
            casePanel.SetActive(true);
            // 如果全屏Panel有Setup方法，在这里调用
            // fullscreenPanel.GetComponent<XxxPanel>().Setup(...);

        }
    }

    protected override void OnPlayerExit()
    {
        casePanel.SetActive(false);
    }
}
