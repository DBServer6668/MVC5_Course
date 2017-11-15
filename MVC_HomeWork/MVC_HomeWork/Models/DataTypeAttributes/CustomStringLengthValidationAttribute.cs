using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HomeWork.Models.DataTypeAttributes
{
    public class CustomStringLengthValidationAttribute : ValidationAttribute
    {
        private int num;

        public CustomStringLengthValidationAttribute(int num) 
        {
            this.num = num ;
            
            ErrorMessage = "測試中，本系統名稱最長允許10個字元";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString().Length < num)
            {
                return ValidationResult.Success;
            }
            else {
                var errorMsg = string.Format("測試中，本系統名稱最長允許{0}個字元，您目前輸入{1}個字元", num, value.ToString().Length);
                return new ValidationResult(errorMsg);
            }
        }
    }
}