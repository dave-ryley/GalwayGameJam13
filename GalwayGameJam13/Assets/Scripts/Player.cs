using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    const float GROUNDED_RADIUS = 0.2f;
    const float TURN_SPEED = 2f;
    [SerializeField] private float _jumpForce = 600f;
    private LayerMask _groundLayer;
    private float _prevRotation = 0f;

    private RectTransform _graphicTransform;
    private Rigidbody2D _rigidbody2D;
    private bool _grounded;
    private float _transition = 1f;
    public int score = 0;

    public void Setup(string key)
    {
        _graphicTransform = transform.Find("Graphic") as RectTransform;
        Transform textTransform = _graphicTransform.Find("Letter");
        TextMesh text = textTransform.GetComponent<TextMesh>();
        text.text = key.ToUpper();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    public void Jump()
    {
        if(_grounded)
        {
            _grounded = false;
            _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
            _transition = 0f;
        }
    }

    public void Update()
    {
        float deltaTime = Time.deltaTime;
        if(_transition < 1f)
        {
            _transition = Mathf.Min(_transition + deltaTime * TURN_SPEED, 1f);
            // TODO: use sine/cos
            _graphicTransform.rotation = Quaternion.Euler(0f, 0f, -(_prevRotation + (_transition * 90f)));
            if(_transition == 1f)
            {
                _prevRotation += 90f;
            }
        }

    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GROUNDED_RADIUS, _groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                // if (!_grounded) OnLandEvent.Invoke();
                _grounded = true;
                return;
            }
        }
        _grounded = false;
    }

    public void ReceieveCoins(int amount)
    {
        score += amount;
    }
}
