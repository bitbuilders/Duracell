using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Explosion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_message = null;
    [SerializeField] [Range(0.0f, 10.0f)] float m_duration = 1.5f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_pause = 0.25f;
    [SerializeField] [Range(0.0f, 1.0f)] float m_percentageFadeIn = 0.75f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / (m_duration * m_percentageFadeIn))
        {
            Color c = m_message.color;
            c.a = i;
            m_message.color = c;
            yield return null;
        }

        Color color = m_message.color;
        color.a = 1.0f;
        m_message.color = color;

        yield return new WaitForSeconds(m_pause);

        for (float i = 1.0f; i > 0.0f; i -= Time.deltaTime / (m_duration * (1.0f - m_percentageFadeIn)))
        {
            Color c = m_message.color;
            c.a = i;
            m_message.color = c;
            yield return null;
        }

        color = m_message.color;
        color.a = 0.0f;
        m_message.color = color;
        Destroy(gameObject, 1.0f);
    }
}
