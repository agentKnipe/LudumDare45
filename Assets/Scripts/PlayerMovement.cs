using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float Jump = .2f;
    public Rigidbody2D Rb;

    bool _isJumping = false;
    int _jumpModifier = 1;

    //Vector2 _movement;
    float _movement;
    Vector2 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            _isJumping = true;
            if (Input.GetKey(KeyCode.A)) {
                _jumpModifier = -1;
            }
            else {
                _jumpModifier = 1;
            }
        }

        _movement = Input.GetAxis("Vertical") * Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        var forward = Vector2.Lerp(Vector2.zero, transform.up * _movement, .8f);
        Rb.velocity = forward;

        //rotate to face the mouse
        var lookDir = _mousePosition - Rb.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Rb.rotation = angle;

        if (_isJumping) {
            transform.Translate(Vector2.right * Jump * _jumpModifier);
            _isJumping = false;
        }
    }

    void OnDrawGizmos() {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.green;
        Vector2 direction = transform.TransformDirection(Vector2.up) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
