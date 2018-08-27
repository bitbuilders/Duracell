﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [System.Serializable]
    public enum ProductType
    {
        FLASHLIGHT,
        ROBOT,
        TOASTER
    }

    [SerializeField] Sprite m_flashlightPNG = null;
    [SerializeField] Sprite m_toasterPNG = null;
    [SerializeField] Sprite m_robotPNG = null;
    [SerializeField] [Range(0.0f, 10.0f)] float m_falloffRate = 1.0f;
    [SerializeField] ProductType m_type = ProductType.FLASHLIGHT;

    Rigidbody2D m_rigidbody2D;
    SpriteRenderer m_spriteRenderer;
    Vector3 m_velocity;
    
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {

    }

    public void Initialize(Vector3 velocity, ProductType type = ProductType.FLASHLIGHT)
    {
        m_velocity = velocity;
        if (m_rigidbody2D == null)
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_rigidbody2D.AddForce(m_velocity, ForceMode2D.Impulse);
        
        if (m_spriteRenderer == null)
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_type = type;
        switch (m_type)
        {
            case ProductType.FLASHLIGHT:
                m_spriteRenderer.sprite = m_flashlightPNG;
                break;
            case ProductType.ROBOT:
                m_spriteRenderer.sprite = m_robotPNG;
                break;
            case ProductType.TOASTER:
                m_spriteRenderer.sprite = m_toasterPNG;
                break;
        }
    }

    public void InitializeRandom(Vector3 velocity)
    {
        m_velocity = velocity;
        if (m_rigidbody2D == null)
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_rigidbody2D.AddForce(m_velocity, ForceMode2D.Impulse);

        if (m_spriteRenderer == null)
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        int x = Random.Range(0, 3);
        m_type = (ProductType)x;
        switch (m_type)
        {
            case ProductType.FLASHLIGHT:
                m_spriteRenderer.sprite = m_flashlightPNG;
                break;
            case ProductType.ROBOT:
                m_spriteRenderer.sprite = m_robotPNG;
                break;
            case ProductType.TOASTER:
                m_spriteRenderer.sprite = m_toasterPNG;
                break;
        }
    }
}
