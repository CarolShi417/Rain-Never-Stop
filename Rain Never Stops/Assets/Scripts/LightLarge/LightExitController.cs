using System.Collections;
using UnityEngine;

public class LightExitController : MonoBehaviour
{
    [Header("触发前置条件")]
    [SerializeField] private bool requireAllItems = true;
    [Header("申明其他代码")]
    [SerializeField] private SceneManagement sceneManager;
    [SerializeField] private ItemInteractionManagement itemInteractionManager;
    public LightInteraction lightInteraction;

    [Header("灯光和光线")]
    public GameObject bg; //环境光线亮-->黑
    public GameObject lightline_large;
    //动画
    private Animator bg_animator;

    

    void Start()
    {
        bg.gameObject.SetActive(false);

        lightline_large.gameObject.SetActive(!requireAllItems);

        bg_animator = bg.GetComponent<Animator>(); // 获取Animator
        SetSpriteAlpha(lightline_large, 1f);//初始光线透明度为1
    }

    private void OnEnable()
    {
        ItemInteractionManagement.OnAllCompleted += ShowLargeLightLine;
    }

    private void OnDisable()
    {
        ItemInteractionManagement.OnAllCompleted -= ShowLargeLightLine;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SetSpriteAlpha(lightline_large, 0.3f);
            //停止player一切行为，隐藏一切多余的ui
            if (!requireAllItems)
            {
                
                StartCoroutine(GoToNextScene());
                return;
            }

            if (itemInteractionManager != null && itemInteractionManager.hasInteractAllItems)
            {
                
                StartCoroutine(GoToNextScene());
            }
            else
            {
                Debug.Log("尚有未完成的交互");
            }
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

    public void ShowLargeLightLine()
    {
        lightline_large.gameObject.SetActive(true);
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
