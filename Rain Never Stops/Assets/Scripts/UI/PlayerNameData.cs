using UnityEngine;

public class PlayerNameData : MonoBehaviour
{
    public static PlayerNameData Instance;

    public string playerName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切场景不销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
