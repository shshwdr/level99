using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatientManager : MonoBehaviour
{

    public float generateTime = 3;

    private float generateTimer = 0;

    public GameObject patientPrefab;

    public List<Patient> patients;
    // Start is called before the first frame update
    void Start()
    {
        generatePatient();
    }

    void generatePatient()
    {
        
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-8f, 8f);

        Vector2 randomPoint = new Vector2(x, y);
        var go = Instantiate(patientPrefab, randomPoint, quaternion.identity);
        patients.Add(go.GetComponent<Patient>());
    }
    // Update is called once per frame
    void Update()
    {
        generateTimer += Time.deltaTime;
        if (generateTimer >= generateTime)
        {
            generatePatient();
            generateTimer = 0;
        }
    }
}
