using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyColor
{
    Blue,
    Red,
    Yellow
}
public class EnemyAI : MonoBehaviour
{
    [Header("ENEMY COLOR")]
    public EnemyColor selectColor;

    [Space(10)]

    [Header("ENEMY POWER")]
    public int enemyPower;

    [Space(10)]

    [Header("ANIMATION")]
    public Animator enemyAnim;

    [Space(10)]

    [Header("ENEMY MATERIALS")]
    public List<Material> enemyMaterials;

    private void Start()
    {
        SelectEnemyColor(selectColor);
    }
    private void SelectEnemyColor(EnemyColor color)
    {
        switch (color)
        {
            case EnemyColor.Blue:
                GetComponentInChildren<Renderer>().material = enemyMaterials[0];
                break;
            case EnemyColor.Red:
                GetComponentInChildren<Renderer>().material = enemyMaterials[1];
                break;
            case EnemyColor.Yellow:
                GetComponentInChildren<Renderer>().material = enemyMaterials[2];
                break;
        }
    }

}
