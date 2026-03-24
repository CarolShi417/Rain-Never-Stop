using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text dialogueText;
    public TMP_Text Speaker;

    [Header("Speaker")]

    [Header("text文本文档")]
    public TextAsset textFile;
    public int index;
    List<string> textList = new List<string>();

    void Start()
    {
        GetTextFromFile(textFile);
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SetTextUI());
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
            Speaker.text = textList[index].Substring(1); // 去掉#
            index++;
        }

        for (int i = 0; i < textList[index].Length; i++)
        {
            dialogueText.text += textList[index][i];

            yield return new WaitForSeconds(0.1f );
        }

        index++;
    }
}
