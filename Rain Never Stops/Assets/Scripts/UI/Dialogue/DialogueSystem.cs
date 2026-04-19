using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text dialogueText;
    public TMP_Text speakerText;

    [Header("text文本文档")]
    public TextAsset textFile;

    public int index;
    List<string> textList = new List<string>();
    private bool isTyping = false; // 是否正在打字中
    [Header("回调函数")]
    public System.Action OnLastLineShown;// 判定是否为当前对话最后一句话，便于显示按钮
    [Header("LeaveOrStay联动")]
    [SerializeField] private DoctorNPCInteraction npcInteraction; // 拖入NPC对象
    public void Setup(TextAsset file)
    {
        //Debug.Log("Setup 被调用, file = " + file);
        GetTextFromFile(file); // 用传入的file而不是自身的textFile
        StopAllCoroutines();
        dialogueText.text = "";
        speakerText.text = "";
        StartCoroutine(SetTextUI());
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // LeaveOrStay 显示时，禁止点击关闭对话
            if (npcInteraction != null && npcInteraction.ChoicePanel.activeSelf) return;

            if (isTyping)
            {
                // 正在打字时点击 → 直接显示完整这句
                StopAllCoroutines();
                dialogueText.text = textList[index - 1]; // 显示当前句完整内容
                isTyping = false;
            }
            else if (index >= textList.Count)
            {
                // 对话结束 → 关闭
                gameObject.SetActive(false);
            }
            else
            {
                // 下一句
                StartCoroutine(SetTextUI());
            }
        }
    }
    //获取text文字
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    //逐字出现
    IEnumerator SetTextUI()
    {
        //清空上次一次文字
        dialogueText.text = "";

        //在这里处理speaker（要考虑到如果有2个speaker）
        if (textList[index].StartsWith("#"))
        {
            speakerText.text = textList[index].Substring(1); // 去掉#
            index++;
        }

        if (index >= textList.Count) yield break; // 已无更多内容，退出协程

        isTyping = true; // 标记为打字中，防止此时触发下一句
        string currentLine = textList[index]; 
        index++; 

        for (int i = 0; i < currentLine.Length; i++) // 逐字遍历当前行
        {
            dialogueText.text += currentLine[i]; 
            yield return new WaitForSeconds(0.05f); 
        }

        isTyping = false; // 打字完毕，允许点击进入下一句

        // 打字结束后，判断是否是最后一句
        if (index >= textList.Count)
        {
            OnLastLineShown?.Invoke();
        }
    }
}
