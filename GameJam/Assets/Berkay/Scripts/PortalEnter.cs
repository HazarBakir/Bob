using TheGame.SceneManagement;
using TheGame.ScriptableObjects.Channels;
using TheGame.ScriptableObjects.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Berkay.Scripts
{
    public class PortalEnter : MonoBehaviour
    {
        [SerializeField] SceneListSO sceneListSO;
        [SerializeField] SceneLoadChannelSO sceneLoadChannel;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagConstants.Player))
            {
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

    }
}
