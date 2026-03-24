using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PagePanel : MonoBehaviour
{
    public TMP_Text text;
    public Image background;



    public void Setup(string content, Sprite bgSprite)
    {

        text.text = content;
        background.sprite = bgSprite;
        //Debug.Log("bubble panel显示");
    }

   
}
