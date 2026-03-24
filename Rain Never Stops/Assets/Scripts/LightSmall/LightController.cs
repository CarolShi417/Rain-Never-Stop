using UnityEngine;
using System.Collections;


public class LightController : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("玩家来到光下");
            PlayerStateManagement.playerInLight = true;
            StartCoroutine(DestroyByRain());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStateManagement.playerInLight = false;
            //Debug.Log("playerInLight" + PlayerStateManagement.playerInLight);
        }
    }

    IEnumerator DestroyByRain()
    {
        yield return new WaitForSeconds(5f);
        //播放动画
        Destroy(gameObject);
    }
}
