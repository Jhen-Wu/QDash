using UnityEngine;   

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float destroyX = -10f; // 超過此 X 座標即刪除

    private void Update()
    {
        // 若障礙物移出畫面左側
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject); // 刪除自身
        }
    }
}





