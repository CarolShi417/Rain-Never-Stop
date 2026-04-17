using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaveOrStayPanel : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text lastWordFromDoctor;
    public Button leaveButton;
    public Button stayButton;

    [Header("对话内容")]
    [SerializeField] private TextAsset leaveTextFile;  // 5_doctor_paper_leave.txt
    [SerializeField] private TextAsset stayTextFile;   // 5_doctor_paper_stay.txt

    [Header("对话系统")]
    [SerializeField] private DialogueSystem dialogueSystem; // 拖入，用于隐藏最后一句
    void Start()
    {
        lastWordFromDoctor.gameObject.SetActive(false);
    }

    public void OnLeaveButton()
    {
        // 隐藏按钮
        leaveButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(false);

        // 隐藏dialogueSystem最后一句
        dialogueSystem.gameObject.SetActive(false);

        // 显示医生最后的话
        lastWordFromDoctor.gameObject.SetActive(true);
        if (leaveTextFile != null)
            lastWordFromDoctor.text = leaveTextFile.text;

        //再次点击鼠标，触发结局
        StartCoroutine(Wait());
        //if (Input.GetMouseButtonDown(0))
        //{
            
        //}
        Events.TriggerBadEnding3?.Invoke();
    }

    public void OnStayButton()
    {
        // 隐藏按钮
        leaveButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(false);

        // 隐藏dialogueSystem最后一句
        dialogueSystem.gameObject.SetActive(false);

        // 显示医生最后的话
        lastWordFromDoctor.gameObject.SetActive(true);
        if (stayTextFile != null)
            lastWordFromDoctor.text = stayTextFile.text;

        StartCoroutine(Wait());
        Events.TriggerHappyEnding1?.Invoke();
        //if (Input.GetMouseButtonDown(0))
        //{
            
        //}
            
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        // 2秒后执行的代码
    }
}
