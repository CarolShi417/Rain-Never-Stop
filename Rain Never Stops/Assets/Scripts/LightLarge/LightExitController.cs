using System.Collections;
using UnityEngine;

public class LightExitController : MonoBehaviour
{
    [SerializeField] private SceneManagement sceneManager;
    [SerializeField] private ItemInteractionManagement itemInteractionManager;
    public GameObject bg;
    private Animator bg_animator;

    void Start()
    {
        bg.gameObject.SetActive(false);
        bg_animator = bg.GetComponent<Animator>(); // 获取Animator
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //停止player一切行为，隐藏一切多余的ui
            if(itemInteractionManager.hasInteractAllItems)
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
        //等待2s
        yield return new WaitForSeconds(2f);

        //播放变黑动画
        bg.gameObject.SetActive(true);
        bg_animator.SetBool("hasEnterLight2s", true);

        // 等待动画播放完
        yield return new WaitForSeconds(2f);

        sceneManager.GoToNextScene();

        
    }
}
