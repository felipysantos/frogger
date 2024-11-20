using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    // Não é o ideal botar aqui mas pra esse jorgin, tá rolante >.o
    public Text score_text;
    public Text gameover_text;
    private int score = 0;
    private float initial_position = -4.5f;
    public int move_speed;
    public SpriteRenderer sprite_;

    // caso true, congela a cena
    private bool should_reset = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score_text.text = "Score: " + score;
        gameover_text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (should_reset)
        {
            StartCoroutine(ResetScene());
            should_reset = false;
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, move_speed * Time.deltaTime, 0);
            sprite_.GetComponent<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, 0); ;

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(move_speed * Time.deltaTime, 0, 0);
            sprite_.GetComponent<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, -90); ;


        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-move_speed * Time.deltaTime, 0, 0);
            sprite_.GetComponent<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, 90); ;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -move_speed * Time.deltaTime, 0);
            sprite_.GetComponent<SpriteRenderer>().transform.rotation = Quaternion.Euler(180, 0, 0); ;
        }
        ScoreOrDeath();
    }

    void ScoreOrDeath()
    {
        Vector3 screenEdge = Camera.main.WorldToViewportPoint(transform.position);

        if (screenEdge.y > 1)
        {
            StartCoroutine(FreezeGame());
            score += 1;
            score_text.text = "Score: " + score;
            transform.position = new Vector3(0, initial_position, 0);
        }
        if (screenEdge.x < 0 || screenEdge.x > 1 || screenEdge.y < 0)
        {
            should_reset = true;
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            should_reset = true;
        }
    }

    private IEnumerator ResetScene()
    {
        gameover_text.text = "GAME OVER";
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator FreezeGame()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1f;
    }

}
