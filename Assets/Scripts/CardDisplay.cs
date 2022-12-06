using UnityEngine;

public abstract class CardDisplay : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public abstract void GetAsset(int index);
    public abstract void Turn(CardState state);    
    public void PlayFX()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
    public virtual void Set(AudioClip clip)
    {        
        audioSource.clip = clip;
    }
}
