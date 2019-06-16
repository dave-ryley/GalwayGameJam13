using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    const float GROUNDED_RADIUS = 0.1f;
    const float PLAYER_RADIUS = 0.7f;
    const float TURN_SPEED = 2f;
    [SerializeField] private float _jumpForce = 600f;
    private LayerMask _groundLayer;
    private LayerMask _obstacleLayer;
    private LayerMask _coinLayer;
    private float _prevRotation = 0f;
    private RectTransform _graphicTransform;
    private Rigidbody2D _rigidbody2D;
    private float _transition = 1f;
    private float _growOffset = 0f;

    public int score = 0;

    public void Setup(string key)
    {
        gameObject.name = key;
        _graphicTransform = transform.Find("Graphic") as RectTransform;
        Transform textTransform = _graphicTransform.Find("Letter");
        TextMesh text = textTransform.GetComponent<TextMesh>();
        text.text = key.ToUpper();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");
        _obstacleLayer = LayerMask.GetMask("Obstacle");
        _coinLayer = LayerMask.GetMask("Coin");
    }

    public void Jump()
    {
        bool grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GROUNDED_RADIUS, _groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                break;
            }
        }
        if(grounded)
        {
            _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
            _transition = 0f;
        }
    }

    public void Grow()
    {
        float deltaTime = Time.deltaTime;
        _growOffset += Time.deltaTime;
        if(_growOffset > 1f)
        {
            GGJGameManager.KillPlayer(this);
            return;
        }
        else if(_growOffset > 0.3)
        {
            float offset = _growOffset - 0.3f;
            _graphicTransform.localPosition = new Vector3(Mathf.Sin(offset*100) * offset, _graphicTransform.localPosition.y, 0f);
            _graphicTransform.localScale = new Vector3(1f + offset, 1f + offset);
        }
    }

    public void ResetSize()
    {
        _growOffset = 0f;
        _graphicTransform.localScale = Vector3.one;
        _graphicTransform.localPosition = new Vector3(0f, _graphicTransform.localPosition.y, 0f);
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0f, 0.5f, 0f), PLAYER_RADIUS, _obstacleLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                GGJGameManager.KillPlayer(this);
                return;
            }
        }

        colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0f, 0.5f, 0f), PLAYER_RADIUS, _coinLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                this.receieveCoins(1);
                Destroy(colliders[i].gameObject);
                return;
            }

        }
    }

    public void receieveCoins(int amount)
    {
        score += amount;
        GameObject.Find("Text:" + this.gameObject.name).GetComponent<Text>().text = this.gameObject.name.ToUpper() + ": " + this.score;
    }
}

    

