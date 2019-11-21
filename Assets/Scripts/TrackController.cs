using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public GameObject ZombieEnemy;
    public GameObject WhiteWomanEnemy;

    public List<GameObject> AvailableTracks;

    public float BoundsLimit = 10f;

    private GameObject previousTrack;
    private GameObject currentTrack;
    private GameObject nextTrack;




    // Start is called before the first frame update
    void Start()
    {
        previousTrack = GetRandomTrack();
        currentTrack = GetRandomTrack();
        nextTrack = GetRandomTrack();

        currentTrack.transform.position = this.transform.position;
        DrawPreviousTrack();
        DrawNextTrack();

    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x - this.currentTrack.transform.position.x > BoundsLimit)
        {
            GameObject.Destroy(previousTrack);
            previousTrack = currentTrack;
            currentTrack = nextTrack;
            nextTrack = GetRandomTrack();
            DrawNextTrack();
            RandomizeEnemies(nextTrack);

        }

    }

    public GameObject GetRandomTrack()
    {
        var trackModel = AvailableTracks[Random.Range(0, AvailableTracks.Count)];
        return GameObject.Instantiate(trackModel);
    }

    private void RandomizeEnemies(GameObject trackModel)
    {

        var enemyPositions = GameHelper.FindComponentsInChildrenWithTag<Transform>(trackModel, "EnemySpot");
        if (!enemyPositions.Any()) return;

        var randomEnemies = Random.Range(0, 4);
        for(var i=0; i<randomEnemies; i++)
        {
            var randomIndex = Random.Range(0, enemyPositions.Length);
            var randomEnemy = Random.Range(0, 10);
            var currentPosition =  enemyPositions[randomIndex].transform.position;
            enemyPositions = enemyPositions.Where((val, idx) => idx != randomIndex).ToArray();

            GameObject enemy = null;
            if(randomEnemies < 8)
            {
                enemy = GameObject.Instantiate(ZombieEnemy);
            }
            else
            {
                enemy = GameObject.Instantiate(WhiteWomanEnemy);
            }
            enemy.transform.position = currentPosition;
            
            
        }

    }

    public void DrawPreviousTrack()
    {
        float previousTrackWidth = this.previousTrack.GetComponent<SpriteRenderer>().bounds.size.x;

        float previousPositionx = currentTrack.transform.position.x - previousTrackWidth;
        previousTrack.transform.position = new Vector2(previousPositionx, currentTrack.transform.position.y);

    }

    public void DrawNextTrack()
    {
        float currentTrackWidth = this.currentTrack.GetComponent<SpriteRenderer>().bounds.size.x;
        float nextPositionX = currentTrack.transform.position.x + currentTrackWidth;
        nextTrack.transform.position = new Vector2(nextPositionX, currentTrack.transform.position.y);

    }

}
