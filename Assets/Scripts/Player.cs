﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Arrow m_arrow = null;
    [SerializeField] GameObject m_projectile = null;
    [SerializeField] [Range(0.0f, 30.0f)] float m_strengthMIN = 5.0f;
    [SerializeField] [Range(0.0f, 30.0f)] float m_strengthMAX = 20.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_throwCooldown = 0.1f;
    //[SerializeField] Transform m_projectilePool = null;

    public int Score { get; set; }

    AudioSource m_audioSource;
    Animator m_animator;
    float m_time;

    static Player ms_instance;
    void Start()
    {
        if (ms_instance == null)
        {
            ms_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        m_time += Time.deltaTime;
        if (m_time >= m_throwCooldown && Input.GetButtonDown("Fire1"))
        {
            m_time = 0.0f;
            Fire();
        }
    }

    public void Fire()
    {
        Vector3 velocity = Vector3.right;
        velocity *= Mathf.Lerp(m_strengthMIN, m_strengthMAX, m_arrow.Power);
        velocity = Quaternion.AngleAxis(m_arrow.Angle, Vector3.forward) * velocity;

        GameObject go = Instantiate(m_projectile, transform.position, Quaternion.identity, null);
        Battery b = go.GetComponent<Battery>();
        b.Initialize(velocity, this);

        Destroy(go, 4.0f);

        m_animator.SetTrigger("Throw");
        m_audioSource.Play();
    }
}
