using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DO_AN.Models;

public partial class DoAnContext : IdentityDbContext<User>
{
    public DoAnContext()
    {
    }

    public DoAnContext(DbContextOptions<DoAnContext> options) : base(options)
    {
    }

    public virtual DbSet<CtDtq> CtDtqs { get; set; }

    public virtual DbSet<Ctdd> Ctdds { get; set; }

    public virtual DbSet<Ctpt> Ctpts { get; set; }

    public virtual DbSet<Danhgia> Danhgia { get; set; }

    public virtual DbSet<Danhlamtc> Danhlamtcs { get; set; }

    public virtual DbSet<Diemden> Diemdens { get; set; }

    public virtual DbSet<Diemkhoihanh> Diemkhoihanhs { get; set; }

    public virtual DbSet<Diemthamquan> Diemthamquans { get; set; }

    public virtual DbSet<Hinhanh> Hinhanhs { get; set; }

    public virtual DbSet<Huongdan> Huongdans { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Khachsan> Khachsans { get; set; }

    public virtual DbSet<Khuyenmai> Khuyenmais { get; set; }

    public virtual DbSet<Lichtrinh> Lichtrinhs { get; set; }

    public virtual DbSet<Loaitour> Loaitours { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phieudattour> Phieudattours { get; set; }

    public virtual DbSet<Phuongtiendc> Phuongtiendcs { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=thu6bc;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CtDtq>(entity =>
        {
            entity.HasKey(e => new { e.Madtq, e.Madl });

            entity.ToTable("CT_DTQ");

            entity.HasIndex(e => e.Madl, "CT_DTQ2_FK");

            entity.HasIndex(e => e.Madtq, "CT_DTQ_FK");

            entity.Property(e => e.Madtq).HasColumnName("MADTQ");
            entity.Property(e => e.Madl).HasColumnName("MADL");
            entity.Property(e => e.Motachitiet)
                .HasMaxLength(4000)
                .HasColumnName("MOTACHITIET");

            entity.HasOne(d => d.MadlNavigation).WithMany(p => p.CtDtqs)
                .HasForeignKey(d => d.Madl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_DTQ_CT_DTQ2_DANHLAMT");

            entity.HasOne(d => d.MadtqNavigation).WithMany(p => p.CtDtqs)
                .HasForeignKey(d => d.Madtq)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_DTQ_CT_DTQ_DIEMTHAM");
        });

        modelBuilder.Entity<Ctdd>(entity =>
        {
            entity.HasKey(e => new { e.Malt, e.Maks, e.Madtq });

            entity.ToTable("CTDD");

            entity.HasIndex(e => e.Maks, "CTDD2_FK");

            entity.HasIndex(e => e.Madtq, "CTDD3_FK");

            entity.HasIndex(e => e.Malt, "CTDD_FK");

            entity.Property(e => e.Malt).HasColumnName("MALT");
            entity.Property(e => e.Maks).HasColumnName("MAKS");
            entity.Property(e => e.Madtq).HasColumnName("MADTQ");
            entity.Property(e => e.Ngay).HasColumnName("NGAY");
            entity.Property(e => e.Thutu).HasColumnName("THUTU");

            entity.HasOne(d => d.MadtqNavigation).WithMany(p => p.Ctdds)
                .HasForeignKey(d => d.Madtq)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTDD_CTDD3_DIEMTHAM");

            entity.HasOne(d => d.MaksNavigation).WithMany(p => p.Ctdds)
                .HasForeignKey(d => d.Maks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTDD_CTDD2_KHACHSAN");

            entity.HasOne(d => d.MaltNavigation).WithMany(p => p.Ctdds)
                .HasForeignKey(d => d.Malt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTDD_CTDD_LICHTRIN");
        });

        modelBuilder.Entity<Ctpt>(entity =>
        {
            entity.HasKey(e => new { e.Matour, e.Mapt });

            entity.ToTable("CTPT");

            entity.HasIndex(e => e.Mapt, "CTPT2_FK");

            entity.HasIndex(e => e.Matour, "CTPT_FK");

            entity.Property(e => e.Matour).HasColumnName("MATOUR");
            entity.Property(e => e.Mapt).HasColumnName("MAPT");
            entity.Property(e => e.Bienso)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("BIENSO");

            entity.HasOne(d => d.MaptNavigation).WithMany(p => p.Ctpts)
                .HasForeignKey(d => d.Mapt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPT_CTPT2_PHUONGTI");

            entity.HasOne(d => d.MatourNavigation).WithMany(p => p.Ctpts)
                .HasForeignKey(d => d.Matour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPT_CTPT_TOUR");
        });

        modelBuilder.Entity<Danhgia>(entity =>
        {
            entity.HasKey(e => new { e.Makh, e.Matour });

            entity.ToTable("DANHGIA");

            entity.HasIndex(e => e.Makh, "ASSOCIATION_16_FK");

            entity.HasIndex(e => e.Matour, "ASSOCIATION_17_FK");

            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Matour).HasColumnName("MATOUR");
            entity.Property(e => e.Noidungdanhgia)
                .HasMaxLength(4000)
                .HasColumnName("NOIDUNGDANHGIA");
            entity.Property(e => e.Sosao).HasColumnName("SOSAO");
            entity.Property(e => e.Thoigiandanhgia).HasColumnName("THOIGIANDANHGIA");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Danhgia)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANHGIA_ASSOCIATI_KHACHHAN");

            entity.HasOne(d => d.MatourNavigation).WithMany(p => p.Danhgia)
                .HasForeignKey(d => d.Matour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DANHGIA_ASSOCIATI_TOUR");
        });

        modelBuilder.Entity<Danhlamtc>(entity =>
        {
            entity.HasKey(e => e.Madl);

            entity.ToTable("DANHLAMTC");

            entity.Property(e => e.Madl).HasColumnName("MADL");
            entity.Property(e => e.Tendl)
                .HasMaxLength(100)
                .HasColumnName("TENDL");
        });

        modelBuilder.Entity<Diemden>(entity =>
        {
            entity.HasKey(e => e.Madd);

            entity.ToTable("DIEMDEN");

            entity.Property(e => e.Madd).HasColumnName("MADD");
            entity.Property(e => e.Tendd)
                .HasMaxLength(250)
                .HasColumnName("TENDD");
        });

        modelBuilder.Entity<Diemkhoihanh>(entity =>
        {
            entity.HasKey(e => e.Madkh);

            entity.ToTable("DIEMKHOIHANH");

            entity.Property(e => e.Madkh).HasColumnName("MADKH");
            entity.Property(e => e.Dc)
                .HasMaxLength(250)
                .HasColumnName("DC");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.Tendkh)
                .HasMaxLength(250)
                .HasColumnName("TENDKH");
        });

        modelBuilder.Entity<Diemthamquan>(entity =>
        {
            entity.HasKey(e => e.Madtq);

            entity.ToTable("DIEMTHAMQUAN");

            entity.HasIndex(e => e.Madd, "NAM_TRONG_FK");

            entity.Property(e => e.Madtq).HasColumnName("MADTQ");
            entity.Property(e => e.Madd).HasColumnName("MADD");
            entity.Property(e => e.Tendtq)
                .HasMaxLength(100)
                .HasColumnName("TENDTQ");
            entity.Property(e => e.Thongtinct)
                .HasMaxLength(4000)
                .HasColumnName("THONGTINCT");

            entity.HasOne(d => d.MaddNavigation).WithMany(p => p.Diemthamquans)
                .HasForeignKey(d => d.Madd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIEMTHAM_NAM_TRONG_DIEMDEN");
        });

        modelBuilder.Entity<Hinhanh>(entity =>
        {
            entity.HasKey(e => e.Mah);

            entity.ToTable("HINHANH");

            entity.HasIndex(e => e.Madl, "BAOGOM_FK");

            entity.Property(e => e.Mah).HasColumnName("MAH");
            entity.Property(e => e.Madl).HasColumnName("MADL");
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.MadlNavigation).WithMany(p => p.Hinhanhs)
                .HasForeignKey(d => d.Madl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HINHANH_BAOGOM_DANHLAMT");
        });

        modelBuilder.Entity<Huongdan>(entity =>
        {
            entity.HasKey(e => new { e.Manv, e.Matour });

            entity.ToTable("HUONGDAN");

            entity.HasIndex(e => e.Matour, "HUONGDAN2_FK");

            entity.HasIndex(e => e.Manv, "HUONGDAN_FK");

            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Matour).HasColumnName("MATOUR");
            entity.Property(e => e.Nhiemvu)
                .HasMaxLength(100)
                .HasColumnName("NHIEMVU");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Huongdans)
                .HasForeignKey(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HUONGDAN_HUONGDAN_NHANVIEN");

            entity.HasOne(d => d.MatourNavigation).WithMany(p => p.Huongdans)
                .HasForeignKey(d => d.Matour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HUONGDAN_HUONGDAN2_TOUR");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Makh);

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Dc)
                .HasMaxLength(250)
                .HasColumnName("DC");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.Sotourdadat).HasColumnName("SOTOURDADAT");
            entity.Property(e => e.Tenkh)
                .HasMaxLength(250)
                .HasColumnName("TENKH");
        });

        modelBuilder.Entity<Khachsan>(entity =>
        {
            entity.HasKey(e => e.Maks);

            entity.ToTable("KHACHSAN");

            entity.HasIndex(e => e.Madd, "PHAN_BO_FK");

            entity.Property(e => e.Maks).HasColumnName("MAKS");
            entity.Property(e => e.Dc)
                .HasMaxLength(250)
                .HasColumnName("DC");
            entity.Property(e => e.Madd).HasColumnName("MADD");
            entity.Property(e => e.Tenks)
                .HasMaxLength(250)
                .HasColumnName("TENKS");

            entity.HasOne(d => d.MaddNavigation).WithMany(p => p.Khachsans)
                .HasForeignKey(d => d.Madd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KHACHSAN_PHAN_BO_DIEMDEN");
        });

        modelBuilder.Entity<Khuyenmai>(entity =>
        {
            entity.HasKey(e => e.Makm);

            entity.ToTable("KHUYENMAI");

            entity.Property(e => e.Makm)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MAKM");
            entity.Property(e => e.Dk)
                .HasMaxLength(250)
                .HasColumnName("DK");
            entity.Property(e => e.Phantramkm).HasColumnName("PHANTRAMKM");
            entity.Property(e => e.Tenkm)
                .HasMaxLength(250)
                .HasColumnName("TENKM");
        });

        modelBuilder.Entity<Lichtrinh>(entity =>
        {
            entity.HasKey(e => e.Malt);

            entity.ToTable("LICHTRINH");

            entity.Property(e => e.Malt).HasColumnName("MALT");
            entity.Property(e => e.Tenlt)
                .HasMaxLength(250)
                .HasColumnName("TENLT");
        });

        modelBuilder.Entity<Loaitour>(entity =>
        {
            entity.HasKey(e => e.Maloai);

            entity.ToTable("LOAITOUR");

            entity.Property(e => e.Maloai).HasColumnName("MALOAI");
            entity.Property(e => e.Tenloai)
                .HasMaxLength(250)
                .HasColumnName("TENLOAI");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv);

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Dc)
                .HasMaxLength(250)
                .HasColumnName("DC");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(50)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Ngaysinh).HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.Tennv)
                .HasMaxLength(50)
                .HasColumnName("TENNV");
        });

        modelBuilder.Entity<Phieudattour>(entity =>
        {
            entity.HasKey(e => e.Mapdt);

            entity.ToTable("PHIEUDATTOUR");

            entity.HasIndex(e => e.Makm, "AP_DUNG_FK");

            entity.HasIndex(e => e.Makh, "DAT_FK");

            entity.HasIndex(e => e.Matour, "LAP_FK");

            entity.Property(e => e.Mapdt).HasColumnName("MAPDT");
            entity.Property(e => e.Dvt)
                .HasMaxLength(250)
                .HasColumnName("DVT");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Makm)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MAKM");
            entity.Property(e => e.Matour).HasColumnName("MATOUR");
            entity.Property(e => e.Ngaydat).HasColumnName("NGAYDAT");
            entity.Property(e => e.Song).HasColumnName("SONG");
            entity.Property(e => e.Tongtien).HasColumnName("TONGTIEN");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Phieudattours)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUDAT_DAT_KHACHHAN");

            entity.HasOne(d => d.MakmNavigation).WithMany(p => p.Phieudattours)
                .HasForeignKey(d => d.Makm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUDAT_AP_DUNG_KHUYENMA");

            entity.HasOne(d => d.MatourNavigation).WithMany(p => p.Phieudattours)
                .HasForeignKey(d => d.Matour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUDAT_LAP_TOUR");
        });

        modelBuilder.Entity<Phuongtiendc>(entity =>
        {
            entity.HasKey(e => e.Mapt);

            entity.ToTable("PHUONGTIENDC");

            entity.Property(e => e.Mapt).HasColumnName("MAPT");
            entity.Property(e => e.Tenpt)
                .HasMaxLength(250)
                .HasColumnName("TENPT");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Matour);

            entity.ToTable("TOUR");

            entity.HasIndex(e => e.Madkh, "BAT_DAU_FK");

            entity.HasIndex(e => e.Malt, "CO_FK");

            entity.HasIndex(e => e.Maloai, "THUOC_FK");

            entity.Property(e => e.Matour).HasColumnName("MATOUR");
            entity.Property(e => e.Anhdaidien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ANHDAIDIEN");
            entity.Property(e => e.Dvt)
                .HasMaxLength(250)
                .HasColumnName("DVT");
            entity.Property(e => e.Gia).HasColumnName("GIA");
            entity.Property(e => e.Madkh).HasColumnName("MADKH");
            entity.Property(e => e.Maloai).HasColumnName("MALOAI");
            entity.Property(e => e.Malt).HasColumnName("MALT");
            entity.Property(e => e.Ngaykh).HasColumnName("NGAYKH");
            entity.Property(e => e.Ngaykt).HasColumnName("NGAYKT");
            entity.Property(e => e.Sochodadat).HasColumnName("SOCHODADAT");
            entity.Property(e => e.Sodem).HasColumnName("SODEM");
            entity.Property(e => e.Soluongtoida).HasColumnName("SOLUONGTOIDA");
            entity.Property(e => e.Songay).HasColumnName("SONGAY");
            entity.Property(e => e.Tentour)
                .HasMaxLength(250)
                .HasColumnName("TENTOUR");

            entity.HasOne(d => d.MadkhNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.Madkh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TOUR_BAT_DAU_DIEMKHOI");

            entity.HasOne(d => d.MaloaiNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.Maloai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TOUR_THUOC_LOAITOUR");

            entity.HasOne(d => d.MaltNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.Malt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TOUR_CO_LICHTRIN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
