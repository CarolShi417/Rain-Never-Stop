using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    [Header("UI文字")]
    [SerializeField] private Transform bubblePanelAnchor; // UI出现位置
    [SerializeField] private string contentText;//文字内容

    [SerializeField] private LightSmallPanel lightSmallPanel;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowBubblePanel()
    {
        //LightSmallPanel.Instance.Setup(bubblePanelAnchor);

        LightSmallPanel panel = lightSmallPanel != null ? lightSmallPanel : LightSmallPanel.Instance;
        if (panel == null)
        {
            panel = FindPanelInScene();
        }

        if (panel == null)
        {
            Debug.LogError("LightSmallPanel 未找到，无法显示提示面板。");
            return;
        }

        if (bubblePanelAnchor == null)
        {
            Debug.LogError("bubblePanelAnchor 未设置，无法显示提示面板。");
            return;
        }

        panel.Setup(bubblePanelAnchor);

    }

    private LightSmallPanel FindPanelInScene()
    {
        LightSmallPanel[] panels = Resources.FindObjectsOfTypeAll<LightSmallPanel>();
        foreach (LightSmallPanel panel in panels)
        {
            if (panel.gameObject.scene.IsValid())
            {
                return panel;
            }
        }

        return null;
    }
}
