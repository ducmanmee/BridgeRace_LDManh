using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public class Character : MonoBehaviour
{
    private string currentAnim;
    [SerializeField] private Animator anim;
    public SkinnedMeshRenderer rendererCharacter;
    [SerializeField] private Rigidbody characterBody;
    private Constain.ColorPlay colorCharacter;

    private List<GameObject> BrickCharacterList;
    [SerializeField] private GameObject brickPlayer;
    [SerializeField] private Transform posBrickPlayer;
    [SerializeField] private MeshRenderer rendererBrickCharacter;
    public LayerMask bridgeLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void OnInit()
    {
        ChangeAnim(Constain.ANIM_IDLE);
        SetMaterialCharacter();
        BrickCharacterList = new List<GameObject>();
        posBrickPlayer.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 1f);
    }

    public virtual void OnDespawn()
    {

    }

    public virtual void Movement()
    {

    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    
    public bool CheckMaterial(BrickGround brickGround)
    {
        if (colorCharacter == brickGround.colorBrick)
        {
            return true;
        }    
        return false;
    }

    public virtual void AddBrick()
    {
        GameObject BrickPlayer = Instantiate(brickPlayer, posBrickPlayer.position, transform.rotation, transform);
        BrickPlayer.GetComponent<MeshRenderer>().material = GetMaterialCharacter();
        BrickCharacterList.Add(BrickPlayer);
        Vector3 temp = posBrickPlayer.position;
        temp.y += .3f;
        posBrickPlayer.position = temp;
    }

    public void SetMaterialCharacter()
    {   
        if (rendererCharacter != null)
        {
            switch ((int)colorCharacter)
            {
                case 0:
                    rendererCharacter.material = GameManager.Instance.GetListMaterial()[0];
                    break;
                case 1:
                    rendererCharacter.material = GameManager.Instance.GetListMaterial()[1];
                    break;
                case 2:
                    rendererCharacter.material = GameManager.Instance.GetListMaterial()[2];
                    break;
                case 3:
                    rendererCharacter.material = GameManager.Instance.GetListMaterial()[3];
                    break;
                case 4:
                    rendererCharacter.material = GameManager.Instance.GetListMaterial()[4];
                    break;
                default:
                    break;
            }
        }
    }    
    public Transform GetPosBrickPlayer() => posBrickPlayer;
    public Material GetMaterialCharacter() => rendererCharacter.material;
    public Constain.ColorPlay GetColorCharacter() => colorCharacter;
    public int GetListBrickCharacter() => BrickCharacterList.Count;
    public void SetColorCharacter(Constain.ColorPlay color)
    {
        colorCharacter = color;
    }

    public virtual void UnBrickBuildBridge()
    {
        if(BrickCharacterList.Count > 0)
        {
            Destroy(BrickCharacterList[BrickCharacterList.Count - 1]);
            BrickCharacterList.RemoveAt(BrickCharacterList.Count - 1);
            Vector3 temp = posBrickPlayer.localPosition;
            temp.y -= .3f;
            posBrickPlayer.localPosition = temp;
        }    
    }

    public bool CheckUpBridge()
    {
        if(characterBody.velocity.y > 0f)
        {
            return true;
        }    
        return false;
    }    

    public Rigidbody GetRigibody() => characterBody;
    protected void BrickEnemyGroundMid()
    {
        if (!MapManager.Instance.GetIsActiveGround2())
        {
            MapManager.Instance.SetIsActiveGround2(true);
            MapManager.Instance.InstantiateBrick(Constain.POSBRICK_MIDGROUND, MapManager.Instance.GetMidArray());
        }
    }

    protected void BrickEnemyGroundEnd()
    {
        if (!MapManager.Instance.GetIsActiveGround3())
        {
            MapManager.Instance.SetIsActiveGround3(true);
            MapManager.Instance.InstantiateBrick(Constain.POSBRICK_ENDGROUND, MapManager.Instance.GetEndArray());
        }
    }
}
