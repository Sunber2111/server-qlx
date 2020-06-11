using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaoHanh> BaoHanh { get; set; }
        public virtual DbSet<CtHdn> CtHdn { get; set; }
        public virtual DbSet<CtHdx> CtHdx { get; set; }
        public virtual DbSet<CtKho> CtKho { get; set; }
        public virtual DbSet<Hdn> Hdn { get; set; }
        public virtual DbSet<Hdx> Hdx { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<Kho> Kho { get; set; }
        public virtual DbSet<LoaiXe> LoaiXe { get; set; }
        public virtual DbSet<Ncc> Ncc { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<Xe> Xe { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BaoHanh>(entity =>
            {
                entity.HasKey(e => e.MaBh);

                entity.Property(e => e.MaBh)
                    .HasColumnName("MaBH")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.MaXe).HasColumnName("MaXE");

                entity.Property(e => e.ThoiGian)
                    .HasColumnName("ThoiGian");

                entity.Property(e => e.NgayMua).HasColumnType("date");


                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.BaoHanh)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_BaoHanh_KhachHang")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.BaoHanh)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_BaoHanh_NhanVien")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Xe)
                    .WithOne(p => p.BaoHanh)
                    .HasForeignKey<BaoHanh>(p=>p.MaXe)
                    .HasConstraintName("FK_BaoHanh_Xe")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CtHdn>(entity =>
            {
                entity.ToTable("CT_HDN");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MaHdn).HasColumnName("MaHDN");

                entity.Property(e => e.MaKho).HasColumnName("MaKho");

                entity.HasOne(d => d.Hdn)
                    .WithMany(p => p.CtHdn)
                    .HasForeignKey(d => d.MaHdn)
                    .HasConstraintName("FK_CT_HDN_HDN")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Kho)
                    .WithMany(p => p.CtHdn)
                    .HasForeignKey(d => d.MaKho)
                    .HasConstraintName("FK_CT_HDN_Kho")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Xe)
                    .WithOne(p => p.CtHdn)
                    .HasForeignKey<CtHdn>(d => d.MaXe)
                    .HasConstraintName("FK_CT_HDN_Xe")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CtHdx>(entity =>
            {
                entity.ToTable("CT_HDX");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MaHdx).HasColumnName("MaHDX");

                entity.Property(e => e.MaKho).HasColumnName("MaKho");

                entity.HasOne(d => d.Hdx)
                    .WithMany(p => p.CtHdx)
                    .HasForeignKey(d => d.MaHdx)
                    .HasConstraintName("FK_CT_HDX_HDX")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Kho)
                    .WithMany(p => p.CtHdx)
                    .HasForeignKey(d => d.MaKho)
                    .HasConstraintName("FK_CT_HDX_Kho")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Xe)
                    .WithOne(p => p.CtHdx)
                    .HasForeignKey<CtHdx>(d => d.MaXe)
                    .HasConstraintName("FK_CT_HDX_Xe")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CtKho>(entity =>
            {
                entity.ToTable("CT_Kho");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MaKho).HasColumnName("maKho");

                entity.Property(e => e.MaXe).HasColumnName("maXe");

                entity.HasOne(d => d.Kho)
                    .WithMany(p => p.CtKho)
                    .HasForeignKey(d => d.MaKho)
                    .HasConstraintName("FK_CT_Kho_Kho")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Xe)
                    .WithOne(p => p.CtKho)
                    .HasForeignKey<CtKho>(p=>p.MaXe)
                    .HasConstraintName("FK_CT_Kho_Xe")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Hdn>(entity =>
            {
                entity.HasKey(e => e.MaHdn);

                entity.ToTable("HDN");

                entity.Property(e => e.MaHdn)
                    .HasColumnName("MaHDN")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NgayNhap).HasColumnType("date");

                entity.HasOne(d => d.Ncc)
                    .WithMany(p => p.Hdn)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_HDN_NCC")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.Hdn)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HDN_NhanVien")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Hdx>(entity =>
            {
                entity.HasKey(e => e.MaHdx);

                entity.ToTable("HDX");

                entity.Property(e => e.MaHdx)
                    .HasColumnName("MaHDX")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NgayXuat).HasColumnType("date");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.Hdx)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HDX_KhachHang")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.Hdx)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HDX_NhanVien")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cmnd)
                    .HasColumnName("CMND")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenKh)
                    .HasColumnName("TenKH")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Kho>(entity =>
            {
                entity.HasKey(e => e.MaKho);

                entity.Property(e => e.MaKho)
                    .HasColumnName("maKho")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TenKho)
                    .HasColumnName("tenKho")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiXe>(entity =>
            {
                entity.HasKey(e => e.MaLoaiXe);

                entity.Property(e => e.MaLoaiXe).ValueGeneratedOnAdd();

                entity.Property(e => e.TenLoaiXe).HasMaxLength(50);
            });

            modelBuilder.Entity<Ncc>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("NCC");

                entity.Property(e => e.MaNcc)
                    .HasColumnName("MaNCC")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.TenNcc)
                    .HasColumnName("TenNCC")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.Property(e => e.MaNv)
                    .HasColumnName("MaNV")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MaChucVu);

                entity.Property(e => e.Cmnd)
                    .HasColumnName("CMND")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenNv)
                    .HasColumnName("TenNV")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Xe>(entity =>
            {
                entity.HasKey(e => e.MaXe);

                entity.Property(e => e.MaXe).ValueGeneratedOnAdd();

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.TenXe).HasMaxLength(50);

                entity.HasOne(d => d.LoaiXe)
                    .WithMany(p => p.Xe)
                    .HasForeignKey(d => d.MaLoaiXe)
                    .HasConstraintName("FK_Xe_LoaiXe")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Ncc)
                    .WithMany(p => p.Xe)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_Xe_NCC")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
