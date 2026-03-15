using UnityEngine;

public class RainZoneController : MonoBehaviour
{
    public LightGroupGeneration light_group_generation;


    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float humidity = PlayerStateManagement.humidity;
    [SerializeField] private float lightTimer = 0.0f;
    [SerializeField] private float lightDurationStateToHalfDry = 5f;
    [SerializeField] private float humidityIncreasePerSecond = 1f;
    [SerializeField] private float humidityDecreasePerSecond = 1f;
    void Start()
    {
        //humidity = PlayerStateManagement.humidity;
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

            if (lightTimer >= lightDurationStateToHalfDry)
            {
                PlayerStateManagement.humidity = Mathf.Max(
                    0f,
                    PlayerStateManagement.humidity - humidityDecreasePerSecond * Time.deltaTime
                );
            }
            else
            {
                PlayerStateManagement.humidity += humidityIncreasePerSecond * Time.deltaTime;
            }

        }
        //如果玩家在雨里
        else
        {
            lightTimer = 0f;
            PlayerStateManagement.humidity += humidityIncreasePerSecond * Time.deltaTime;
        }

        PlayerStateManagement.ChangeState();
        PlayerStateManagement.ChangeBehaviorState();
    }

}
