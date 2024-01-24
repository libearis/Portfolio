using University.Enumeration;

namespace University;

public class Mahasiswa : Orang
{
    public string NIK { get; set; }
    public DateTime TanggalMasuk { get; set; }
    public string LamaStudi { get; set; }
    public List<Pendaftaran>? KumpulanPendaftaran { get; set; }
    public Mahasiswa
    (
        string nIK, 
        DateTime tanggalMasuk, 
        string lamaStudi, 
        List<Pendaftaran>? kumpulanPendaftaran,
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
        TanggalMasuk = tanggalMasuk;
        LamaStudi = lamaStudi;
        KumpulanPendaftaran = kumpulanPendaftaran;

    }
    public Mahasiswa()
    {

    }
}
