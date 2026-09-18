using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]public float speed = 10f;
    [SerializeField]public float turnSpeed = 20f;
    public InputAction moveAction;
    void Start()
    {
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.forward * Time.deltaTime * speed * moveInput.y);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * moveInput.x);
    }
}
