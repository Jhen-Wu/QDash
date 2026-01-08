using UnityEngine; 

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // 全域音效管理器

    [SerializeField] private AudioSource startBGM;    // 開頭背景音樂
    [SerializeField] private AudioSource gameOverSFX; // 結束音效
    [SerializeField] private AudioSource jumpSFX;     // 跳躍音效

    private void Awake()
    {
        // Singleton + 切場景不銷毀
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        PlayStartSound(); // 第一次進入遊戲播放
    }

    public void PlayStartSound()
    {
        if (!startBGM.isPlaying)
            startBGM.Play();
    }

    public void StopStartSound()
    {
        if (startBGM.isPlaying)
            startBGM.Stop();
    }

    public void PlayGameOver()
    {
        gameOverSFX.Play();
    }

    public void PlayJump()
    {
        jumpSFX.Play();
    }

    public void StopGameOver()
    {
        if (gameOverSFX.isPlaying)
            gameOverSFX.Stop();
    }
}


