using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterZoneManagement : MonoBehaviour
{
    [SerializeField] private float humidity;
    [SerializeField] private float humidityDecreasePerSecond = 1f;

    void Start()
    {
        humidity = PlayerStateManagement.humidity;
        Debug.Log("进入新场景，humidity为"+ humidity);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStateManagement.humidity = Mathf.Max(
            0f,
            PlayerStateManagement.humidity - humidityDecreasePerSecond * Time.deltaTime
        );

        humidity = PlayerStateManagement.humidity; // 同步显示
        PlayerStateManagement.ChangeBehaviorState();
    }
}
