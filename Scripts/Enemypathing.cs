using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> wayPoints;


    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.getWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       Move(); 
    }

    private void Move()
    {
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            var movementThisFrame = waveConfig.getMoveSpeed() * Time.deltaTime;
            // Vector2.MoveTowards(Vector2 current, Vector2 target, float MaxDistanceDelta)
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            
            if (transform.position == targetPosition)
            {
                wayPointIndex ++;
            }
        }
        else 
        {
            Destroy(gameObject);
        }

    }

    public void setWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
