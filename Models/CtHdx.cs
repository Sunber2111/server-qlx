namespace API.Models
{
    public class CtHdx
    {
        public int Id { get; set; }
        public int MaHdx { get; set; }
        public int MaXe { get; set; }
        public int MaKho { get; set; }     
        public double GiaXuat { get; set; }

        public virtual Hdx Hdx { get; set; }
        public virtual Kho Kho { get; set; }
        public virtual Xe Xe { get; set; }
    }
}
