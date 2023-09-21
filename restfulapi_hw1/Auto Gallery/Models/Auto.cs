using System;
using System.ComponentModel.DataAnnotations;

namespace Auto_Gallery.Models;

public class Auto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Status field is mandatory.")]
    public bool Status { get; set; } // 1 --> active for sale, 0 --> passive
    [Required(ErrorMessage = "Brand field is mandatory.")]
    public string Brand { get; set; }
    [Required(ErrorMessage = "Model field is mandatory.")]
    public string Model { get; set; }
    [Required(ErrorMessage = "ModelYear field is mandatory.")]
    public int ModelYear { get; set; }
    [Required(ErrorMessage = "RecordDate field is mandatory.")]
    public DateTime RecordDate { get; set; }
    public DateTime? SoldDate { get; set; }
    [Required(ErrorMessage = "Price field is mandatory.")]
    public int Price { get; set; }
}
