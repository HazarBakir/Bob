using UnityEngine;

public class GlassReplacement : MonoBehaviour
{
    [SerializeField] private GameObject brokenGlassPrefab;
    [SerializeField] private float scatterForce = 3f;

    [SerializeField] private Vector3[] scatterDirections;

    private void Start()
    {
        CalculateScatterDirections();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Drone")) // You can adjust the tag or use another condition.
        {
            ReplaceGlass();
        }
    }

    private void ReplaceGlass()
    {
        // Disable the renderer and collider of the intact glass.
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Instantiate broken glass prefab at the same position as intact glass.
        GameObject brokenGlass = Instantiate(brokenGlassPrefab, transform.position, transform.rotation);

        // Get all rigidbodies from the broken glass prefab.
        Rigidbody[] rigidbodies = brokenGlass.GetComponentsInChildren<Rigidbody>();

        // Apply precalculated force directions to each rigidbody.
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].AddForce(scatterDirections[i] * scatterForce, ForceMode.Impulse);
        }
    }

    private void CalculateScatterDirections()
    {
        // Get the center of the broken glass.
        Vector3 center = new Vector3(0, 0, 0);

        // Get all rigidbodies from the broken glass prefab.
        Rigidbody[] rigidbodies = brokenGlassPrefab.GetComponentsInChildren<Rigidbody>();
        Transform[] transforms = brokenGlassPrefab.GetComponentsInChildren<Transform>();
        // Precalculate scatter directions based on local positions.
        scatterDirections = new Vector3[rigidbodies.Length];
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            Vector3 localPosition = brokenGlassPrefab.transform.InverseTransformPoint(transforms[i].localPosition);
            localPosition.x = -1f;
            scatterDirections[i] = localPosition;
        }
    }
}