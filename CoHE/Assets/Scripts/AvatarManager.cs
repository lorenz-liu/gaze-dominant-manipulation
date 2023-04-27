using UnityEngine;

internal class AvatarManager : MonoBehaviour
{
    public GameObject avatar;
    public Configuration configuration;
    private void Awake()
    {
        avatar.SetActive(configuration.showAvatar);
    }
}