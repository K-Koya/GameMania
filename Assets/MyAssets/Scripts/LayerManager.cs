using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField] string _LayerNameGround = "Ground";
    [SerializeField] string _LayerNamePlayer = "Player";
    [SerializeField] string _LayerNamePlayerAttack = "PlayerAttack";
    [SerializeField] string _LayerNameEnemy = "Enemy";

    public static int Ground = -1;
    public static int Player = -1;
    public static int PlayerAttack = -1;
    public static int Enemy = -1;

    void Awake()
    {
        Ground = LayerMask.NameToLayer(_LayerNameGround);
        Player = LayerMask.NameToLayer(_LayerNamePlayer);
        PlayerAttack = LayerMask.NameToLayer(_LayerNamePlayerAttack);
        Enemy = LayerMask.NameToLayer(_LayerNameEnemy);
    }
}
