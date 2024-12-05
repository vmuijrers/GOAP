using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Var_BoolValue", menuName = "StatsVariables/BoolValue")]
    public class Variable_Bool : Variable
    {
        [SerializeField]
        private bool value;
        public bool Value { get { return value; } }

        public void SetValue(bool value)
        {
            this.value = value;
        }

        public void SetValue(Variable_Bool value)
        {
            this.value = value.value;
        }

    }
}
