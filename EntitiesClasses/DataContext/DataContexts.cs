


namespace EntitiesClasses.DataContext;
   public  class DataContexts : DbContext
{
    public DataContexts(DbContextOptions<DataContexts> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<OperatingSystems> OperatingSystems { get; set; }
    public DbSet<Notification>  Notifications { get; set; }
    public DbSet<OSVersion> OSVersions { get; set; }
    public DbSet<PdfDocument> PdfDocuments { get; set; }
    public DbSet<PdfDocumentImage> PdfDocumentImages { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<PDF> PDFs { get; set; }    
    public DbSet<Message>  Messages { get; set; }
    public DbSet<Mobile> Mobiles { get; set; }
    public DbSet<MobileImage > MobileImages { get; set; }   
    public DbSet<Color> Colors { get; set; }
    public DbSet<ColorProject> ColorProjects { get; set; }
    public DbSet<Carousel> Carousels { get; set; }
    public DbSet<User> Users { get; set; }
     public DbSet<EmailVerificationCode> EmailVerificationCodes { get; set; }
    //public DbSet<Order>  Orders { get; set; }
    //public DbSet<OrderDetail> OrderDetail { get; set; }
    public DbSet<UserTypes> UserTypes { get; set; }
    public DbSet<PaymentCard> PaymentCards { get; set; }
    //public DbSet<OrderPayment> OrderPayments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        modelBuilder.Entity<Brand>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<Mobile>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<MobileImage>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<User>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
 

        modelBuilder.Entity<Brand>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<OperatingSystems>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<OSVersion>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));

        base.OnModelCreating(modelBuilder);
    }




}
 
