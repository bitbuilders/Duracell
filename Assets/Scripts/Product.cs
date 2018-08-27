using System.Collections;
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
    [SerializeField] ProductType m_type = ProductType.FLASHLIGHT;

    SpriteRenderer m_spriteRenderer;
    Vector3 m_velocity;
    
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        transform.position += m_velocity;
    }

    public void Initialize(Vector3 velocity, ProductType type = ProductType.FLASHLIGHT)
    {
        m_velocity = velocity;

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
}
