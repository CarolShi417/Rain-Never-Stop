using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * normalSpeed * Time.deltaTime);
    }
}
