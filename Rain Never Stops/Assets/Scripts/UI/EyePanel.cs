using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EyePanel : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] private Transform anchor;
   


    public void Setup(string content, Transform followAnchor)
    {

        text.text = content;
        anchor = followAnchor;
        //Debug.Log("bubble panel显示");
    }

    void Update()
    {
        if (anchor != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(anchor.position);
            transform.position = screenPos;
        }
    }
}