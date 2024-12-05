using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Var_StringValue", menuName = "StatsVariables/StringValue")]
    public class Variable_String : Variable
    {
        [SerializeField]
        private string value;
        public string Value { get { return value; } }
        public void SetValue(string value)
        {
            this.value = value;
        }

        public void SetValue(Variable_String value)
        {
            this.value = value.value;
        }

        public void AddText(string amount)
        {
            this.value += amount;
        }

        public void AddText(Variable_String amount)
        {
            this.value += amount.value;
        }
    }
}
