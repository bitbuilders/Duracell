using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] Player m_player = null;
    [SerializeField] Spawner m_spawner = null;
    [SerializeField] TextMeshProUGUI m_scoreText = null;
    [SerializeField] TextMeshProUGUI m_countText = null;

    void Start()
    {
        m_player = FindObjectOfType<Player>();
        m_spawner = FindObjectOfType<Spawner>();
    }
    
    void Update()
    {
        m_scoreText.text = m_player.Score.ToString("D6");
        m_countText.text = m_spawner.Count.ToString();
    }
}
