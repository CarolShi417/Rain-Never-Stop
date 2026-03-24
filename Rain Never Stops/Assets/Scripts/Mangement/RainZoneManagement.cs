using UnityEngine;
using UnityEngine.SceneManagement;


public class RainZoneManagement : MonoBehaviour
{
    
    [Header("小灯")]
    public LightGroupGeneration light_group_generation;
    [Header("大灯")]
    public GameObject lightLarge;
    [Header("是否在灯下影响湿度")]
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float humidity = 0.0f;
    [SerializeField] private float lightTimer = 0.0f;
    [SerializeField] private float humidityIncreasePerSecond = 1f;
    [SerializeField] private float humidityDecreasePerSecond = 1f;
    [Header("进入雨场景次数")]
    public static int enterRainSceneTime = 0;

    void Start()
    {
        enterRainSceneTime++;
        Debug.Log("进入雨场景次数: " + enterRainSceneTime);
        humidity = PlayerStateManagement.humidity;
        light_group_generation.SpawnRandomLights(3);
        
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //如果玩家在灯下
        if (PlayerStateManagement.playerInLight)
        {
            
            lightTimer += Time.deltaTime;
            PlayerStateManagement.humidity = Mathf.Max(
                0f,
                PlayerStateManagement.humidity - humidityDecreasePerSecond * Time.deltaTime
            );

        }
        //如果玩家在雨里
        else
        {
            lightTimer = 0f;
            PlayerStateManagement.humidity += humidityIncreasePerSecond * Time.deltaTime;
        }
        humidity = PlayerStateManagement.humidity;
        PlayerStateManagement.ChangeState();
        PlayerStateManagement.ChangeBehaviorState();
    }

    
}
