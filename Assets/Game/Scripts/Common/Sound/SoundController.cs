using UnityEngine;

public class SoundController : MonoBehaviour, IService
{
    public bool CanDestroyed => false;
    
    
    
    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);
    }
}
