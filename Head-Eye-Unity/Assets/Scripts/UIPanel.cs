using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    private Button _createSphereButton;
    private Button _createCubeButton;
    private Button _destroyAllButton;
    private GameObject _createdObjectsContainer;
    
    void Start()
    {
        _createCubeButton = GameObject.Find("CreateCube").GetComponent<Button>();
        _createSphereButton = GameObject.Find("CreateSphere").GetComponent<Button>();
        _destroyAllButton = GameObject.Find("DestroyAll").GetComponent<Button>();

        _createdObjectsContainer = GameObject.Find("CreatedObjects");
        
        _createCubeButton.onClick.AddListener(CreateCube);
        _createSphereButton.onClick.AddListener(CreateSphere);
        _destroyAllButton.onClick.AddListener(DestroyAll);
    }

    private void DestroyAll()
    {
        Destroy(_createdObjectsContainer);
        _createdObjectsContainer = new GameObject("CreatedObjects");
        _createdObjectsContainer.transform.SetParent(GameObject.Find("UI").transform);
    }

    private void CreateCube()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(_createdObjectsContainer.transform);
        cube.transform.position = new Vector3(CenteredRange(1.3f, 0.5f), 1f, 3f);
        cube.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        var cubeRigidbody = cube.AddComponent<Rigidbody>();
        cubeRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Debug.Log("Created a cube");
    }
    
    private void CreateSphere()
    {
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.SetParent(_createdObjectsContainer.transform);
        sphere.transform.position = new Vector3(CenteredRange(-1.3f, 0.5f), 1f, 3f);
        sphere.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        var sphereRigidbody = sphere.AddComponent<Rigidbody>();
        sphereRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Debug.Log("Created a sphere");
    }

    private float CenteredRange(float center, float radius)
    {
        return Random.Range(center - radius, center + radius);
    }
}
