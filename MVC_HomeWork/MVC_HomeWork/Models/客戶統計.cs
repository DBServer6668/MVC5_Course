using MVC_HomeWork.Models.DataTypeAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HomeWork.Models
{
    public class 客戶統計
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public 客戶統計()
        {
            this.客戶銀行資訊 = new HashSet<客戶銀行資訊>();
            this.客戶聯絡人 = new HashSet<客戶聯絡人>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "請填寫客戶名稱")]
        //[StringLength(10, ErrorMessage = "測試中，本系統名稱最長允許10個字元")]
        [CustomStringLengthValidation(10)]
        public string 客戶名稱 { get; set; }

        public int 聯絡人數量 { get; set; }
        public int 銀行帳戶數量 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
