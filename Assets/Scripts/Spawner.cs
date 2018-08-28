using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject m_transition = null;
    [SerializeField] [Range(0.0f, 5.0f)] float m_spawnRate = 2.0f;
    [SerializeField] GameObject m_spawnPointRightMIN = null;
    [SerializeField] GameObject m_spawnPointRightMAX = null;
    [SerializeField] GameObject m_spawnPointLeftMIN = null;
    [SerializeField] GameObject m_spawnPointLeftMAX = null;
    [SerializeField] [Range(0.0f, 90.0f)] float m_startAngleMIN = 15.0f;
    [SerializeField] [Range(0.0f, 90.0f)] float m_startAngleMAX = 35.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_startForceMIN = 1.0f;
    [SerializeField] [Range(0.0f, 10.0f)] float m_startForceMAX = 2.0f;
    [SerializeField] string m_nextLevel = "";
    [SerializeField] Difficulty m_difficulty = Difficulty.EASY;

    public int Count { get; private set; }

    List<float> m_difficultyRates = new List<float>() { 0.035f, 0.070f, 0.090f };
    List<int> m_difficultyCounts = new List<int>() { 10, 18, 25 };

    float m_currentSpawnRate;
    float m_time;

    void Start()
    {
        int diff = (int)m_difficulty;
        m_currentSpawnRate = m_spawnRate;
        Count = m_difficultyCounts[diff];
    }
    
    void Update()
    {
        m_time += Time.deltaTime;
        if (m_time >= m_currentSpawnRate && Count > 0)
        {
            m_time = 0.0f;
            int x = Random.Range(0, 4);
            if (x == 0)
            {
                SpawnProductLeft();
            }
            else
            {
                SpawnProductRight();
            }
            Count--;

            if (Count <= 0.0f)
            {
                StartCoroutine(NextLevel());
            }
        }

        m_currentSpawnRate -= Time.deltaTime * m_difficultyRates[(int)m_difficulty];
    }

    public void SpawnProductRight()
    {
        GameObject go = Instantiate(m_spawnee, GetRandomPointRight(), Quaternion.identity, transform);
        Product product = go.GetComponent<Product>();
        Vector3 velocity = Vector3.left;
        float force = Random.Range(m_startForceMIN, m_startForceMAX);
        velocity *= force;
        float angle = Random.Range(m_startAngleMIN, m_startAngleMAX);
        velocity = Quaternion.AngleAxis(-angle, Vector3.forward) * velocity;
        product.InitializeRandom(velocity);

        Destroy(go, 5.0f);
    }

    public void SpawnProductLeft()
    {
        GameObject go = Instantiate(m_spawnee, GetRandomPointLeft(), Quaternion.identity, transform);
        Product product = go.GetComponent<Product>();
        Vector3 velocity = Vector3.right;
        float force = Random.Range(m_startForceMIN, m_startForceMAX);
        velocity *= force;
        float angle = Random.Range(m_startAngleMIN, m_startAngleMAX);
        velocity = Quaternion.AngleAxis(angle, Vector3.forward) * velocity;
        product.InitializeRandom(velocity);

        Destroy(go, 5.0f);
    }

    public Vector2 GetRandomPointRight()
    {
        Vector2 point = Vector2.zero;

        float y = Random.Range(m_spawnPointRightMIN.transform.position.y, m_spawnPointRightMAX.transform.position.y);
        float x = m_spawnPointRightMAX.transform.position.x;
        point = new Vector2(x, y);

        return point;
    }

    public Vector2 GetRandomPointLeft()
    {
        Vector2 point = Vector2.zero;

        float y = Random.Range(m_spawnPointLeftMIN.transform.position.y, m_spawnPointLeftMAX.transform.position.y);
        float x = m_spawnPointLeftMAX.transform.position.x;
        point = new Vector2(x, y);

        return point;
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(m_transition, Vector3.zero, Quaternion.identity, null);
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene(m_nextLevel);
    }
}
