using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities;
 
     public  class Mobile : CommonClass
    {
    public Mobile()
    {
         this.MobileImages = new List<MobileImage>();
    }
    public string Processor { get; set; }
    public string Storage { get; set; }
    public string BatteryMah { get; set; }
    public string Ram { get; set; }
    [Column(TypeName = "date")] 
    public DateTime? LaunchDate { get; set; }
    public string MobileWeight { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public decimal ScreenSize { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string ScreenType { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Charger { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Resolution { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string HeadPhoneJack { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Bluetooth { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Wifi { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string USBConnector { get; set; }
 

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Camera { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Description { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string  Weight { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public int Stock { get; set; }
     

  

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string CPU { get; set; }
    public int Sell { get; set; } 
    public int MobilePrice { get; set; } 
    public bool StockAvailiability { get; set; }
    public int Quantity { get; set; }

    [ForeignKey("Brand")]
    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; }

    [ForeignKey("OSVersion")]
    public int OSVersionId { get; set; }
    public virtual OSVersion OSVersion { get; set; }

    [ForeignKey("Color")]
    public int ColorId { get; set; }
    public virtual Color Color { get; set; }
    public virtual ICollection<MobileImage>  MobileImages { get; set; }
}
