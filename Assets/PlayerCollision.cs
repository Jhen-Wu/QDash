using UnityEngine;   

public class PlayerCollision : MonoBehaviour 
{
    private void Start() 
    {
        // 訂閱「開始遊戲」事件
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
    }

    private void ActivatePlayer() 
    {
        // 遊戲開始時啟用玩家
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        // 若碰到障礙物
        if (other.transform.tag == "Obstacle") 
        {
            gameObject.SetActive(false);    // 關閉玩家
            GameManager.Instance.GameOver();// 觸發遊戲結束
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    { 
        // 若碰到道具
        if (other.CompareTag("Item")) 
        { 
            Destroy(other.gameObject); // 吃掉道具
        } 
    }
}

