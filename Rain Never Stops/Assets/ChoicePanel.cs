using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    [Header("UI组件")]
    public Button leaveButton;
    public Button stayButton;

    [Header("对话系统")]
    [SerializeField] private DialogueSystem dialogueSystem; // 拖入，用于隐藏最后一句
    [SerializeField] private DoctorNPCInteraction docNPCInteraction;
    void Start()
    {
        
    }

    public void OnLeaveButton()
    {
        // 隐藏panel
        gameObject.SetActive(false);

        // 显示医生最后的话
        dialogueSystem.gameObject.SetActive(true);
        docNPCInteraction.ShowLeaveWordToHE();
        
    }

    public void OnStayButton()
    {
        // 隐藏panel
        gameObject.SetActive(false);

        // 显示医生最后的话
        dialogueSystem.gameObject.SetActive(true);
        docNPCInteraction.ShowStayWordToBE3();
        
        

    }

}
