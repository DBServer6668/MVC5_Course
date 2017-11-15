namespace MVC_HomeWork.Models
{
    using DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料
    {
    }

    public partial class 客戶資料MetaData
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "請填寫客戶名稱")]
        //[StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
        [CustomStringLengthValidation(5)]
        public string 客戶名稱 { get; set; }

        [Required(ErrorMessage = "請填寫統一編號")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "測試中，統一編號應為八碼數字")]
        public string 統一編號 { get; set; }

        [Required(ErrorMessage = "請填寫電話號碼")]
        [RegularExpression(@"^0\d{1}-?\d{8}$", ErrorMessage = "測試中，電話號碼應為02-23517988等正確格式")]
        public string 電話 { get; set; }

        [Required(ErrorMessage = "請填寫傳真號碼")]
        [RegularExpression(@"^0\d{1}-?\d{8}$", ErrorMessage = "測試中，傳真號碼應為02-23517988等正確格式")]
        public string 傳真 { get; set; }

        [Required(ErrorMessage = "請填寫地址")]
        //[StringLength(30, ErrorMessage = "測試中，本系統最長允許30個字元")]
        [CustomStringLengthValidation(30)]
        public string 地址 { get; set; }

        [Required(ErrorMessage = "請填寫Email")]
        [EmailAddressAttribute(ErrorMessage = "測試中，Email格式應符合sales.24drs@24drs.com等正確格式")]
        public string Email { get; set; }

        public string 客戶分類 { get; set; }
        public Nullable<bool> 是否已刪除 { get; set; }

        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
