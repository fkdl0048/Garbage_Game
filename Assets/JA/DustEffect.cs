using UnityEngine;

public class DustEffect : MonoBehaviour
{
    [SerializeField] private GameObject dustEffect; 
    
    private static DustEffect instance = null;

    public static DustEffect Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject SetDustEffect()
    {
        return dustEffect;
    }
}
