using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement; 

/*Tests swiveling the head of the FBWeapon between two points*/ 
public class SwivelHeadTest
{

    [SetUp]
    public void Setup()
    {
        // load dummy scene
        SceneManager.LoadScene("KTestingScene");
    }
    [UnityTest]
    public IEnumerator TestSwivelAngles()
    {
        // Load or instantiate the FBWeapon prefab
        GameObject fbWeapon = GameObject.FindGameObjectWithTag("FBWeapon");
        if (fbWeapon == null)
        {
            Debug.LogError("FBWeapon not found.");
            yield break;
        }

        // Add SwivelBetweenAngles script to the FBWeapon
        SwivelBetweenAngles swivelScript = fbWeapon.AddComponent<SwivelBetweenAngles>();

        // Set swivel parameters for testing
        float minAngle = 100f;
        float maxAngle = 240f;

        // Wait for a short time for the swivel to start
        yield return new WaitForSeconds(1f);

        // Get the current swivel angle
        float currentAngle = fbWeapon.transform.eulerAngles.z;

        // Verify if the current swivel angle is within acceptable bounds
        Assert.GreaterOrEqual(currentAngle, minAngle, "The swivel angle is below the minimum allowed.");
        Assert.LessOrEqual(currentAngle, maxAngle, "The swivel angle is above the maximum allowed.");

        // Remove the SwivelBetweenAngles script from the FBWeapon to avoid interfering with other tests or gameplay
        GameObject.Destroy(swivelScript);
    }
}