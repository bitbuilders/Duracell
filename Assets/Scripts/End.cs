using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_message = null;

    Player m_player;

    void Start()
    {
        m_player = FindObjectOfType<Player>();
        m_message.text = "You got " + m_player.Score.ToString("D6") + " points!";
    }
}
