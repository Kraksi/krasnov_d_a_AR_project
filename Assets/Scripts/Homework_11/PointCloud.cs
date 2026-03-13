using UnityEngine;

public class PointCloud : MonoBehaviour
{
    public int PointCount = 10000;
    public float Radius = 5f;
    public Color PointColor = Color.cyan;
    public float PointSize = 0.05f;

    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps == null)
        {
            Debug.LogError("Нет ParticleSystem!");
            return;
        }

        var main = ps.main;
        main.startLifetime = 999999f;
        main.startSpeed = 0f;
        main.startSize = PointSize;
        main.startColor = PointColor;
        main.maxParticles = PointCount;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.loop = false;
        main.playOnAwake = false;

        var emission = ps.emission;
        emission.enabled = false;

        var particles = new ParticleSystem.Particle[PointCount];
        for (int i = 0; i < PointCount; i++)
        {
            particles[i].position = transform.position +
                                    Random.insideUnitSphere * Radius;
            particles[i].startColor = PointColor;
            particles[i].startSize = PointSize;
            particles[i].remainingLifetime = 999999f;
        }

        ps.SetParticles(particles, PointCount);
        Debug.Log($"PointCloud создан: {PointCount} точек");

        ParticleSystem.Particle[] check = new ParticleSystem.Particle[PointCount];
        int count = ps.GetParticles(check);
        Debug.Log($"Частиц в системе: {count}");
    }
}