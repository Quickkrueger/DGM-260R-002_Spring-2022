using UnityEngine;

[CreateAssetMenu(fileName = "IntData", menuName = "SO/Data/IntData", order = 1)]
public class IntData : ScriptableObject
{
   public int num;

   public void Add(int numChange)
   {
      numChange += numChange;
   }

   public void Multiply(int numChange)
   {
      numChange *= numChange;
   }
}
