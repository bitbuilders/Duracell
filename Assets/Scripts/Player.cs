using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Arrow m_arrow = null;
    [SerializeField] GameObject m_projectile = null;
    [SerializeField] [Range(0.0f, 30.0f)] float m_strengthMIN = 5.0f;
    [SerializeField] [Range(0.0f, 30.0f)] float m_strengthMAX = 20.0f;
    [SerializeField] Transform m_projectilePool = null;

    Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        Vector3 velocity = Vector3.right;
        velocity *= Mathf.Lerp(m_strengthMIN, m_strengthMAX, m_arrow.Power);
        velocity = Quaternion.AngleAxis(m_arrow.Angle, Vector3.forward) * velocity;

        GameObject go = Instantiate(m_projectile, transform.position, Quaternion.identity, m_projectilePool);
        Battery b = go.GetComponent<Battery>();
        b.Initialize(velocity);

        m_animator.SetTrigger("Throw");
    }
}
