using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EyePanel : MonoBehaviour
{
    public TMP_Text text;
    private Transform anchor;
   


    public void Setup(string content, Transform followAnchor)
    {

        text.text = content;
        anchor = followAnchor;
        //Debug.Log("bubble panel显示");
    }

    void Update()
    {
        // 任意一个为空就跳过，不报错
        if (anchor == null || Camera.main == null) return;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(anchor.position);
        transform.position = screenPos;
    }
}