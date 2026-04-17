using System;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("相对于相机位置静止不动")]
    [SerializeField] private float offsetFromLeftEdge; // 距离左边缘的距离
    private Camera mainCamera;
    private float monsterX; // Monster当前X位置
    //[SerializeField] private float normalSpeed = 1f;

    void Start()
    {
        mainCamera = Camera.main;
        monsterX = transform.position.x; // 记录初始位置
    }

    void Update()
    {
        // 计算相机左边缘世界坐标
        float cameraLeftEdge = mainCamera.transform.position.x
                             - mainCamera.orthographicSize * mainCamera.aspect
                             + offsetFromLeftEdge;

        // 相机左边缘比Monster当前位置更靠右 → 玩家在向右走，Monster跟上
        if (cameraLeftEdge > monsterX)
        {
            monsterX = cameraLeftEdge;
        }
        // 否则玩家向左走，Monster不动

        transform.position = new Vector3(monsterX, transform.position.y, transform.position.z);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Events.OnPlayerEatedByMonster?.Invoke();
            Debug.Log("怪物把玩家吃掉了");
        }
    }
}
