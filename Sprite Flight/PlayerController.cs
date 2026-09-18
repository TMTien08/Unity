using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    public float maxSpeed = 10f;
    public GameObject jetEngine;
    private float elapsedTime = 0f;
    Rigidbody2D rb;
    private int score = 0;
    private float scoreMultiplier = 5f;
    public UIDocument uiDocument;
    private Label scoreText;
    private Button restartButton;
    private Label highScoreText;
    public GameObject explosionEffect;
    public GameObject border;
    private AudioSource audioSource;
    public AudioClip explosionClip;
    public InputAction moveForward;
    public InputAction lookPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
        highScoreText = uiDocument.rootVisualElement.Q<Label>("HighScoreLabel");
        highScoreText.style.display = DisplayStyle.None;
        moveForward.Enable();
        lookPosition.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();
    }
    private void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(scoreMultiplier * elapsedTime);
        scoreText.text = "Score: " + score;
    }
    private void MovePlayer()
    {
        if (moveForward.WasPressedThisFrame())
        {
            jetEngine.SetActive(true);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (moveForward.WasReleasedThisFrame())
        {
            jetEngine.SetActive(false);
            audioSource.Stop();
        }
        //kiểm tra ấn chuột trái
        if (moveForward.IsPressed())
        {
            //chuyển tọa độ màn hình thành tọa dộ thế giới
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(lookPosition.ReadValue<Vector2>());
            //tính vector từ vị trí người chơi đến vị trí chuột và cố định chiều dài vector = 1
            Vector2 direction = (mousePos - transform.position).normalized;
            //chỉnh hướng người chơi
            transform.up = direction;
            //thêm lực đẩy
            rb.AddForce(direction * thrustForce);
            //kiểm tra nếu vận tốc tuyến tính > tốc độ tối đa thì giới hạn lại ở tốc độ tối đa
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionClip, transform.position);
        restartButton.style.display = DisplayStyle.Flex;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        highScoreText.text = "High score: " + PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.style.display = DisplayStyle.Flex;

        border.SetActive(false);
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
