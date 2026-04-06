using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    [Header("UI文字")]
    [SerializeField] private Transform bubblePanelAnchor; // UI出现位置
    [SerializeField] private string contentText;//文字内容
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowBubblePanel()
    {
        LightSmallPanel.Instance.Setup(bubblePanelAnchor);

    }
}
