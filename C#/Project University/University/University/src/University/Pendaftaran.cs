namespace University;

public class Pendaftaran
{
    public MataKuliah? MataKuliah { get; set; }
    public DateTime TanggalMulai { get; set; }
    public DateTime TanggalSelesai { get; set; }
    public Pendaftaran(MataKuliah? mataKuliah, DateTime tanggalMulai, DateTime tanggalSelesai)
    {
        MataKuliah = mataKuliah;
        TanggalMulai = tanggalMulai;
        TanggalSelesai = tanggalSelesai;
    }
}
