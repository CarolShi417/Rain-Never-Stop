using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhoneButtonControl : MonoBehaviour
{
    [Header("电话图标摇摆")]
    public Button PhoneButton;
    private float swingAngle = 15f;  // 摇摆角度
    private float swingSpeed = 8f;   // 摇摆速度
    [Header("电话铃声")]
    public AudioSource phoneAudioSource;//电话铃声
    [Header("对话系统")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private TextAsset[] dialogueByProgress;

    void Start()
    {
        PhoneButton.gameObject.SetActive(false);
        StartCoroutine(WaitForShow());
    }

    void Update()
    {
        if(PhoneButton.gameObject.activeSelf)
        {
            // 用Sin函数产生来回摇摆的效果
            float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
            PhoneButton.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }

    public void OnClickPhoneButton()
    {
        PhoneButton.gameObject.SetActive(false);
        phoneAudioSource.Stop();
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.Setup(dialogueByProgress[0]);
    }

    IEnumerator WaitForShow()
    {
        yield return new WaitForSeconds(5f);
        PhoneButton.gameObject.SetActive(true);
        phoneAudioSource.loop = true;
        phoneAudioSource.Play();
    }
}
