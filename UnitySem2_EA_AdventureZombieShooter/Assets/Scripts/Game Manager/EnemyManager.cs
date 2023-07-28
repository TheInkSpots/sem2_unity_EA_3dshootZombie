using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;

    public HealthScript healthScript;

    [SerializeField]
    private GameObject boar_Prefab, cannibal_Prefab;

    public Transform[] cannibal_SpawnPoints, boar_SpawnPoints;

    [SerializeField]
    private int cannibal_Enemy_Count, boar_Enemy_Count;

    private int initial_Cannibal_Count, initial_Boar_Count;

    public float wait_Before_Spawn_Enemies_Time = 10f;

    public static int playerScore;
    public static int zombieGotKilled_count;

    public Text tv_playerScore;
    public Text tv_zombieGotKilled_count;




    // Use this for initialization
    void Awake () {
        MakeInstance();
	}

    void Start() {
        initial_Cannibal_Count = cannibal_Enemy_Count;
        initial_Boar_Count = boar_Enemy_Count;

        zombieGotKilled_count = 0;
        playerScore = 0;
        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    void SpawnEnemies() {
        SpawnCannibals();
        SpawnBoars();
    }

    void SpawnCannibals() {

        int index = 0;

        for (int i = 0; i < cannibal_Enemy_Count; i++) {

            if (index >= cannibal_SpawnPoints.Length) {
                index = 0;
            }

            Instantiate(cannibal_Prefab, cannibal_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        cannibal_Enemy_Count = 0;

    }

    void SpawnBoars() {

        int index = 0;

        for (int i = 0; i < boar_Enemy_Count; i++) {

            if (index >= boar_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(boar_Prefab, boar_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        boar_Enemy_Count = 0;

    }

    IEnumerator CheckToSpawnEnemies() {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);

        SpawnCannibals();

        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");

    }

    public void EnemyDied(bool cannibal) {

        if(cannibal) {

            playerScore += 3;
            zombieGotKilled_count++;
            Debug.Log("zomibeKill(EnemyDied) score:  " + playerScore);

            tv_playerScore.text = "Score:" + (playerScore).ToString(); 
            tv_zombieGotKilled_count.text = "Kill:" + (zombieGotKilled_count).ToString(); 

            cannibal_Enemy_Count++;

            if(cannibal_Enemy_Count > initial_Cannibal_Count) {
                cannibal_Enemy_Count = initial_Cannibal_Count;
            }

        } else {

            boar_Enemy_Count++;

            if(boar_Enemy_Count > initial_Boar_Count) {
                boar_Enemy_Count = initial_Boar_Count;
            }

        }

    }

    public void StopSpawning() {
        StopCoroutine("CheckToSpawnEnemies");
    }

} // class


































