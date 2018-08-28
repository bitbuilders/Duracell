using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    [SerializeField] GameObject m_spawnee = null;
    [SerializeField] [Range(0.0f, 5.0f)] float m_spawnRate = 2.0f;
    [SerializeField] GameObject m_spawnPointMIN = null;
    [SerializeField] GameObject m_spawnPointMAX = null;
    [SerializeField] [Range(0.0f, 90.0f)] float m_startAngleMIN = 15.0f;
    [SerializeField] [Range(0.0f, 90.0f)] float m_startAngleMAX = 35.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_startForceMIN = 1.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_startForceMAX = 2.0f;
    [SerializeField] Difficulty m_difficulty = Difficulty.EASY;

    List<float> m_difficultyRates = new List<float>() { 0.025f, 0.05f, 0.075f };
    List<int> m_difficultyCounts = new List<int>() { 10, 20, 30 };

    float m_currentSpawnRate;
    float m_time;
    int m_count;

    void Start()
    {
        int diff = (int)m_difficulty;
        m_currentSpawnRate = m_spawnRate;
        m_count = m_difficultyCounts[diff];
    }
    
    void Update()
    {
        m_time += Time.deltaTime;
        if (m_time >= m_currentSpawnRate)
        {
            m_time = 0.0f;
            SpawnProduct();
        }

        m_currentSpawnRate -= Time.deltaTime * m_difficultyRates[(int)m_difficulty];
    }

    public void SpawnProduct()
    {
        GameObject go = Instantiate(m_spawnee, GetRandomPoint(), Quaternion.identity, transform);
        Product product = go.GetComponent<Product>();
        Vector3 velocity = Vector3.left;
        float force = Random.Range(m_startForceMIN, m_startForceMAX);
        velocity *= force;
        float angle = Random.Range(m_startAngleMIN, m_startAngleMAX);
        velocity = Quaternion.AngleAxis(-angle, Vector3.forward) * velocity;
        product.InitializeRandom(velocity);
    }

    public Vector2 GetRandomPoint()
    {
        Vector2 point = Vector2.zero;

        float y = Random.Range(m_spawnPointMIN.transform.position.y, m_spawnPointMAX.transform.position.y);
        float x = m_spawnPointMAX.transform.position.x;
        point = new Vector2(x, y);

        return point;
    }
}
