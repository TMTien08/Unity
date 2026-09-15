using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 8, -7);
    private Quaternion turn = Quaternion.Euler(30, 0, 0);
    public InputAction switcherAction;
    private bool driSeat = false;
    public bool isLeft;
    void Start()
    {
        switcherAction.Enable();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.TransformPoint(offset);
        transform.rotation = player.transform.rotation * turn;
        if (switcherAction.WasPressedThisFrame())
        {
            CameraSwitcher();
        }
        Camera cam = GetComponent<Camera>();
        if (isLeft)
        {
            cam.rect = new Rect(0f, 0f, 0.5f, 1f);
        }
        else
        {
            cam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
    }
    void CameraSwitcher()
    {
        driSeat = !driSeat;
        if (driSeat)
        {
            offset = new Vector3(0, 3, 2.5f);
            turn = Quaternion.Euler(10, 0, 0);
        }
        else
        {
            offset = new Vector3(0, 8, -7);
            turn = Quaternion.Euler(30, 0, 0);
        }
    }
}
