using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TestButtons: InputTestFixture
{
    [UnityTest]
    public IEnumerator TestButtonsWithEnumeratorPasses()
    {
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        
        SceneManager.LoadScene("Assets/Example2.unity", LoadSceneMode.Additive);
        string scoreTextLocator = "/Canvas/ScoreText";
        string addButtonLocator = "/Canvas/Add";
        string resetButtonLocator = "/Canvas/Reset";
        
        yield return new WaitForSeconds(2f);
        GameObject addButton = GameObject.Find(addButtonLocator);
        GameObject resetButton = GameObject.Find(resetButtonLocator);
        
        yield return ClickUni(addButton);
        Assert.AreEqual(GetText(scoreTextLocator), "1");
        
        yield return ClickUni(addButton);
        Assert.AreEqual(GetText(scoreTextLocator), "2");

        yield return ClickUni(resetButton);
        Assert.AreEqual(GetText(scoreTextLocator), "0");
    }
    
    public IEnumerator ClickUni(GameObject button)
    {
        PointerDown(button);
        yield return new WaitForSeconds(1f);
        PointerUp(button);
        yield return new WaitForSeconds(0.01f);
    }
    
    public void PointerDown(GameObject button)
    {
        Vector2 buttonPosition = new Vector2(button.transform.position.x, button.transform.position.y);
        var eventData = new PointerEventData(EventSystem.current)
        {
            pointerId = 0,
            position = buttonPosition 
        };
        ExecuteEvents.Execute(button, eventData, ExecuteEvents.pointerDownHandler);
    }
    
    public void PointerUp(GameObject button)
    {
        Vector2 buttonPosition = new Vector2(button.transform.position.x, button.transform.position.y);
        var eventData = new PointerEventData(EventSystem.current)
        {
            pointerId = 0,
            position = buttonPosition 
        };
        ExecuteEvents.Execute(button, eventData, ExecuteEvents.pointerUpHandler);
    }
    
    public string GetText(string locator)
    {
        return GameObject.Find(locator).GetComponent<Text>().text;
    }
}
