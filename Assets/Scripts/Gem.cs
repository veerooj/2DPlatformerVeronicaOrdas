using System;
using UnityEngine;

public class Gem : MonoBehaviour, Item

{
  public static event Action<int> OnGemCollect;
  public int score = 5;
  
  public void Collect()
  {
    OnGemCollect.Invoke(score);
    Destroy(gameObject);
  }
}
