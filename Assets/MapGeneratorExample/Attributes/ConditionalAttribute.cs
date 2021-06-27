using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalAttribute : PropertyAttribute
{
    public string validation { get; private set; }

    public ConditionalAttribute(string validation)
    {
        this.validation = validation;
    }
}
