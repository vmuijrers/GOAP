using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Stats", menuName = "Stats/DefaultStats")]
	public class Stats : ScriptableObject {
        [SerializeField] private Variable[] allVariables;
        private Dictionary<string, Variable> allStats = new Dictionary<string, Variable>();
        public void InitState()
        {
            Debug.Log("Started!");
            foreach (Variable sv in allVariables)
            {
                Variable v = sv.Clone();
                AddStatToDicionary(v);
            }
        }

        private void AddStatToDicionary(Variable var)
        {
            if (allStats.ContainsKey(var.name))
            {
                allStats[var.name] = var;
            }
            else
            {
                allStats.Add(var.name, var);
            }
            
        }
        
        public T GetStat<T>(string name) where T : Variable
        {
            if (allStats.ContainsKey(name))
            {
                return (T)allStats[name];
            }
            return null;
        }

        
    }

    
}
