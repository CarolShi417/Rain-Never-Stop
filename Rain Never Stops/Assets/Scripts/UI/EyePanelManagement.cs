using UnityEngine;

public class EyePanelManagement : MonoBehaviour
{
    public static EyePanelManagement Instance;

    [SerializeField] private GameObject eyePanel;

    private Transform currentTarget;

    void Awake()
    {
        Instance = this;
        eyePanel.SetActive(false);
    }

    void Update()
    {
        if (currentTarget == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(currentTarget.position);
        screenPos.z = 0;
        eyePanel.transform.position = screenPos;
    }

    // 显示
    public void Show(Transform target)
    {
        currentTarget = target;
        eyePanel.SetActive(true);
    }

    // 隐藏
    public void Hide(Transform target)
    {
        // 只允许“当前控制者”关闭
        if (currentTarget == target)
        {
            currentTarget = null;
            eyePanel.SetActive(false);
        }
    }
}
