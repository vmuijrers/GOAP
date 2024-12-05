using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{

    public abstract class Variable : ScriptableObject
    {
        public Variable Clone()
        {
            return (Variable)MemberwiseClone();
        }
    }

    /// <summary>
    /// References
    /// </summary>
    /// 
    [System.Serializable]
    public class BaseReference
    {

    }

    [System.Serializable]
    public class FloatReference : BaseReference
    {
        public bool UseConstant = false;
        public float ConstantValue;
        public Variable_Float Variable;

        public FloatReference()
        { }

        public FloatReference(float value)
        {
            UseConstant = false;
            ConstantValue = value;
        }

        public float Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set { if (UseConstant) { ConstantValue = value; } else { Variable.SetValue(value); } }
        }

        public static implicit operator float (FloatReference reference)
        {
            return reference.Value;
        }
    }

    [System.Serializable]
    public class BoolReference : BaseReference
    {
        public bool UseConstant = false;
        public bool ConstantValue;
        public Variable_Bool Variable;

        public BoolReference()
        { }

        public BoolReference(bool value)
        {
            UseConstant = false;
            ConstantValue = value;
            
        }

        public bool Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set { if (UseConstant) { ConstantValue = value; } else { Variable.SetValue(value); } }
        }
        public static implicit operator bool (BoolReference reference)
        {
            return reference.Value;
        }
    }

    [System.Serializable]
    public class StringReference : BaseReference
    {
        public bool UseConstant = false;
        public string ConstantValue;
        public Variable_String Variable;

        public StringReference()
        { }

        public StringReference(string value)
        {
            UseConstant = false;
            ConstantValue = value;
        }

        public string Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set { if (UseConstant) { ConstantValue = value; } else { Variable.SetValue(value); } }
        }

        public static implicit operator string (StringReference reference)
        {
            return reference.Value;
        }
    }

    [System.Serializable]
    public class IntReference : BaseReference
    {
        public bool UseConstant = false;
        public int ConstantValue;
        public Variable_Int Variable;

        public IntReference()
        { }

        public IntReference(int value)
        {
            UseConstant = false;
            ConstantValue = value;
        }

        public int Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set { if (UseConstant) { ConstantValue = value; } else { Variable.SetValue(value); } }
        }

        public static implicit operator int (IntReference reference)
        {
            return reference.Value;
        }
    }


   
}
