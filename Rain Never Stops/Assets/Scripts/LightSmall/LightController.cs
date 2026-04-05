using UnityEngine;
using System.Collections;


public class LightController : MonoBehaviour
{
    public GameObject lightline_small;
    public GameObject light_small;

    private Animator animator;

    private void Start()
    {
        animator = light_small.GetComponent<Animator>(); // 获取Animator
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("玩家来到光下");
            PlayerStateManagement.playerInLight = true;
            // 让 lightline_small 半透明
            SetSpriteAlpha(lightline_small, 0.3f);

            StartCoroutine(DestroyByRain());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStateManagement.playerInLight = false;
            // 恢复透明度
            SetSpriteAlpha(lightline_small, 1f);
            //Debug.Log("playerInLight" + PlayerStateManagement.playerInLight);
        }
    }

    IEnumerator DestroyByRain()
    {
        yield return new WaitForSeconds(4.5f);
        //播放动画
        animator.SetBool("isStartDissolve", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
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
