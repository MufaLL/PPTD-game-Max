
using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    GameObject towerObject;
    [SerializeField]
    Sptite dragSprite;

    public GameObject TowerObject
    {
        get
        {



            return towerObject;
        }
    }




    public Sprite DragSprite
    {
        get
        {



            return dragSprite;
        }
    }
}
