using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 900.0f)] float m_initRotMIN = 60.0f;
    [SerializeField] [Range(0.0f, 900.0f)] float m_initRotMAX = 120.0f;

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

    public void Initialize(Vector3 velocity)
    {
        m_rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
    }
}
