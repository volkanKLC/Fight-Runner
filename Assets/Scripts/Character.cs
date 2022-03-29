using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Character : MonoBehaviour
{

    [Header("POWER TEXT")]
    public Text powerTXT;

    [Space(10)]

    [Header("MATERIALS")]
    public List<Material> playerMaterials;

    [Space(10)]

    [Header("ANIMATION")]
    public Animator playerAnim;

    public int playerPower = 0;

    private void Start()
    {
        UpdatePower();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("redgate"))
        {
            GetComponentInChildren<Renderer>().material = playerMaterials[2];
        }
       
        if (other.CompareTag("yellowgate"))
        {
            GetComponentInChildren<Renderer>().material = playerMaterials[1];
        }
       
        if (other.CompareTag("bluegate"))
        {
            GetComponentInChildren<Renderer>().material = playerMaterials[0];
        }

        if (other.CompareTag("ball"))
        {
            Destroy(other.gameObject);
            if (other.GetComponent<Renderer>().material.color != GetComponentInChildren<Renderer>().material.color)
            {
                //Debug.Log("farklý renk");
                playerPower--;
                UpdatePower();

                // To do Game Over
                //Debug.Log("GAME OVER");
            }
            else
            {
               // Debug.Log("ayný renk");
                playerPower++;
                UpdatePower();
                GetComponentInChildren<Renderer>().material = other.GetComponent<Renderer>().material;
            }

        }
        if (other.CompareTag("enemyTrigger"))
        {
            // Enemy Fight
            PlayerController.Instance.speed = 0;
            StartCoroutine(moveEnemy(other.transform.parent.position, other.gameObject));
        }
        if (other.CompareTag("bossTrigger"))
        {
            PlayerController.Instance.speed = 0;
            StartCoroutine(bossFight(other.transform.parent.position, other.transform.GetComponentInParent<EnemyAI>().gameObject));
        }
    }

    // Boss Fight
    private IEnumerator bossFight(Vector3 targetPos, GameObject enemy)
    {
        float t = 0;
        Vector3 firstPos = transform.position;
        Vector3 firstScale = transform.localScale;
        Vector3 targetScale = new Vector3(5, 5, 5);

        while (t < 1)
        {
            t += Time.deltaTime / 2;
            transform.position = Vector3.Lerp(firstPos, new Vector3(targetPos.x, transform.position.y, targetPos.z - 2), t);
            transform.localScale = Vector3.Lerp(firstScale, targetScale, t);
            yield return 0;
        }
        yield return new WaitForSeconds(.1f);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Fight", true);

        // Enemy AI Animation
        enemy.transform.GetComponentInParent<EnemyAI>().enemyAnim.enabled = true;
        enemy.transform.GetComponentInParent<EnemyAI>().enemyAnim.SetBool("Fight", true);

        yield return new WaitForSeconds(2f);
        FightWinnerControl(enemy.transform.GetComponentInParent<EnemyAI>().enemyPower, enemy.transform.GetComponentInParent<EnemyAI>().gameObject);

    }

    // Enemy Fight
    private IEnumerator moveEnemy(Vector3 targetPos, GameObject enemy)
    {
        float t = 0;
        Vector3 firstPos = transform.position;

        while (t < 1)
        {
            t += Time.deltaTime / 2;
            transform.position = Vector3.Lerp(firstPos, new Vector3(targetPos.x, transform.position.y, targetPos.z - 2), t);
            yield return 0;
        }
        yield return new WaitForSeconds(.1f);
        playerAnim.SetBool("Run", false);
        playerAnim.SetBool("Fight", true);

        // Enemy AI Animation
        enemy.transform.GetComponentInParent<EnemyAI>().enemyAnim.enabled = true;
        enemy.transform.GetComponentInParent<EnemyAI>().enemyAnim.SetBool("Fight", true);

        yield return new WaitForSeconds(2f);
        FightWinnerControl(enemy.transform.GetComponentInParent<EnemyAI>().enemyPower, enemy.transform.GetComponentInParent<EnemyAI>().gameObject);
    }

    public void FightWinnerControl(int enemyPower, GameObject obj)
    {
        if (playerPower < enemyPower)
        {
            Time.timeScale = 0;
            UImanager.ýnstance.gameOverPanel.SetActive(true);
            Debug.Log("loser");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("winner");
            Destroy(obj);
            StopAllCoroutines();
            PlayerController.Instance.speed = 10;
            playerAnim.SetBool("Fight", false);
            playerAnim.SetBool("Run", true);
        }
    }


    public void UpdatePower()
    {
        powerTXT.text = "POWER : " + playerPower;
    }
}
