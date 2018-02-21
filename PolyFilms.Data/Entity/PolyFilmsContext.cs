using Microsoft.EntityFrameworkCore;
using PolyFilms.Data.CustomModel;

namespace PolyFilms.Data.Entity
{
    public partial class PolyFilmsContext : DbContext
    {
        public PolyFilmsContext(DbContextOptions<PolyFilmsContext> options)
            : base(options)
        { }

        public virtual DbSet<TblAudit> TblAudit { get; set; }
        public virtual DbSet<TblAuditSettings> TblAuditSettings { get; set; }
        public virtual DbSet<TblConsignee> TblConsignee { get; set; }
        public virtual DbSet<TblCore> TblCore { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblDispatch> TblDispatch { get; set; }
        public virtual DbSet<TblDispatchDetail> TblDispatchDetail { get; set; }
        public virtual DbSet<TblInvoice> TblInvoice { get; set; }
        public virtual DbSet<TblInvoiceDetail> TblInvoiceDetail { get; set; }
        public virtual DbSet<TblJumbo> TblJumbo { get; set; }
        public virtual DbSet<TblJumboStatus> TblJumboStatus { get; set; }
        public virtual DbSet<TblMenu> TblMenu { get; set; }
        public virtual DbSet<TblOrder> TblOrder { get; set; }
        public virtual DbSet<TblOrderDetail> TblOrderDetail { get; set; }
        public virtual DbSet<TblOrderStatus> TblOrderStatus { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblRoleMenuMap> TblRoleMenuMap { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblSetting> TblSetting { get; set; }
        public virtual DbSet<TblShiftType> TblShiftType { get; set; }
        public virtual DbSet<TblSlitting> TblSlitting { get; set; }
        public virtual DbSet<TblSlittingStatus> TblSlittingStatus { get; set; }
        public virtual DbSet<TblTreatment> TblTreatment { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
      
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAudit>(entity =>
            {
                entity.ToTable("tblAudit");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.KeyValues)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewValues).IsUnicode(false);

                entity.Property(e => e.OldValues).IsUnicode(false);

                entity.Property(e => e.Changes).IsUnicode(false);
                
                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAuditSettings>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.ToTable("tblAuditSettings");

                entity.Property(e => e.ScreenName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblConsignee>(entity =>
            {
                entity.HasKey(e => e.ConsigneeId);

                entity.ToTable("tblConsignee");

                entity.Property(e => e.DeliveryAddress)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gstnumber)
                    .IsRequired()
                    .HasColumnName("GSTNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PanNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.TblConsignee)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblConsignee_tblCustomer");
            });

            modelBuilder.Entity<TblCore>(entity =>
            {
                entity.HasKey(e => e.CoreId);

                entity.ToTable("tblCore");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Thickness).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 4)");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("tblCustomer");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddress).IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gstnumber)
                    .IsRequired()
                    .HasColumnName("GSTNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PanNumber)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber2)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDispatch>(entity =>
            {
                entity.HasKey(e => e.DispatchId);

                entity.ToTable("tblDispatch");

                entity.Property(e => e.AppGrossWeight)
                    .HasColumnType("decimal(10, 4)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DispatchDate).HasColumnType("date");

                entity.Property(e => e.DispatchNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lrno)
                    .IsRequired()
                    .HasColumnName("LRNO")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModeOfTransport)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NameOfTransporter)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.TotalRoll).HasDefaultValueSql("((0))");

                entity.Property(e => e.VehicleNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.TblDispatch)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK_tblDispatch_tblCustomer");

                entity.HasOne(d => d.BuyerNavigation)
                    .WithMany(p => p.TblDispatch)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK_tblDispatch_tblOrder");

                entity.HasOne(d => d.Consignee)
                    .WithMany(p => p.TblDispatch)
                    .HasForeignKey(d => d.ConsigneeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDispatch_tblConsignee");
            });

            modelBuilder.Entity<TblDispatchDetail>(entity =>
            {
                entity.HasKey(e => e.DispatchDetailId);

                entity.ToTable("tblDispatchDetail");

                entity.HasOne(d => d.Dispatch)
                    .WithMany(p => p.TblDispatchDetail)
                    .HasForeignKey(d => d.DispatchId)
                    .HasConstraintName("FK_tblDispatchDetail_tblDispatch");

                entity.HasOne(d => d.Slitting)
                    .WithMany(p => p.TblDispatchDetail)
                    .HasForeignKey(d => d.SlittingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDispatchDetail_tblSlitting");
            });

            modelBuilder.Entity<TblInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.ToTable("tblInvoice");

                entity.Property(e => e.AmountInWords)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Cgst)
                    .HasColumnName("CGST")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Igst)
                    .HasColumnName("IGST")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sgst)
                    .HasColumnName("SGST")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalAmountWithTax).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblInvoice)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblInvoice_tblCustomer");

                entity.HasOne(d => d.Consignee)
                    .WithMany(p => p.TblInvoice)
                    .HasForeignKey(d => d.ConsigneeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblInvoice_tblConsignee");
            });

            modelBuilder.Entity<TblInvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.InvoiceDetailId);

                entity.ToTable("tblInvoiceDetail");

                entity.HasOne(d => d.Dispatch)
                    .WithMany(p => p.TblInvoiceDetail)
                    .HasForeignKey(d => d.DispatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblInvoiceDetail_tblDispatch");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.TblInvoiceDetail)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_tblInvoiceDetail_tblInvoice");
            });

            modelBuilder.Entity<TblJumbo>(entity =>
            {
                entity.HasKey(e => e.JumboId);

                entity.ToTable("tblJumbo");

                entity.Property(e => e.CoreNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.JumboDate).HasColumnType("date");

                entity.Property(e => e.JumboNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.JumboStatusRemarks).IsUnicode(false);

                entity.Property(e => e.Length).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.LineNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RemainingJumbo)
                    .HasColumnType("decimal(23, 4)")
                    .HasComputedColumnSql("([Weight]-[TotalSlitJumbo]-[WasteWeight])");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Speed).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Thickness).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.TimeIn).HasColumnType("datetime");

                entity.Property(e => e.TimeOut).HasColumnType("datetime");

                entity.Property(e => e.TotalSlitJumbo)
                    .HasColumnType("decimal(10, 4)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.WasteWeight).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Width).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Winder)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblJumbo)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblJumbo_tblProduct");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TblJumbo)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblJumbo_tblShiftType");

                entity.HasOne(d => d.ShiftIncharge)
                    .WithMany(p => p.TblJumbo)
                    .HasForeignKey(d => d.ShiftInchargeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblJumbo_tblUser");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblJumbo)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_tblJumbo_tblJumboStatus");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TblJumbo)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblJumbo_tblTreatment");
            });

            modelBuilder.Entity<TblJumboStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("tblJumboStatus");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("tblMenu");

                entity.Property(e => e.Action).HasMaxLength(500);

                entity.Property(e => e.Controller).HasMaxLength(500);

                entity.Property(e => e.ImagePath).HasMaxLength(500);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("tblOrder");

                entity.Property(e => e.DeliverySchedule).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderStatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Specialinstruction).IsUnicode(false);

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOrder_tblCustomerBuyer");

                entity.HasOne(d => d.Consignee)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.ConsigneeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOrder_tblConsignee");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOrder_tblOrderStatus");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId);

                entity.ToTable("tblOrderDetail");

                entity.Property(e => e.Od)
                    .HasColumnName("OD")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.RemainingSlitting)
                    .HasColumnType("decimal(8, 2)")
                    .HasComputedColumnSql("([Quantity]-[TotalSlittingDone])");

                entity.Property(e => e.Thickness).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.TotalSlittingDone)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Width).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblOrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_tblOrderDetail_tblOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOrderDetail_tblProduct");
            });

            modelBuilder.Entity<TblOrderStatus>(entity =>
            {
                entity.HasKey(e => e.OrderStatusId);

                entity.ToTable("tblOrderStatus");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tblProduct");

                entity.Property(e => e.FilmType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MainApplication)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MainFeatures)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreTreatment)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Thickness).HasColumnType("decimal(7, 2)");
            });

            modelBuilder.Entity<TblRoleMenuMap>(entity =>
            {
                entity.HasKey(e => e.RoleMenuPk);

                entity.ToTable("tblRoleMenuMap");

                entity.Property(e => e.RoleMenuPk).HasColumnName("RoleMenuPK");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.TblRoleMenuMap)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRoleMenuMap_tblMenu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblRoleMenuMap)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_tblRoleMenuMap_tblRoles");
            });

            modelBuilder.Entity<TblRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("tblRoles");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSetting>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.ToTable("tblSetting");

                entity.Property(e => e.Datatype)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.KeyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblShiftType>(entity =>
            {
                entity.HasKey(e => e.ShiftId);

                entity.ToTable("tblShiftType");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSlitting>(entity =>
            {
                entity.HasKey(e => e.SlittingId);

                entity.ToTable("tblSlitting");

                entity.Property(e => e.CoreWeight).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Difference).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Grossweight).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.SlitWasteWeight).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Length).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Netweight).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Od)
                    .HasColumnName("OD")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SlittingDate).HasColumnType("date");

                entity.Property(e => e.SlittingRollNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Thickness).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Width).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.Core)
                    .WithMany(p => p.TblSlitting)
                    .HasForeignKey(d => d.CoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSlitting_tblCore");

                entity.HasOne(d => d.Jumbo)
                    .WithMany(p => p.TblSlittingJumbo)
                    .HasForeignKey(d => d.JumboId)
                    .HasConstraintName("FK_tblSlitting_tblJumbo");

                entity.HasOne(d => d.JumboJointId1Navigation)
                    .WithMany(p => p.TblSlittingJumboJointId1Navigation)
                    .HasForeignKey(d => d.JumboJointId1)
                    .HasConstraintName("FK_tblSlitting_tblJumboJoint1");

                entity.HasOne(d => d.JumboJointId2Navigation)
                    .WithMany(p => p.TblSlittingJumboJointId2Navigation)
                    .HasForeignKey(d => d.JumboJointId2)
                    .HasConstraintName("FK_tblSlitting_tblJumboJoint2");

                entity.HasOne(d => d.JumboJointId3Navigation)
                    .WithMany(p => p.TblSlittingJumboJointId3Navigation)
                    .HasForeignKey(d => d.JumboJointId3)
                    .HasConstraintName("FK_tblSlitting_tblJumboJoint3");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.TblSlitting)
                    .HasForeignKey(d => d.OperatorId)
                    .HasConstraintName("FK_tblSlitting_tblUser");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblSlitting)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_tblSlitting_tblOrder");

                entity.HasOne(d => d.PrimarySlitting)
                    .WithMany(p => p.InversePrimarySlitting)
                    .HasForeignKey(d => d.PrimarySlittingId)
                    .HasConstraintName("FK_tblSlitting_tblSlitting");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblSlitting)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSlitting_tblProduct");

                entity.HasOne(d => d.QualityNavigation)
                    .WithMany(p => p.TblSlitting)
                    .HasForeignKey(d => d.Quality)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSlitting_tblSlittingStatus");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TblSlitting)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSlitting_tblShiftType");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.TblSlitting)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSlitting_tblTreatment");
            });

            modelBuilder.Entity<TblSlittingStatus>(entity =>
            {
                entity.HasKey(e => e.SlittingStatusId);

                entity.ToTable("tblSlittingStatus");

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTreatment>(entity =>
            {
                entity.HasKey(e => e.TreatmentId);

                entity.ToTable("tblTreatment");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUser");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TokenExpiryDateTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_tblRoles");
            });
        }
    }
}
