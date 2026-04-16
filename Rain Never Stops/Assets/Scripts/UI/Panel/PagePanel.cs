using TMPro;
using UnityEngine;
using UnityEngine.Localization;
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
    public GameObject Page03;
    public GameObject PageVersion2;
    [SerializeField] private LocalizedAsset<TextAsset> casePage01;
    [SerializeField] private LocalizedAsset<TextAsset> casePage02;
    [SerializeField] private LocalizedAsset<TextAsset> casePage03;
    [SerializeField] private LocalizedAsset<TextAsset> caseVersion2;
    [Header("病例正文")]
    public TMP_Text casePageText;
    
    //public float warningDuration = 2f;  // 警告显示时间

    private void Start()
    {
        Page00.gameObject.SetActive(true);
        Page01.gameObject.SetActive(false);
        Page02.gameObject.SetActive(false);
        Page03.gameObject.SetActive(false);
        PageVersion2.gameObject.SetActive(false);
        casePageText.text = casePage01.LoadAsset().text;
        casePageText.gameObject.SetActive(false);

        nameText.gameObject.SetActive(false);

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
            Debug.Log("玩家名字已保存: " + name);
            // 赋值并显示nameText
            nameText.text = PlayerNameData.Instance.playerName;
            nameText.gameObject.SetActive(true);

            Page00.gameObject.SetActive(false);
            Page01.gameObject.SetActive(true);
            
            //显示第1页病例文字
            casePageText.gameObject.SetActive(true);            
        }
    }

   public void onToPage2Button()
    {
        Page01.gameObject.SetActive(false);
        Page02.gameObject.SetActive(true);
        casePageText.text = casePage02.LoadAsset().text;
        Debug.Log("第二页");
    }

    public void onToPage3Button()
    {
        Page02.gameObject.SetActive(false);
        Page03.gameObject.SetActive(true);
        casePageText.text = casePage03.LoadAsset().text;
        Debug.Log("第三页");
    }

    public void onCancelButton()
    {
        Page03.gameObject.SetActive(false);
        PageVersion2.gameObject.SetActive(true);
        casePageText.text = caseVersion2.LoadAsset().text;
        gameObject.SetActive(false);
    }
}
