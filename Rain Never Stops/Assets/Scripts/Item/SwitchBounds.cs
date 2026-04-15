using Cinemachine;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    private void Start()
    {
        SwitchConfinerShape();
    }

    private void SwitchConfinerShape()
    {
        // 寻找bounds，获取其身上的组件
        GameObject boundsObj = GameObject.FindGameObjectWithTag("Bounds");
        if (boundsObj == null)
        {
            Debug.LogError("未找到带有 'Bounds' 标签的游戏对象！");
            return;
        }

        PolygonCollider2D confinerShape = boundsObj.GetComponent<PolygonCollider2D>();
        if (confinerShape == null)
        {
            Debug.LogError("Bounds 对象上没有 PolygonCollider2D 组件！");
            return;
        }

        // 使用 CinemachineConfiner2D（注意是 2D 版本）
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        if (confiner == null)
        {
            Debug.LogError("当前对象上没有 CinemachineConfiner2D 组件！");
            return;
        }

        // 赋值
        confiner.m_BoundingShape2D = confinerShape;

        // 清除缓存（2D 版本的方法名略有不同）
        confiner.InvalidateCache();
    }
}


