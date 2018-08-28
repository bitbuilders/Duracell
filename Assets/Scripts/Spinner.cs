using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] [Range(180.0f, -180.0f)] float m_angleTo = -90.0f;
    [SerializeField] [Range(0.0f, 30.0f)] float m_rotationSpeed = 2.0f;
    [SerializeField] AnimationCurve m_rotationOverLife = null;

    RectTransform m_rectTransform;
    float m_angleStart;
    float m_currentAngle;
    float m_time;
    bool m_goingTo;

    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_angleStart = m_rectTransform.localEulerAngles.z;
        if (m_angleStart > 180.0f)
            m_angleStart -= 360.0f;
        m_currentAngle = m_angleStart;
        m_goingTo = true;
    }
    
    void Update()
    {
        m_time += Time.deltaTime * m_rotationSpeed;
        float t = m_rotationOverLife.Evaluate(m_time);
        if (m_goingTo)
        {
            m_currentAngle = Mathf.Lerp(m_angleStart, m_angleTo, t);
        }
        else
        {
            m_currentAngle = Mathf.Lerp(m_angleTo, m_angleStart, t);
        }

        if (m_time >= 1.0f)
        {
            m_goingTo = !m_goingTo;
            m_time = 0.0f;
        }

        m_rectTransform.rotation = Quaternion.AngleAxis(m_currentAngle, Vector3.forward);
    }
}
