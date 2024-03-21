using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


public class EnemyStressTest
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("TestingScene");
    }
    
    //Test will spawn enemies until the game breaks
    [UnityTest]
    public IEnumerator EnemySpawnTest()
    {
        GameObject enemyObj = new GameObject("Treevle", typeof(BoxCollider2D), typeof(SpriteRenderer));
        int i = 0;
        
        while (i < int.MaxValue){
            GameObject.Instantiate(enemyObj); 
            i++;
            Debug.Log(i + " instances of EnemyObj created.");
            yield return null;
        }
        yield return null; 

       
        Assert.True(false); 
    }

    [TearDown]
    public void Teardown()
    {
    }
    }
