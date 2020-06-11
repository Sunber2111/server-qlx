using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenKH = table.Column<string>(maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(maxLength: 50, nullable: true),
                    GioiTinh = table.Column<bool>(nullable: false),
                    CMND = table.Column<string>(unicode: false, fixedLength: true, maxLength: 15, nullable: true),
                    SDT = table.Column<string>(unicode: false, fixedLength: true, maxLength: 15, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    LienHe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "Kho",
                columns: table => new
                {
                    maKho = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tenKho = table.Column<string>(maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    NgungHoatDong = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kho", x => x.maKho);
                });

            migrationBuilder.CreateTable(
                name: "LoaiXe",
                columns: table => new
                {
                    MaLoaiXe = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenLoaiXe = table.Column<string>(maxLength: 50, nullable: true),
                    DaNgungNhap = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiXe", x => x.MaLoaiXe);
                });

            migrationBuilder.CreateTable(
                name: "NCC",
                columns: table => new
                {
                    MaNCC = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenNCC = table.Column<string>(maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(maxLength: 50, nullable: true),
                    SDT = table.Column<string>(nullable: true),
                    NgungCungCap = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCC", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenNV = table.Column<string>(maxLength: 50, nullable: true),
                    GioiTinh = table.Column<bool>(nullable: false),
                    DiaChi = table.Column<string>(maxLength: 50, nullable: true),
                    SDT = table.Column<string>(unicode: false, fixedLength: true, maxLength: 15, nullable: true),
                    CMND = table.Column<string>(unicode: false, fixedLength: true, maxLength: 15, nullable: true),
                    MaChucVu = table.Column<int>(nullable: true),
                    TenDangNhap = table.Column<string>(nullable: true),
                    Hinh = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    MatKhau = table.Column<string>(nullable: true),
                    KichHoat = table.Column<bool>(nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    DaNghiViec = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "Xe",
                columns: table => new
                {
                    MaXe = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenXe = table.Column<string>(maxLength: 50, nullable: true),
                    MaLoaiXe = table.Column<int>(nullable: true),
                    MaNCC = table.Column<int>(nullable: true),
                    DaNgungBan = table.Column<bool>(nullable: false),
                    SoKhung = table.Column<string>(nullable: true),
                    SoMay = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xe", x => x.MaXe);
                    table.ForeignKey(
                        name: "FK_Xe_LoaiXe",
                        column: x => x.MaLoaiXe,
                        principalTable: "LoaiXe",
                        principalColumn: "MaLoaiXe",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Xe_NCC",
                        column: x => x.MaNCC,
                        principalTable: "NCC",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HDN",
                columns: table => new
                {
                    MaHDN = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaNCC = table.Column<int>(nullable: true),
                    MaNV = table.Column<int>(nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "date", nullable: false),
                    TrangThaiDaXoa = table.Column<bool>(nullable: false),
                    MoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HDN", x => x.MaHDN);
                    table.ForeignKey(
                        name: "FK_HDN_NCC",
                        column: x => x.MaNCC,
                        principalTable: "NCC",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_HDN_NhanVien",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HDX",
                columns: table => new
                {
                    MaHDX = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaNV = table.Column<int>(nullable: false),
                    MaKH = table.Column<int>(nullable: false),
                    NgayXuat = table.Column<DateTime>(type: "date", nullable: false),
                    TrangThaiDaXoa = table.Column<bool>(nullable: false),
                    MoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HDX", x => x.MaHDX);
                    table.ForeignKey(
                        name: "FK_HDX_KhachHang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_HDX_NhanVien",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BaoHanh",
                columns: table => new
                {
                    MaBH = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaNV = table.Column<int>(nullable: false),
                    MaKH = table.Column<int>(nullable: false),
                    MaXE = table.Column<int>(nullable: false),
                    ThoiGian = table.Column<int>(nullable: false),
                    NgayMua = table.Column<DateTime>(type: "date", nullable: false),
                    GiaBan = table.Column<int>(nullable: false),
                    TrangThaiDaXoa = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoHanh", x => x.MaBH);
                    table.ForeignKey(
                        name: "FK_BaoHanh_KhachHang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BaoHanh_NhanVien",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BaoHanh_Xe",
                        column: x => x.MaXE,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CT_Kho",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    maXe = table.Column<int>(nullable: false),
                    maKho = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_Kho", x => x.id);
                    table.ForeignKey(
                        name: "FK_CT_Kho_Kho",
                        column: x => x.maKho,
                        principalTable: "Kho",
                        principalColumn: "maKho",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_Kho_Xe",
                        column: x => x.maXe,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CT_HDN",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaHDN = table.Column<int>(nullable: false),
                    MaXe = table.Column<int>(nullable: false),
                    MaKho = table.Column<int>(nullable: false),
                    GiaNhap = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_HDN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CT_HDN_HDN",
                        column: x => x.MaHDN,
                        principalTable: "HDN",
                        principalColumn: "MaHDN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_HDN_Kho",
                        column: x => x.MaKho,
                        principalTable: "Kho",
                        principalColumn: "maKho",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CT_HDN_Xe",
                        column: x => x.MaXe,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CT_HDX",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaHDX = table.Column<int>(nullable: false),
                    MaXe = table.Column<int>(nullable: false),
                    MaKho = table.Column<int>(nullable: false),
                    GiaXuat = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_HDX", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CT_HDX_HDX",
                        column: x => x.MaHDX,
                        principalTable: "HDX",
                        principalColumn: "MaHDX",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_HDX_Kho",
                        column: x => x.MaKho,
                        principalTable: "Kho",
                        principalColumn: "maKho",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CT_HDX_Xe",
                        column: x => x.MaXe,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaoHanh_MaKH",
                table: "BaoHanh",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_BaoHanh_MaNV",
                table: "BaoHanh",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_BaoHanh_MaXE",
                table: "BaoHanh",
                column: "MaXE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CT_HDN_MaHDN",
                table: "CT_HDN",
                column: "MaHDN");

            migrationBuilder.CreateIndex(
                name: "IX_CT_HDN_MaKho",
                table: "CT_HDN",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_CT_HDN_MaXe",
                table: "CT_HDN",
                column: "MaXe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CT_HDX_MaHDX",
                table: "CT_HDX",
                column: "MaHDX");

            migrationBuilder.CreateIndex(
                name: "IX_CT_HDX_MaKho",
                table: "CT_HDX",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_CT_HDX_MaXe",
                table: "CT_HDX",
                column: "MaXe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CT_Kho_maKho",
                table: "CT_Kho",
                column: "maKho");

            migrationBuilder.CreateIndex(
                name: "IX_CT_Kho_maXe",
                table: "CT_Kho",
                column: "maXe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HDN_MaNCC",
                table: "HDN",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_HDN_MaNV",
                table: "HDN",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_HDX_MaKH",
                table: "HDX",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_HDX_MaNV",
                table: "HDX",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_MaLoaiXe",
                table: "Xe",
                column: "MaLoaiXe");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_MaNCC",
                table: "Xe",
                column: "MaNCC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaoHanh");

            migrationBuilder.DropTable(
                name: "CT_HDN");

            migrationBuilder.DropTable(
                name: "CT_HDX");

            migrationBuilder.DropTable(
                name: "CT_Kho");

            migrationBuilder.DropTable(
                name: "HDN");

            migrationBuilder.DropTable(
                name: "HDX");

            migrationBuilder.DropTable(
                name: "Kho");

            migrationBuilder.DropTable(
                name: "Xe");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "LoaiXe");

            migrationBuilder.DropTable(
                name: "NCC");
        }
    }
}
