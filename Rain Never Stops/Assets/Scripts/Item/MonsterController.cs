using System;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //[SerializeField] private float normalSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * normalSpeed * Time.deltaTime);
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
