using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterZoneManagement : MonoBehaviour
{
    [SerializeField] private float humidity = 0.0f;

    void Start()
    {
        humidity = PlayerStateManagement.humidity;
    }

    // Update is called once per frame
    void Update()
    {
        humidity = PlayerStateManagement.humidity;
    }
}
