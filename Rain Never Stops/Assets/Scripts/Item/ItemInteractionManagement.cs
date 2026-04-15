using UnityEngine;

public class ItemInteractionManagement : MonoBehaviour
{
    [Header("可交互物体")]
    public int totalItems;
    public int currentCount = 0;
    public bool hasInteractAllItems = false;
    //[Header("广播")]
    // 完成一次交互，广播一次，用于背景动画变化
    public static event System.Action<int> OnProgressChanged;
    // 完成所有交互，广播一次，用于大灯亮，场景切换等
    public static event System.Action OnAllCompleted;

    void Start()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Interactable").Length;
    }

    public void RegisterInteraction()
    {
        if (currentCount >= totalItems) return;

        currentCount++;
        OnProgressChanged?.Invoke(currentCount);

        Debug.Log("当前交互物品进度: " + currentCount + "/" + totalItems);

        if (currentCount >= totalItems)
        {
            hasInteractAllItems = true;
            Debug.Log("全部完成！");
            OnAllCompleted?.Invoke();
            //lightExitController.ShowLargeLightLine();
        }
    }
}