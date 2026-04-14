using UnityEngine;

public class ItemInteractionManagement : MonoBehaviour
{
    [Header("场景")]
    //[SerializeField] private SceneManagement sceneManager;
    //[SerializeField] private LightExitController lightExitController;

    [Header("可交互物体")]
    public int totalItems;
    public int currentCount = 0;
    public bool hasInteractAllItems = false;

    // 进度变化时广播
    public static event System.Action<int> OnProgressChanged;

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
            //lightExitController.ShowLargeLightLine();
        }
    }
}