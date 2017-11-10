namespace Course_Example.Models
{
    using DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ProductName.Contains("台灣") && this.Stock <= 0)
            {
                yield return new ValidationResult(
                    "台灣只能有一個",
                    new string[] { "ProductName", "Stock" });
            }
        }
    }

    public partial class ProductMetaData
    {

        public int ProductId { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        [ValidateTaiwanAddress]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
