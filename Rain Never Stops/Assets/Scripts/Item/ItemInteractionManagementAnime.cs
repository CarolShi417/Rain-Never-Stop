using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteractionManagementAnime : MonoBehaviour
{
    [Header("场景")]
    [SerializeField] private SceneManagement sceneManager;
    [SerializeField] private LightExitController lightExitController;
    [Header("可交互物体")]
    public int totalItems; // 总共多少个物体
    private int currentCount = 0;
    public bool hasInteractAllItems = false;//是否所有都已交互
    [Header("背景动画")]
    [SerializeField] private Animator bgAnimator;
    [SerializeField] private string[] animationTriggers = { "Crash1", "Crash2", "Crash3", "Crash4" };// 背景动画trigger
    void Start()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Interactable").Length;
    }
    public void RegisterInteraction()
    {
        if (currentCount >= totalItems) return; // 防止重复触发

        currentCount++;

        // 播放当前进度的背景动画
        if (currentCount <= animationTriggers.Length)
        {
            string triggerName = animationTriggers[currentCount - 1];
            bgAnimator.SetTrigger(triggerName);
            Debug.Log("播放第 " + currentCount + " 段动画: " + triggerName);
        }

        Debug.Log("当前交互物品进度: " + currentCount + "/" + totalItems);

        if (currentCount >= totalItems)
        {
            hasInteractAllItems = true;
            
            Debug.Log("全部完成！");
            lightExitController.ShowLargeLightLine();
        }

        
    }

}
