using UnityEngine;
public class darcDestroy : MonoBehaviour
{
    [SerializeField] float destroyAfterTime = 2f;

    void Awake() => Destroy(gameObject, destroyAfterTime);
}
