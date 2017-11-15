namespace MVC_HomeWork.Models
{
    using DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (db.客戶聯絡人.Any(P => P.Id != this.Id && P.客戶Id == this.客戶Id && P.姓名 == this.姓名))
            {
                yield return new ValidationResult("測試中，本系統聯絡人姓名不可重複", new string[] { "姓名" });
            }

            if (db.客戶聯絡人.Any(P => P.Id != this.Id && P.客戶Id == this.客戶Id && P.Email == this.Email))
            {
                yield return new ValidationResult("測試中，本系統聯絡人Email不可重複", new string[] { "Email" });
            }
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [Required(ErrorMessage = "請填寫聯絡人職稱")]
        //[StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
        [CustomStringLengthValidation(10)]
        public string 職稱 { get; set; }

        [Required(ErrorMessage = "請填寫聯絡人姓名")]
        //[StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
        [CustomStringLengthValidation(5)]
        public string 姓名 { get; set; }

        [Required(ErrorMessage = "請填寫Email")]
        [EmailAddressAttribute(ErrorMessage = "測試中，Email格式應符合sales.24drs@24drs.com等正確格式")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請填寫手機號碼")]
        [RegularExpression(@"^09\d{2}-?\d{6}$", ErrorMessage = "測試中，手機號碼應為0975-111111等正確格式")]
        public string 手機 { get; set; }

        [Required(ErrorMessage = "請填寫電話號碼")]
        [RegularExpression(@"^0\d{1}-?\d{8}$", ErrorMessage = "測試中，電話號碼應為02-23517988")]
        public string 電話 { get; set; }
        public Nullable<bool> 是否已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
