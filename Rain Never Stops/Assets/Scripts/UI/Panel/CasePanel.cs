using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CasePanel : MonoBehaviour
{
    [Header("第1页")]
    public GameObject Page01;
    public Button NextPageButton;
    //public TMP_Text warningText;  // 提示玩家必须正确解谜  
    [Header("字母")]
    [SerializeField] private TMP_Text[] wordTexts; // 拖入F、A、E、R下面的Text(TMP)

    [Header("拖拽词")]
    [SerializeField] private DragDropWord[] dragDropWords; // 拖入F、A、E、R prefab,用于确认位置
    public bool allCorrect = false;  // 全部正确

    [Header("第2页")]
    public GameObject Page02;
    public TMP_Text nameText;
    public TMP_Text caseText;
    public Button CloseButton;

    private void Awake()
    {
        Page01.gameObject.SetActive(true);
        Page02.gameObject.SetActive(false);

        // 初始 按钮无效 warning隐藏
        NextPageButton.interactable = false;
        //warningText.gameObject.SetActive(false);

        //gameObject.SetActive(false); //运行时隐藏自己

    }

    public void Setup(string[] words, TextAsset textFile)
    {
        // 注入字母
        // 假设你有4个字母Text，用数组管理
        for (int i = 0; i < wordTexts.Length; i++)
        {
            if (i < words.Length)
                wordTexts[i].text = words[i];
        }

        // 注入内容
        if (textFile != null)
            caseText.text = textFile.text;

        // 重置拼图状态
        ResetPuzzle();
    }

    public void onNextPageButton()
    {
        Page01.gameObject.SetActive(false);
        Page02.gameObject.SetActive(true);

    }

    public void onCloseButton()
    {
        gameObject.SetActive(false);
    }

    // 由 DragDropWord 调用，4个都正确就可按下一页
    public void CheckAllCorrect()
    {
        //Debug.Log("CheckAllCorrect 被调用，dragDropWords 数量: " + dragDropWords.Length);

        int correctCount = 0;

        foreach (var word in dragDropWords)
        {
            if (word.isCorrect)
                correctCount++;
            //Debug.Log("correctCount = " + correctCount);
        }

        allCorrect = (correctCount == dragDropWords.Length);

        if (allCorrect)
        {
            NextPageButton.interactable = true;
            //warningText.gameObject.SetActive(false);
            //Debug.Log("全部正确！");
        }
    }

    public void ResetPuzzle()
    {
        // 重置所有DragDropWord
        foreach (var word in dragDropWords)
        {
            word.ResetWord();
        }

        allCorrect = false;
        NextPageButton.interactable = false;
        Page01.SetActive(true);
        Page02.SetActive(false);
    }
}