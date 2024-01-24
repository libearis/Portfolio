using static University.Helper.CariTanggal;
using University.Enumeration;

namespace University;

public static class Data
{
    public static List<MataKuliah> TambahMataKuliah()
    {
        MataKuliah oop = new MataKuliah(null, "Programming", 20);
        MataKuliah datStruk = new MataKuliah("Data Structure", "Programming", 30);
        MataKuliah networking = new MataKuliah("Networking Fundamental", "Networking", 20);
        MataKuliah database = new MataKuliah("Database Fundamental", "Programming", 30);
        MataKuliah projectManage = new MataKuliah("Project Management Professional", "Management and Analysis", 40);
        MataKuliah uml = new MataKuliah("Unified Modeling Languange", "Management and Analysis", 20);
        MataKuliah java = new MataKuliah("Java Fundamental", "Programming", 30);
        MataKuliah infrastruktur = new MataKuliah("Infrastructure Design", "Networking", 30);
        MataKuliah networkSecurity = new MataKuliah("Network and Security", "Networking", 20);

        List<MataKuliah> kumpulanMatkul = new List<MataKuliah>()
        {
            oop,
            datStruk,
            networking,
            database,
            projectManage,
            uml,
            java,
            infrastruktur,
            networkSecurity
        };

        return kumpulanMatkul;
    }
    public static Dictionary<string, Mahasiswa> InformasiMahasiswa(List<MataKuliah> kumpulanMatkul)
    {
        #region Alex
        DateTime alexLahir = new DateTime(1990, 11, 23);
        int umurAlex = CariTahun(alexLahir, DateTime.Now);

        DateTime alexMasuk = new DateTime(2018, 4, 12);
        string periodeStudiAlex = CariJarakDetailTanggal(alexMasuk, DateTime.Now);

        Pendaftaran alexOOP = new Pendaftaran(kumpulanMatkul[0], new DateTime(2018, 4, 12), new DateTime(2018, 9, 12));
        Pendaftaran alexDatstruk = new Pendaftaran(kumpulanMatkul[1], new DateTime(2018, 8, 18), new DateTime());
        List<Pendaftaran> pendaftaranAlex = new List<Pendaftaran>(){alexOOP, alexDatstruk};
        Mahasiswa alex = new Mahasiswa
        (
            "A021", alexMasuk, periodeStudiAlex, pendaftaranAlex, 
            "Alex", "Wirianata", alexLahir, "Jakarta", Gender.Laki_laki, 
            umurAlex, "312008923111990002", Agama.Kristen, GolonganDarah.A
        );
        #endregion

        #region Desy
        DateTime desyLahir = new DateTime(1995, 5, 11);
        int umurDesy = CariTahun(desyLahir, DateTime.Now);

        DateTime desyMasuk = new DateTime(2018, 4, 1);
        string periodeDesy = CariJarakDetailTanggal(desyMasuk, DateTime.Now);

        Pendaftaran desyNetwork = new Pendaftaran(kumpulanMatkul[2], new DateTime(2018, 4, 1), new DateTime());
        Pendaftaran desyDatstruk = new Pendaftaran(kumpulanMatkul[1], new DateTime(2018, 8, 18), new DateTime());
        List<Pendaftaran> pendaftaranDesy = new List<Pendaftaran>(){desyNetwork, desyDatstruk};
        Mahasiswa desy = new Mahasiswa
        (
            "A022", desyMasuk, periodeDesy, pendaftaranDesy, 
            "Desy", "Oktaviani", desyLahir, "Bandung", Gender.Perempuan, 
            umurDesy, "312008911051995002", Agama.Islam, GolonganDarah.O
        ); 
        #endregion

        #region Joko
        DateTime jokoLahir = new DateTime(1990, 6, 7);
        int umurJoko = CariTahun(jokoLahir, DateTime.Now);

        DateTime jokoMasuk = new DateTime(2018, 4, 14);
        string periodeJoko = CariJarakDetailTanggal(jokoMasuk, DateTime.Now);

        Pendaftaran jokoUML = new Pendaftaran(kumpulanMatkul[2], new DateTime(2018, 5, 3), new DateTime(2018, 11, 1));
        List<Pendaftaran> pendaftaranJoko = new List<Pendaftaran>(){jokoUML};
        Mahasiswa joko = new Mahasiswa
        (
            "A023", jokoMasuk, periodeJoko, pendaftaranJoko, 
            "Joko", "Davidson", jokoLahir, "Jakarta", Gender.Laki_laki, 
            umurJoko, "3120089277889990002", Agama.Islam, GolonganDarah.A
        ); 
        #endregion
        
        Dictionary<string, Mahasiswa> kumpulanMahasiswa = new Dictionary<string, Mahasiswa>(){
            {alex.NIK, alex}, 
            {desy.NIK, desy}, 
            {joko.NIK, joko}
            };

        return kumpulanMahasiswa;
    }
    public static Dictionary<string, Dosen> InformasiDosen(List<MataKuliah> kumpulanMataKuliah)
    {
        #region Antik
        DateTime antikLahir = new DateTime(1988, 11, 12);
        int umurAntik = CariTahun(antikLahir, DateTime.Now);
        
        DateTime antikMasukKerja = new DateTime(2016, 3, 8);
        List<MataKuliah> antikKompetensi = new List<MataKuliah>()
        {
            kumpulanMataKuliah[7],
            kumpulanMataKuliah[8]
        };

        Dosen antik = new Dosen
        (
            "T701", "6.500.000/bulan", antikMasukKerja, antikKompetensi, 
            "Antik", "Haya", antikLahir, "Jakarta", 
            Gender.Perempuan, umurAntik, "312008912111988002", Agama.Islam, GolonganDarah.A
        );
        #endregion
        
        #region Cahya
        DateTime cahyaLahir = new DateTime(1989, 1, 7);
        int umurCahya = CariTahun(antikLahir, DateTime.Now);
        
        DateTime cahyaMasukKerja = new DateTime(2016, 4, 4);
        List<MataKuliah> cahyaKompetensi = new List<MataKuliah>()
        {
            kumpulanMataKuliah[0],
            kumpulanMataKuliah[3],
            kumpulanMataKuliah[6]
        };

        Dosen cahya = new Dosen
        (
            "T808", "8.800.000/bulan", cahyaMasukKerja, cahyaKompetensi, 
            "Cahya", "Subroto", cahyaLahir, "Surabaya", 
            Gender.Laki_laki, umurCahya, "312008907011989002", Agama.Islam, GolonganDarah.B
        );
        #endregion

        Dictionary<string, Dosen> kumpulanDosen = new Dictionary<string, Dosen>(){
            {antik.NIK, antik}, 
            {cahya.NIK, cahya}
            };
        
        return kumpulanDosen;
    }
}
