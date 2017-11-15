namespace MVC_HomeWork.Models
{
    using DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶銀行資訊MetaData))]
    public partial class 客戶銀行資訊
    {
    }

    public partial class 客戶銀行資訊MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [Required(ErrorMessage = "請填寫銀行名稱")]
        //[StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
        [CustomStringLengthValidation(10)]
        public string 銀行名稱 { get; set; }
        [Required]
        public int 銀行代碼 { get; set; }
        public Nullable<int> 分行代碼 { get; set; }

        [Required(ErrorMessage = "請填寫帳戶名稱")]
        //[StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
        [CustomStringLengthValidation(5)]
        public string 帳戶名稱 { get; set; }

        [Required(ErrorMessage = "請填寫帳戶號碼")]
        [RegularExpression(@"^[0-9]{12}$", ErrorMessage = "測試中，帳戶號碼應為十二碼數字")]
        public string 帳戶號碼 { get; set; }
        public Nullable<bool> 是否已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
