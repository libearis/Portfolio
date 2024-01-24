using University.Enumeration;

namespace University;

public class Dosen : Orang
{
    public string? NIK { get; set; }
    public string Gaji { get; set; }
    public DateTime TanggalBekerja { get; set; }
    public List<MataKuliah> KumpulanMatkul { get; set; }
    public Dosen
    (
        string? nIK, 
        string gaji, 
        DateTime tanggalBekerja, 
        List<MataKuliah> kumpulanMatkul,
        string namaDepan, 
        string namaBelakang, 
        DateTime tanggalLahir, 
        string kotaLahir, 
        Gender jenisKelamin,
        int umur,
        string nomorKTP,
        Agama agama,
        GolonganDarah golonganDarah
    ) : base(namaDepan, namaBelakang, tanggalLahir, kotaLahir, jenisKelamin, umur, nomorKTP, agama, golonganDarah)
    {
        NIK = nIK;
        Gaji = gaji;
        TanggalBekerja = tanggalBekerja;
        this.KumpulanMatkul = kumpulanMatkul;
    }
    public Dosen()
    {

    }
}
