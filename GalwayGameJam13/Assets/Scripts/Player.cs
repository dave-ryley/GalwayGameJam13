using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    const float GROUNDED_RADIUS = 0.2f;
    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;

    private Rigidbody2D _rigidbody2D;
    private bool _grounded;

    public void Setup(string key)
    {
        Transform textTransform = transform.GetChild(0);
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
        }
    }

    public void Update()
    {
        float deltaTime = Time.deltaTime;

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
}
