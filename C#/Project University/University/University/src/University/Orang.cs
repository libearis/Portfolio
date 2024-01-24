using University.Enumeration;

namespace University;

public class Orang
{
    public string? NamaDepan { get; set; }
    public string? NamaBelakang { get; set; }
    public DateTime TanggalLahir { get; set; }
    public string? KotaLahir { get; set; }
    public Gender JenisKelamin { get; set; }
    public int Umur { get; set; }
    public string? NomorKTP { get; set; }
    public Agama Agama { get; set; }
    public GolonganDarah GolonganDarah { get; set; }
    public Orang
    (
        string? namaDepan, 
        string? namaBelakang, 
        DateTime tanggalLahir, 
        string? kotaLahir, 
        Gender jenisKelamin, 
        int umur, 
        string? nomorKTP, 
        Agama agama, 
        GolonganDarah golonganDarah
    )
    {
        NamaDepan = namaDepan;
        NamaBelakang = namaBelakang;
        TanggalLahir = tanggalLahir;
        KotaLahir = kotaLahir;
        JenisKelamin = jenisKelamin;
        Umur = umur;
        NomorKTP = nomorKTP;
        Agama = agama;
        GolonganDarah = golonganDarah;
    }
    public Orang()
    {
        
    }
}
