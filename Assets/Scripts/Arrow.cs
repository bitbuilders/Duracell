using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Transform m_source = null;
    [SerializeField] [Range(0.0f, 10.0f)] float m_distanceFromSource = 0.5f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_scaleUp = 2.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_scaleSpeed = 2.0f;
    [SerializeField] AnimationCurve m_sizeOverLife = null;
    [SerializeField] Color m_endColor = Color.red;
    [SerializeField] AnimationCurve m_colorOverLife = null;

    public float Power { get { return m_time; } }
    public float Angle {
        get
        {
            Vector3 dir = m_target - m_source.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x);
            angle *= Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, 0.0f, 70.0f);
            return angle;
        }
    }

    SpriteRenderer m_spriteRenderer;
    Vector3 m_target;
    float m_offset;
    float m_time;
    float m_t;
    bool m_holding;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_offset = transform.localScale.x;
    }
    
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            m_holding = true;
        }
        else
        {
            m_holding = false;
        }

        if (m_holding)
        {
            m_t += Time.deltaTime;
            m_time = Mathf.PingPong(m_t * m_scaleSpeed, 1.0f);
        }
        else
        {
            m_time -= Time.deltaTime * m_scaleSpeed * 4.0f;
            m_t = 0.0f;
        }
        m_time = Mathf.Clamp01(m_time);
        float x = (m_sizeOverLife.Evaluate(m_time) * m_scaleUp) + 0.5f;
        transform.localScale = new Vector3(x, 0.5f, 1.0f);

        Color c = Color.Lerp(Color.white, m_endColor, m_colorOverLife.Evaluate(m_time));
        m_spriteRenderer.color = c;

        Vector3 mouse = Input.mousePosition;
        m_target = Camera.main.ScreenToWorldPoint(mouse);
        m_target.z = 0.0f;
        
        transform.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);

        m_offset = transform.localScale.x;
        float offset = m_distanceFromSource + (m_offset - 1.0f);
        Vector3 p = new Vector3(offset, 0.0f, 0.0f);
        p = transform.rotation * p;
        Vector3 dir = m_target - (m_source.transform.position);
        Vector3 pos = dir.normalized * offset;
        transform.position = m_source.transform.position + p;
    }
}
