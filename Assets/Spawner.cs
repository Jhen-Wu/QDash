using UnityEngine; 

public class Spawner : MonoBehaviour 
{
    [SerializeField] private GameObject[] obstaclePrefabs; // 障礙物預製物
    [SerializeField] private Transform obstacleParent;     // 障礙物父物件

    public float obstacleSpawnTime = 2f;        // 初始生成間隔
    [Range(0,1)] public float obstacleSpawnTimeFactor = 0.1f; // 生成加速係數
    public float obstacleSpeed = 1f;            // 初始速度
    [Range(0,1)] public float obstacleSpeedFactor = 0.2f;     // 速度成長係數

    private float _obstacleSpawnTime;            // 實際生成間隔
    private float _obstacleSpeed;                // 實際速度
    private float timeAlive;                     // 存活時間
    private float timeUntilObstacleSpawn;        // 生成倒數

    private void Start() 
    {
        // 訂閱事件
        GameManager.Instance.onGameOver.AddListener(ClaerObstacles);
        GameManager.Instance.onPlay.AddListener(ResetFactors);
    }

    private void Update() 
    {
        if (GameManager.Instance.isPlaying) 
        {
            timeAlive += Time.deltaTime; // 遊戲時間累積
            CalculateFactors();          // 計算難度
            SpawnLoop();                 // 控制生成
        }
    }

    private void SpawnLoop() 
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= _obstacleSpawnTime) 
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void ResetFactors() 
    {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
    }

    private void ClaerObstacles() 
    {
        // 清除所有場上障礙物
        foreach (Transform child in obstacleParent) 
        {
            Destroy(child.gameObject);
        }
    }

    private void CalculateFactors() 
    {
        // 隨時間增加難度
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void Spawn() 
    {
        // 隨機選擇障礙物
        GameObject obstacleToSpawn =
            obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // 生成障礙物
        GameObject spawnedObstacle =
            Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

        spawnedObstacle.transform.parent = obstacleParent;

        // 設定移動速度
        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.linearVelocity = Vector2.left * _obstacleSpeed;
    }
}




