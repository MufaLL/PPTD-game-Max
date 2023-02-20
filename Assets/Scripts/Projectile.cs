using UnityEngine;





public enum projectileType
{
    Projectile1, Projectile2, Projectile3
}

public class Projectile : MonoBehaviour
{
    [SerializeField]
    int attackDamage;

    [SerializeField]
    projectileType pType;

    public int AttackDamage
    {
        get
        {


            return attackDamage;
        }
    }

    void Start()
    {
        
        Manager.Instance.RegisterProjectile(this);
        Debug.Log("тест 3");
    }


    public projectileType PType
    {
        get
        {


            return pType;
        }
    }
     
}
