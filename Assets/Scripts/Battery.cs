using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] GameObject m_explosion = null;
    [SerializeField] [Range(0.0f, 900.0f)] float m_initRotMIN = 60.0f;
    [SerializeField] [Range(0.0f, 900.0f)] float m_initRotMAX = 120.0f;

    Player m_owner;

    Rigidbody2D m_rigidbody2D;
    float m_rotationSpeed;
    float m_rotation;
    
    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_rotationSpeed = Random.Range(m_initRotMIN, m_initRotMAX);
    }

    void Update()
    {
        m_rotation += m_rotationSpeed * Time.deltaTime;
        m_rigidbody2D.MoveRotation(-m_rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Product p = collision.GetComponent<Product>();
        if (p != null)
        {
            m_owner.Score += p.PointValue;

            Vector3 dir = p.transform.position - transform.position;
            Vector3 pos = dir * 0.5f + transform.position;
            Instantiate(m_explosion, pos, Quaternion.identity, null);

            Destroy(gameObject);
            Destroy(p.gameObject);
        }
    }

    public void Initialize(Vector3 velocity, Player owner)
    {
        m_rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
        m_owner = owner;
    }
}
