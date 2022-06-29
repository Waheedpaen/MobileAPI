

namespace EntitiesClasses.Entities;
    public  class OSVersion : CommonClass
{
    [ForeignKey("OperatingSystems")]
    public int  OperatingSystemId { get; set; }
    public virtual OperatingSystems OperatingSystems { get; set; }
}

