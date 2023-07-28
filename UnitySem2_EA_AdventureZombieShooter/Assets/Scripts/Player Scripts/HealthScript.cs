using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour {

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool is_Player, is_Boar, is_Cannibal;

    public bool is_Dead;

    private EnemyAudio enemyAudio;

    private PlayerStats player_Stats;

    public GameObject playerDieUI;

    GameObject HScoreManagerObj;
    HighScoreManager HScoreManager;

    // public int playerScore;
    //public int zombieGotKilled_count;

    //public Text tv_playerScore;
    //public Text tv_zombieGotKilled_count;


    void Awake () {

        playerDieUI = GetComponent<GameObject>();
        //tv_playerScore = GetComponent<Text>();
        //tv_zombieGotKilled_count = GetComponent<Text>();

        // playerScore = 0;
        //tv_zombieGotKilled_count = 0;


        HScoreManagerObj = GameObject.Find("HighScoreManager");
        HScoreManager = HScoreManagerObj.GetComponent<HighScoreManager>();

        if (is_Boar || is_Cannibal) {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            // get enemy audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if(is_Player) {
            player_Stats = GetComponent<PlayerStats>();

        }

	}
	
    public void ApplyDamage(float damage) {

        // if we died don't execute the rest of the code
        if (is_Dead)
            return;

        health -= damage;

        if(is_Player) {
            // show the stats(display the health UI value)
            player_Stats.Display_HealthStats(health);
        }

        if(is_Boar || is_Cannibal) {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL) {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if(health <= 0f) {

            PlayerDied();
                // ??? UI of dead
            is_Dead = true;
        }

    } // apply damage

     void PlayerDied() {

        if(is_Cannibal) {

            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            StartCoroutine(DeadSound());

            
            //zombieGotKilled_count ++;
            //playerScore ++;
            //Debug.Log("zomibeKill(PlayerDied), score:" + playerScore + "countKilled:"+ zombieGotKilled_count);
            //tv_playerScore.text = "Score:" + (playerScore).ToString(); 
            //tv_zombieGotKilled_count.text = "Kill:" + (zombieGotKilled_count).ToString(); 

            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(true);
        }

        if(is_Boar) {

            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();

            StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(false);
        }

        if(is_Player) {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // call enemy manager to stop spawning enemis
            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }

        if(tag == Tags.PLAYER_TAG) {


            // UI of dead ???
            HScoreManager.saveScore();
            DontDestroyOnLoad(HScoreManager);
            //playerDieUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Invoke("RestartGame", 3f);


        } else {

            Invoke("TurnOffGameObject", 3f);

        }

    } // player died

    void RestartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound() {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }

} // class









































