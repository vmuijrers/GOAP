using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Var_FloatValue", menuName = "StatsVariables/FloatValue")]
    public class Variable_Float : Variable
    {
        [SerializeField]
        private float value;
        public float Value { get { return value; } }

        public void SetValue(float value)
        {
            this.value = value;
        }

        public void SetValue(Variable_Float value)
        {
            this.value = value.value;
        }

        public void ApplyChange(float amount)
        {
            this.value += amount;
        }

        public void ApplyChange(Variable_Float amount)
        {
            this.value += amount.value;
        }
    }
}
