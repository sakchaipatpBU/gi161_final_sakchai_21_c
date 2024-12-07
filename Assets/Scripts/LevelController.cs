using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public float TimeToStart;
    public GameObject Wave;
}

public class LevelController : MonoBehaviour
{
    public int WaveCount;
    public EnemyWave[] EnemyWave;
    public GameObject Powerup;
    public float TimeForNewPowerup;
    public GameObject[] Asteroids;
    public float TimeBetweenAsteroid;
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
        WaveCount = EnemyWave.Length;
        for (int i = 0; i < EnemyWave.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(EnemyWave[i].TimeToStart, EnemyWave[i].Wave));
        }
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(AsteroidCreation());
    }

    IEnumerator CreateEnemyWave(float delay, GameObject wave)
    {
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        Instantiate(wave);
        WaveCount--;
        if(WaveCount == 0)
        {
            GameManager.Instance.IsNoWaveRemain = true;
        }
    }
    IEnumerator PowerupBonusCreation()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeForNewPowerup);
            Instantiate(Powerup, SetPositionObjectSpawn(), Quaternion.identity);
        }
    }

    IEnumerator AsteroidCreation()
    {
        yield return new WaitForSeconds(TimeBetweenAsteroid);
        while (true)
        {
            int randomIndex = Random.Range(0, Asteroids.Length);
            Instantiate(Asteroids[randomIndex], SetPositionObjectSpawn(), Quaternion.identity);
            yield return new WaitForSeconds(TimeBetweenAsteroid);
        }
    }
    private Vector2 SetPositionObjectSpawn()
    {
        float x, newPosY;
        float minY = PlayerController.Instance.MinY;
        float maxY = PlayerController.Instance.MaxY;
        newPosY = Random.Range(minY, maxY);
        x = mainCamera.ViewportToWorldPoint(Vector2.right).x +1;
        return new Vector2(x, newPosY);
    }

}
