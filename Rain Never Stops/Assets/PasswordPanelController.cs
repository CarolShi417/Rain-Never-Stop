using UnityEngine;
using TMPro;
using System.Collections;

public class PasswordController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private GameObject uiPanel;

    private string correctPassword = "fear";
    private void Start()
    {
        errorText.gameObject.SetActive(false);
    }
    public void CheckPassword()
    {
        string playerInput = inputField.text.ToLower();

        if (playerInput == correctPassword)
        {
            Debug.Log("密码正确，通关！");
            uiPanel.SetActive(false);
            // 这里可以触发过关
        }
        else
        {
            Debug.Log("密码错误");

            errorText.gameObject.SetActive(true);

            StartCoroutine(HideErrorText());   // 启动协程

            inputField.text = "";   // 清空输入
            inputField.ActivateInputField(); // 重新聚焦
        }

        IEnumerator HideErrorText()
        {
            yield return new WaitForSeconds(2f);
            errorText.gameObject.SetActive(false);
        }
    }
}