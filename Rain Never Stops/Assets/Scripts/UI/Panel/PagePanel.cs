using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PagePanel : MonoBehaviour
{
    [Header("第0页")]
    public TMP_Text text;
    public TMP_InputField nameInput;
    public GameObject Page00;
    [Header("第1页")]
    public TMP_Text nameText;
    public GameObject Page01;

    public GameObject Page02;
    public GameObject PageEnd;
    [SerializeField] private TextAsset casePage00;
    [SerializeField] private TextAsset casePage01;
    [SerializeField] private TextAsset casePage02;
    public TMP_Text casePageText; 

    private void Start()
    {
        Page00.gameObject.SetActive(true);
        Page01.gameObject.SetActive(false);
        Page02.gameObject.SetActive(false);
        casePageText.text = casePage00.text;
    }
    public void Setup(string content)
    {

        text.text = content;
        //Debug.Log("bubble panel显示");
    }

   public void onConfirmButton()
    {
        string name = nameInput.text;
        PlayerNameData.Instance.playerName = name;
        Debug.Log("玩家名字已保存: " + name);

        Page00.gameObject.SetActive(false);
        Page01.gameObject.SetActive(true);
        nameText.text = PlayerNameData.Instance.playerName;
        casePageText.text = casePage01.text;
    }

   public void onNextPageButton()
    {
        Page01.gameObject.SetActive(false);
        Page02.gameObject.SetActive(true);
        casePageText.text = casePage02.text;
    }
}
