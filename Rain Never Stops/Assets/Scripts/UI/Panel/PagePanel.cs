using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PagePanel : MonoBehaviour
{
    [Header("第0页")]
    public TMP_Text text;
    public TMP_InputField nameInput;
    public GameObject Page00;
    public TMP_Text warningText;  // 提示玩家必须输入名字
    public Button confirmButton;
    [Header("第1页")]
    public TMP_Text nameText;
    public GameObject Page01;
    [Header("第2-最后页")]
    public GameObject Page02;
    public GameObject PageEnd;
    [SerializeField] private TextAsset casePage00;
    [SerializeField] private TextAsset casePage01;
    [SerializeField] private TextAsset casePage02;
    [Header("病例正文")]
    public TMP_Text casePageText;
    
    //public float warningDuration = 2f;  // 警告显示时间

    private void Start()
    {
        Page00.gameObject.SetActive(true);
        Page01.gameObject.SetActive(false);
        Page02.gameObject.SetActive(false);
        casePageText.text = casePage00.text;

        // 初始 按钮无效
        confirmButton.interactable = false;
        warningText.gameObject.SetActive(false);

        // 监听输入框变化，实时判断是否可以启用确认按钮
        nameInput.onValueChanged.AddListener(OnNameInputCompleted);
    }
    public void Setup(string content)
    {

        text.text = content;
        //Debug.Log("bubble panel显示");
    }

    private void OnNameInputCompleted(string inputText)
    {
        // string.IsNullOrWhiteSpace 检查文字是否为空或全是空格
        confirmButton.interactable = !string.IsNullOrWhiteSpace(inputText);

    }

    //按下确认按钮
    public void onConfirmButton()
    {
        // 检查玩家是否已输入名字，如果没有输入名字
        if (string.IsNullOrWhiteSpace(nameInput.text))
        {
            // 显示警告文字
            warningText.gameObject.SetActive(true);           
        }
        else
        {
            // 隐藏警告文字
            warningText.gameObject.SetActive(false);

            string name = nameInput.text;
            PlayerNameData.Instance.playerName = name;
            //Debug.Log("玩家名字已保存: " + name);

            Page00.gameObject.SetActive(false);
            Page01.gameObject.SetActive(true);
            nameText.text = PlayerNameData.Instance.playerName;
            casePageText.text = casePage01.text;
        }
    }

   public void onNextPageButton()
    {
        Page01.gameObject.SetActive(false);
        Page02.gameObject.SetActive(true);
        casePageText.text = casePage02.text;
    }
}
