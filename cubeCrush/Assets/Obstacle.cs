using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float destroyX = -10f;

    private void Update()
    {
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}


