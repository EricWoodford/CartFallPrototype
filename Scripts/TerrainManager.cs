using UnityEngine;

public enum SurfaceType
{
    Default,
    Grass,
    Dirt,
    Pavement,
    Mud,
    Snow
}

public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance;

    [System.Serializable]
    public class SurfaceConfig
    {
        public SurfaceType type;
        public PhysicMaterial physicMaterial;
        public float frictionMultiplier = 1f;
    }

    public SurfaceConfig[] surfaces;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public SurfaceConfig GetConfig(SurfaceType type)
    {
        foreach (var s in surfaces)
        {
            if (s.type == type) return s;
        }
        return null;
    }
}
