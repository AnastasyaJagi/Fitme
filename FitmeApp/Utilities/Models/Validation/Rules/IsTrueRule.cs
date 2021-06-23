using System;
using FitmeApp.Helper;

namespace FitmeApp.Utilities.Models.Validation.Rules
{
    public class IsTrueRule : ValidationRule
    {
        public IsTrueRule() { }

        public override bool Check(object obj)
        {
            if (obj != null)
            {
                SerializationHelper.GetPropertyValue(obj, PropertyPath, out object propertyValue);

                if (propertyValue == null || (propertyValue is bool boolean && boolean))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
