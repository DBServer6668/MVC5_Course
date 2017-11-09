//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class 客戶聯絡人
    {
        public int Id { get; set; }
        public int 客戶Id { get; set; }
        [Required(ErrorMessage = "請填寫聯絡人職稱")]
        [StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
        public string 職稱 { get; set; }
        [Required(ErrorMessage = "請填寫聯絡人姓名")]
        [StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
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

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
