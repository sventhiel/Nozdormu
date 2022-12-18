using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nozdormu.Library.Attributes
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value.GetType() == typeof(ICollection))
            {
                var collection = (ICollection)value;
                return collection.Count > 0;
            }

            return false;
        }
    }
}
