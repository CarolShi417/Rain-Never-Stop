using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteractionManagement : MonoBehaviour
{
    [SerializeField] private SceneManagement sceneManager;
    
    public int totalItems; // 总共多少个物体
    private int currentCount = 0;

    [SerializeField] private Animator bgAnimator;
    void Start()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Interactable").Length;
    }
    public void RegisterInteraction()
    {
        currentCount++;

        Debug.Log("当前交互物品进度: " + currentCount);

        if (currentCount >= totalItems)
        {
            Debug.Log("全部完成！");
            StartCoroutine(GoToNextScene());
        }

        
    }

    IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(5f);

        bgAnimator.SetBool("InteractAllDone", true);

        yield return new WaitForSeconds(1f);

        //SceneManager.LoadScene("Scene_rain");
        sceneManager.GoToNextScene();
    }
}
