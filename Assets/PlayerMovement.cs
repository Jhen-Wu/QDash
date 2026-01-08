using Unity.VisualScripting; 
using UnityEngine;          

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;       // 玩家 Rigidbody
    [SerializeField] private Transform GFX;        // 玩家圖像物件
    [SerializeField] private float jumpForce = 10f;// 跳躍力道
    [SerializeField] private LayerMask groundLayer;// 地面圖層
    [SerializeField] private Transform feePos;     // 腳底偵測點
    [SerializeField] private float groundDistance = 0.25f; // 地面偵測半徑
    [SerializeField] private float jumpTime = 0.3f;// 可持續跳躍時間
    
    private bool isGrounded = false;  // 是否站在地面
    private bool isJumping = false;   // 是否正在跳躍
    private float jumpTimer;          // 跳躍計時器
        
    private void Update()
    {   
        // 使用圓形偵測是否接觸地面
        isGrounded = Physics2D.OverlapCircle(
            feePos.position, groundDistance, groundLayer
        );
       
        #region JUMPING
        // 按下跳躍鍵，且在地面上
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.linearVelocity = Vector2.up * jumpForce; // 向上施力
            AudioManager.Instance.PlayJump();           // 播放跳躍音效
        }

        // 長按跳躍鍵可跳更高
        if (isJumping && Input.GetButton("Jump"))
        {
            if(jumpTimer < jumpTime)
            {
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // 放開跳躍鍵
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0;
        }
        #endregion
    
        #region CROUCHING
        // 蹲下時縮小角色高度
        if (isGrounded && Input.GetButton("Crouch"))
        {
            GFX.localScale = new Vector3(
                GFX.localScale.x, 0.2f, GFX.localScale.z
            );
        }

        // 放開蹲下鍵恢復大小
        if (Input.GetButtonUp("Crouch"))
        {
            GFX.localScale = new Vector3(
                GFX.localScale.x, 0.39f, GFX.localScale.z
            );
        }
        #endregion
    }
}




