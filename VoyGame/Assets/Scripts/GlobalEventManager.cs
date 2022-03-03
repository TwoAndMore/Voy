using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent OnBiblePut = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();

    public static void SendBiblePut() => 
        OnBiblePut.Invoke();

    public static void SendGameOver() => 
        OnGameOver.Invoke();
}
