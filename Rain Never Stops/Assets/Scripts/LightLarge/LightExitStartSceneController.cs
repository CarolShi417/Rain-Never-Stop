using System.Collections;
using UnityEngine;

public class LightExitStartSceneController : MonoBehaviour
{
    [Header("申明其他代码")]
    [SerializeField] private SceneManagement sceneManager;
    public LightInteraction lightInteraction;

    [Header("灯光和光线")]
    public GameObject bg; //环境光线亮-->黑
    public GameObject lightline_large;
    //动画
    private Animator bg_animator;

    

    void Start()
    {
        bg.gameObject.SetActive(false);

        bg_animator = bg.GetComponent<Animator>(); // 获取Animator
        SetSpriteAlpha(lightline_large, 1f);//初始光线透明度为1
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SetSpriteAlpha(lightline_large, 0.3f);
            StartCoroutine(GoToNextScene());            
        }
    }


    IEnumerator GoToNextScene()
    {
        if (lightInteraction != null)
        {
            lightInteraction.ShowBubblePanel();
        }
        else
        {
            Debug.LogWarning("LightInteraction 未绑定，跳过提示面板显示。");
        }

        //等待2s
        yield return new WaitForSeconds(2f);

        //播放变黑动画
        bg.gameObject.SetActive(true);
        bg_animator.SetBool("hasEnterLight2s", true);

        // 等待动画播放完
        yield return new WaitForSeconds(2f);

        sceneManager.GoToNextScene();        
    }
        

    //设置物体spriterenderer透明度
    private void SetSpriteAlpha(GameObject obj, float alpha)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = alpha;   // alpha 范围 0~1
            sr.color = c;
        }
    }
}
