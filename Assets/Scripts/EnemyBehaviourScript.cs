
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    // 0 esquerda, 1 direita
    private int direction_;

    public SpriteRenderer sprite_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    public void SetEnemyDirection(int dir)
    {
        direction_ = dir;
    }

    void MoveEnemy()
    {
        transform.Translate((direction_ == 0 ? Vector3.left : Vector3.right) * 2f * Time.deltaTime);
        sprite_.GetComponent<SpriteRenderer>().flipY = !(direction_ == 0);
        EnemyViewport();
    }
    void EnemyViewport()
    {
        float viewportPos = Camera.main.WorldToViewportPoint(transform.position).x;

        if (viewportPos < 0 || viewportPos > 1)
        {
            Destroy(gameObject);
        }

    }
}
