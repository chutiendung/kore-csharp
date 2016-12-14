﻿// ***********************************************************************
// <copyright file="RequiredWhenAttribute.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;
using KoreAsp.Attributes.Validation.Resource;

namespace KoreAsp.Attributes.Validation
{
    /// <summary>
    /// Validates that the annotated property is specified when some other property is a defined value.
    /// </summary>
    /// <seealso cref="KoreAsp.Attributes.Validation.DualPropertyAttributeBase" />
    public class RequiredWhenAttribute : DualPropertyAttributeBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredWhenAttribute" /> class.
        /// </summary>
        public RequiredWhenAttribute()
            : base("RequiredWhen")
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Returns true if this validator is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance != null)
            {
                PropertyInfo otherPropInfo = validationContext.ObjectType.GetProperty(this.OtherPropertyName);
                if (otherPropInfo != null)
                {
                    object otherPropValue = otherPropInfo.GetValue(validationContext.ObjectInstance);
                    if ((otherPropValue == null && this.OtherPropertyValue == null) ||
                        otherPropInfo.GetValue(validationContext.ObjectInstance).Equals(this.OtherPropertyValue))
                    {
                        if (value == null || 
                            string.IsNullOrEmpty(value.ToString()) ||
                            value.ToString() == false.ToString() ||
                            value.ToString() == 0.ToString())
                        {
                            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
