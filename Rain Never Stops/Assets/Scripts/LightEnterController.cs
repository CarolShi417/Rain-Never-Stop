using UnityEngine;
using System.Collections;

public class LightEnterController : MonoBehaviour
{
    [Header("灯光和光线")]
    public GameObject bg;
    private Animator bg_animator;
    void Start()
    {
        StartCoroutine(ShowandHideLight());
        bg_animator = bg.GetComponent<Animator>();
    }

    IEnumerator ShowandHideLight()
    {
        yield return new WaitForSeconds(2f);
        //播放动画
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        
    }

}
