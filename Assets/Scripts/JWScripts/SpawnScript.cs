/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public Transform[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        List<int> selectedLocations = new List<int>();

        for (int i = 0; i < Random.Range(0, spawnLocations.Length); i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, spawnLocations.Length);
            } while (selectedLocations.Contains(randomIndex));

            selectedLocations.Add(randomIndex);
            Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], spawnLocations[randomIndex]);
        }
    }


}
*/
using UnityEngine;
using System.Collections.Generic;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public Transform[] spawnLocations;

    /* Pattern: iterator
     * Chose because It perfectly fit the code I needed.
     * Nothing would've worked better, only a bunch of if/for loops
     * A bad time to use the pattern would be the opposite of my scenario, would be useless. 
     */
    HashSet<Transform> usedLocations = new HashSet<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        int numberOfObjectsToSpawn = Random.Range(2, 5); // Random number between 2 and 4
        SpawnObjects(numberOfObjectsToSpawn);
    }

    void SpawnObjects(int numberOfObjects)
    {
        List<Transform> availableLocations = GetAvailableLocations();

        // Ensure we don't spawn more objects than available locations
        numberOfObjects = Mathf.Min(numberOfObjects, availableLocations.Count);

        for (int i = 0; i < numberOfObjects; i++)
        {
            int randomIndex = Random.Range(0, spawnObjects.Length);
            int randomLocationIndex = Random.Range(0, availableLocations.Count);

            Transform location = availableLocations[randomLocationIndex];
            Instantiate(spawnObjects[randomIndex], location);
            usedLocations.Add(location);
            availableLocations.RemoveAt(randomLocationIndex); // Remove the used location
        }
    }
    // THis code was taken from a book I found online, this violated copyright
    List<Transform> GetAvailableLocations()
    {
        List<Transform> availableLocations = new List<Transform>();

        foreach (var location in spawnLocations)
        {
            if (!usedLocations.Contains(location))
            {
                availableLocations.Add(location);
            }
        }

        return availableLocations;
    }
}
