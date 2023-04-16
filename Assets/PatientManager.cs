using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatientManager : Singleton<PatientManager>
{

    public float generateTime = 3;

    private float generateTimer = 0;

    public GameObject patientPrefab;

    public List<Patient> patients;
    
    public BoxCollider2D insideBox;

    private int maxAttempts = 30;
    // Start is called before the first frame update
    void Start()
    {
        generatePatient();
    }

    // void generatePatient()
    // {
    //     
    //     float x = Random.Range(-10f, 10f);
    //     float y = Random.Range(-8f, 8f);
    //
    //     Vector2 randomPoint = new Vector2(x, y);
    //     var go = Instantiate(patientPrefab, randomPoint, quaternion.identity);
    //     patients.Add(go.GetComponent<Patient>());
    // }
    
    public void generatePatient()
    {
        int attempts = 0;
        bool overlaps = true;
        GameObject newObj = null;

        // Instantiate the prefab at the random position
        newObj = Instantiate(patientPrefab, Vector3.zero, Quaternion.identity);
        while (overlaps && attempts < maxAttempts)
        {
            // Generate a random position within the inside box
            Vector2 randomPos = new Vector2(Random.Range(insideBox.bounds.min.x, insideBox.bounds.max.x),
                Random.Range(insideBox.bounds.min.y, insideBox.bounds.max.y));
            newObj.transform.position = randomPos;

            // Check if the new object overlaps with any other boxes
            overlaps = false;


            var playerBox = PlayerSkillManager.Instance.GetComponent<BoxCollider2D>();
            if (newObj.GetComponent<BoxCollider2D>().IsTouching(playerBox))
            {
                overlaps = true;
                continue;
            }
            foreach (var patient in patients)
            {
                var otherBox = patient.GetComponent<BoxCollider2D>();
                if (newObj.GetComponent<BoxCollider2D>().IsTouching(otherBox))
                {
                    overlaps = true;
                    break;
                }
            }

            attempts++;
        }

        if (overlaps)
        {
            Debug.LogWarning("Could not generate object in a valid position after " + maxAttempts + " attempts.");
            Destroy(newObj);
        }
        else
        {
            patients.Add(newObj.GetComponent<Patient>());
        }
    }

    public void removePatient(Patient patient)
    {
        patients.Remove(patient);
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
