using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BubblePanel : MonoBehaviour
{
    public TMP_Text text;
    public Image background;
    private Transform anchor;
   


    public void Setup(string content, Sprite bgSprite, Transform followAnchor)
    {

        text.text = content;
        background.sprite = bgSprite;
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