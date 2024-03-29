﻿


using EntitiesClasses.CommonClasses;

namespace ViewModel.ViewModels.MobileViewModel;
     public class MobileDtoForSave : CommonClass
    {
    public MobileDtoForSave()
    {
        this.MobileImages = new List<MobileImageDtoForSave>();
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
    public string ScreenSize { get; set; }
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
    public string Weight { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public int Stock { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public double DisplaySize { get; set; }

 

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string CPU { get; set; }
    public int  Sell { get; set; }
    public int? MobilePrice { get; set; }
    public bool StockAvailiability { get; set; }
    public int Quantity { get; set; }
    public int ? BrandId { get; set; }
    public int ? OSVersionId { get; set; }
    public int ? ColorId { get; set; }
    public virtual List<MobileImageDtoForSave> MobileImages { get; set; }
}
