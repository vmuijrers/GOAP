using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Var_IntValue", menuName = "StatsVariables/IntValue")]
    public class Variable_Int : Variable
    {
        [SerializeField]
        private int value;
        public int Value { get { return value; } }
        public void SetValue(int value)
        {
            this.value = value;
        }

        public void SetValue(Variable_Int value)
        {
            this.value = value.value;
        }

        public void ApplyChange(int amount)
        {
            this.value += amount;
        }

        public void ApplyChange(Variable_Int amount)
        {
            this.value += amount.value;
        }
    }
}
