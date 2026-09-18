using UnityEngine;
using UnityEngine.InputSystem;

public class Obstacle : MonoBehaviour
{
    public float minSide = 0.5f;
    public float maxSide = 2.0f;
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float spinSpeed = 10.0f;
    private float speed = 5f;
    Rigidbody2D rb;
    public GameObject bounceEffect;
    private float randomSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomSize = Random.Range(minSide, maxSide);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;

        float randomTorque = Random.Range(-spinSpeed, spinSpeed) / randomSize;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRb = collision.rigidbody;
        if (otherRb != null && rb.linearVelocity.sqrMagnitude < otherRb.linearVelocity.sqrMagnitude)
        {
            return;
        }
        //lấy vận tốc va chạm
        float impactSpeed = collision.relativeVelocity.magnitude;
        //lấy vị trí va chạm
        Vector2 contactPoint = collision.GetContact(0).point;
        //tạo hiệu ứng
        GameObject effect = Instantiate(bounceEffect, contactPoint, Quaternion.identity);
        //thay đổi tốc độ bắt đầu của hiệu ứng
        var main = effect.GetComponent<ParticleSystem>().main;
        float baseSpeed = main.startSpeed.constant;
        main.startSpeed = Mathf.Lerp(2f, 10f, impactSpeed / maxSpeed);
        //giới han tốc độ
        if (rb.linearVelocity.sqrMagnitude > (speed * speed) / randomSize)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed / randomSize;
        }
    }
}
