using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class Flashlight : MonoBehaviour {
    public float lightRadius = 10;                                      // Raio de iluminação
    [Range(0, 360)]
    public float lightAngle = 45;                                       // Ângulo de iluminação
    Vector2 lightDirection = Vector2.down;

    public LayerMask enemyMask;                                         // Máscara que contém os inimigos
    public LayerMask obstacleMask;                                      // Máscara que contém os obstáculos da luz

    //[HideInInspector]
    public List<Transform> illuminatedEnemies = new List<Transform>();  // Lista com os inimigos iluminados

    public float meshResolution = 1;                                    // Taxa de raycasts utilizado na geração da luz
    public int edgeResolveIterations = 4;                               // Quantidade de interações para encontrar a borda de um objeto
    public float edgeDistanceThreshold = 0.5f;                          // Tolerância para considerar como outro objeto

    Mesh lightMesh;                                                     // Mesh da luz

    public bool enableFlashlight = true;                                // A lanterna está habilitada para uso?

    void Start () {
        lightMesh = new Mesh();
        lightMesh.name = "Light Mesh";
        transform.GetComponent<MeshFilter>().mesh = lightMesh;
        RotateLight(Vector2.down);

        StartCoroutine("FindEnemiesWithDelay", .2f);
    }

    // Busca os inimigos a cada [delay] segundos
    IEnumerator FindEnemiesWithDelay (float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);

            if(GameManager.instance.flashlightOn)
                FindIlluminatedEnemies();
        }
    }

    void Update () {
        if (Input.GetButtonDown("Flashlight") && enableFlashlight) {
            GameManager.instance.UpdateFlashlight();
            transform.parent.GetComponent<SpriteRenderer>().sortingOrder = GameManager.instance.flashlightOn ? 5 : 4;
        }
    }

    void LateUpdate () {
        lightMesh.Clear();

        if (GameManager.instance.flashlightOn)
            DrawLight();
    }

    // Busca os inimigos que estão sendo iluminados
    void FindIlluminatedEnemies() {
        illuminatedEnemies.Clear();
        Collider2D[] enemiesInLightRadius = Physics2D.OverlapCircleAll(transform.position, lightRadius, enemyMask);

        foreach (Collider2D col in enemiesInLightRadius)
        {
            Transform enemy = col.transform;
            Vector2 directionToEnemy = (enemy.position - transform.position).normalized;

            if (Vector2.Angle(lightDirection, directionToEnemy) < lightAngle / 2)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.position);

                if (!Physics2D.Raycast(transform.position, directionToEnemy, distanceToEnemy, obstacleMask))
                    illuminatedEnemies.Add(enemy);
            }
        }
    }

    // Gera a mesh da luz
    void DrawLight () {
        int stepCount = Mathf.RoundToInt(lightAngle * meshResolution);
        float stepAngleSize = lightAngle / stepCount;
        List<Vector3> lightPoints = new List<Vector3>();
        LightCastInfo oldLightCast = new LightCastInfo();

        for (int i = 0; i <= stepCount; i++) {
            float angle = transform.eulerAngles.z - lightAngle / 2 + stepAngleSize * i;
            LightCastInfo newLightCast = LightCast(angle);

            if (i > 0) {
                bool edgeDistanceThresholdExceeded = Mathf.Abs(oldLightCast.distance - newLightCast.distance) > edgeDistanceThreshold;

                if (oldLightCast.hit != newLightCast.hit || (oldLightCast.hit && newLightCast.hit && edgeDistanceThresholdExceeded)) {
                    EdgeInfo edge = FindEdge(oldLightCast, newLightCast);
                    if (edge.pointA != Vector3.zero)
                        lightPoints.Add(edge.pointA);

                    if (edge.pointB != Vector3.zero)
                        lightPoints.Add(edge.pointB);
                }
            }

            lightPoints.Add(newLightCast.point);
            oldLightCast = newLightCast;
        }

        int vertexCount = lightPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++) {
            vertices[i + 1] = transform.InverseTransformPoint(lightPoints[i]);

            if (i < vertexCount - 2) {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        lightMesh.vertices = vertices;
        lightMesh.triangles = triangles;
        lightMesh.RecalculateNormals();
    }

    // Encontra a borda de um objeto
    EdgeInfo FindEdge (LightCastInfo minLightCast, LightCastInfo maxLightCast) {
        float minAngle = minLightCast.angle;
        float maxAngle = maxLightCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++) {
            float angle = (minAngle + maxAngle) / 2;
            LightCastInfo newLightCast = LightCast(angle);

            bool edgeDistanceThresholdExceeded = Mathf.Abs(minLightCast.distance - newLightCast.distance) > edgeDistanceThreshold;
            if (newLightCast.hit == minLightCast.hit && !edgeDistanceThresholdExceeded) {
                minAngle = angle;
                minPoint = newLightCast.point;
            } else {
                maxAngle = angle;
                maxPoint = newLightCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    // Gera um raio de luz no ângulo dado
    LightCastInfo LightCast (float globalAngle) {
        Vector2 direction = DirectionFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, lightRadius, obstacleMask);

        if (hit.collider != null)
            return new LightCastInfo(true, hit.point, hit.distance, globalAngle);
        else
            return new LightCastInfo(false, (Vector2)transform.position + direction * lightRadius, lightRadius, globalAngle);
    }

    // Retorna a direção para qual um ângulo aponta
    public Vector2 DirectionFromAngle (float angleInDegrees, bool angleIsGlobal) {
        // Se o ângulo dado não for global, converte ele.
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // Rotaciona a luz corretamente para acompanhar o personagem
    public void RotateLight (Vector2 direction) {
        if (direction == Vector2.up) {
            transform.localPosition = new Vector2(0.26f, -0.16f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (direction == Vector2.right) {
            transform.localPosition = new Vector2(0.2f, -0.16f);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        } else if (direction == Vector2.left) {
            transform.localPosition = new Vector2(-0.41f, -0.14f);
            transform.rotation = Quaternion.Euler(0, 0, -90);
        } else if (direction == Vector2.down) {
            transform.localPosition = new Vector2(-0.1f, -0.17f);
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        lightDirection = direction;
    }

    // Informações de um raio de luz
    public struct LightCastInfo {
        public bool hit;        // Atingiu algum objeto?
        public Vector3 point;   // Ponto que atingiu o objeto
        public float distance;  // Distância até o objeto
        public float angle;     // Ângulo formado com o objeto

        public LightCastInfo(bool _hit, Vector3 _point, float _dst, float _angle) {
            hit = _hit;
            point = _point;
            distance = _dst;
            angle = _angle;
        }
    }

    // Informações de uma borda
    public struct EdgeInfo {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
