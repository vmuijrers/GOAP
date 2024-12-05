using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    public enum CompareSettings
    {
        FloatCompare,
        StringCompare,
        IntCompare,
        BoolCompare
    }

    public enum CompareSign
    {
        Equals,
        LessThan,
        GreaterThan,
        EqualsNot
    }

    [CreateAssetMenu(fileName = "ConditionAsset", menuName = "Condition", order = 1)]
    public class Condition : ScriptableObject{

        public CompareSettings setting;
        public CompareSign sign;

        public FloatReference floatParameter;
        public BoolReference boolParameter;
        public StringReference stringParameter;
        public IntReference intParameter;

        public FloatReference floatCompareParameter;
        public BoolReference boolCompareParameter;
        public StringReference stringCompareParameter;
        public IntReference intCompareParameter;

        public bool EvaluateCondition(Stats stats)
        {

            switch (setting)
            {
                case CompareSettings.FloatCompare:
                    return Compare(stats.GetStat<Variable_Float>(floatParameter.Variable.name).Value, floatCompareParameter.Value, sign);
                case CompareSettings.IntCompare:
                    return Compare(stats.GetStat<Variable_Int>(intParameter.Variable.name).Value, intCompareParameter.Value, sign);
                case CompareSettings.StringCompare:
                    return Compare(stats.GetStat<Variable_String>(stringParameter.Variable.name).Value, stringCompareParameter.Value, sign);
                case CompareSettings.BoolCompare:
                    return Compare(stats.GetStat<Variable_Bool>(boolParameter.Variable.name).Value, boolCompareParameter.Value, sign);
            }
           
            return false;
        }
        public bool Compare<T>(T v1, T v2, CompareSign sign)
        {
            switch (sign)
            {
                case CompareSign.Equals:
                    return Comparer<T>.Default.Compare(v1, v2) == 0;
                case CompareSign.GreaterThan:
                    return Comparer<T>.Default.Compare(v1, v2) > 0;
                case CompareSign.LessThan:
                    return Comparer<T>.Default.Compare(v1, v2) < 0;
                case CompareSign.EqualsNot:
                    return Comparer<T>.Default.Compare(v1, v2) != 0;
            }
            return false;
        }
    }

    
}
