using System.Globalization;
using static University.Helper.CariTanggal;
using static University.Helper.Validasi;
namespace University;

public static class Menu
{
    public static void MenuUtama()
    {
        Dictionary<string, Mahasiswa> kumpulanMahasiswa = Data.InformasiMahasiswa(Data.TambahMataKuliah());
        Dictionary<string, Dosen> kumpulanDosen = Data.InformasiDosen(Data.TambahMataKuliah());
        System.Console.WriteLine("Pilih nomor menu untuk masuk ke menunya:");
        System.Console.WriteLine("1. All Student Data");
        System.Console.WriteLine("2. All Tutor Data");
        System.Console.WriteLine("3. About this University");
        System.Console.WriteLine("4. Exit Application");
        System.Console.WriteLine("5. Print based on gender");
        string menuInput = MenuValidasi(4);

        switch(menuInput)
        {
            case "1":
            MenuSiswa();
            break;

            case "2":
            MenuTutor();
            break;

            case "3":
            System.Console.WriteLine("Universitas ini bernama Unicorn (University of Cornelius). Sudah didirikan sejak 12 December 1978 di Amerika Serikat, Southern State, di kota Texas");
            break;

            case "4":
            System.Console.WriteLine("Exiting Application....");
            break;

            case "5":
            CetakBerdasarkanGender(kumpulanDosen, kumpulanMahasiswa);
            break;
        }
        
    }
    public static void MenuSiswa()
    {
        Dictionary<string, Mahasiswa> kumpulanMahasiswa = Data.InformasiMahasiswa(Data.TambahMataKuliah());
        foreach(KeyValuePair<string, Mahasiswa> mahasiswa in kumpulanMahasiswa)
            {
                System.Console.WriteLine($"NIK: {mahasiswa.Key}, Name: {mahasiswa.Value.NamaDepan} {mahasiswa.Value.NamaBelakang}");
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Pilih nomor menu untuk masuk ke menunya:");
            System.Console.WriteLine("1. Student Information");
            System.Console.WriteLine("2. Back to Main Menu");
            System.Console.WriteLine("3. Exit Application");
            string menuInput = MenuValidasi(3);

            switch(menuInput)
            {
                case "1":
                CetakMahasiswa(kumpulanMahasiswa);

                System.Console.WriteLine();
                System.Console.WriteLine("Pilih nomor menu untuk masuk ke menunya:");
                System.Console.WriteLine("1. Back to All Student Data");
                System.Console.WriteLine("2. Back to Main Menu");
                System.Console.WriteLine("3. Exit Application");
                menuInput = MenuValidasi(3);

                switch(menuInput)
                {
                    case "1":
                    MenuSiswa();
                    break;
                    
                    case "2":
                    MenuUtama();
                    break;

                    case "3":
                    System.Console.WriteLine("Exiting Application....");
                    break;
                } 
                break;

                case "2":
                MenuUtama();
                break;

                case "3":
                System.Console.WriteLine("Exiting Application....");
                break;
            }
    }
    public static void MenuTutor()
    {
        Dictionary<string, Dosen> kumpulanDosen = Data.InformasiDosen(Data.TambahMataKuliah());
        foreach(KeyValuePair<string, Dosen> dosen in kumpulanDosen)
            {
                System.Console.WriteLine($"NIK: {dosen.Key}, Name: {dosen.Value.NamaDepan} {dosen.Value.NamaBelakang}");
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Pilih nomor menu untuk masuk ke menunya:");
            System.Console.WriteLine("1. Tutor Information");
            System.Console.WriteLine("2. Back to Main Menu");
            System.Console.WriteLine("3. Exit Application");
            string menuInput = MenuValidasi(3);

            switch(menuInput)
            {
                case "1":
                CetakTutor(kumpulanDosen);
                
                System.Console.WriteLine();
                System.Console.WriteLine("Pilih nomor menu untuk masuk ke menunya:");
                System.Console.WriteLine("1. Back to All Tutor Data");
                System.Console.WriteLine("2. Back to Main Menu");
                System.Console.WriteLine("3. Exit Application");
                menuInput = MenuValidasi(3);

                switch(menuInput)
                {
                    case "1":
                    MenuTutor();
                    break;
                    
                    case "2":
                    MenuUtama();
                    break;

                    case "3":
                    System.Console.WriteLine("Exiting Application....");
                    break;
                } 
                break;

                case "2":
                MenuUtama();
                break;

                case "3":
                System.Console.WriteLine("Exiting Application....");
                break;
            }
    }
    public static void CetakMahasiswa(Dictionary<string, Mahasiswa> kumpulanMahasiswa)
    {
        System.Console.WriteLine("Masukan nomor NIK yang anda ingin lihat informasinya:");
        string infoMahasiswaInput = NIKSiswaValidasi(kumpulanMahasiswa);
        System.Console.WriteLine();

        System.Console.WriteLine($"First Name: {kumpulanMahasiswa[infoMahasiswaInput].NamaDepan}");
        System.Console.WriteLine($"Last Name: {kumpulanMahasiswa[infoMahasiswaInput].NamaBelakang}");
        System.Console.WriteLine($"Gender {kumpulanMahasiswa[infoMahasiswaInput].JenisKelamin}");
        System.Console.WriteLine($"Birth Information: {kumpulanMahasiswa[infoMahasiswaInput].KotaLahir}, {kumpulanMahasiswa[infoMahasiswaInput].TanggalLahir.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("id-ID"))} ({kumpulanMahasiswa[infoMahasiswaInput].Umur} tahun)");
        System.Console.WriteLine($"Religion: {kumpulanMahasiswa[infoMahasiswaInput].Agama}");
        System.Console.WriteLine($"Blood Type: {kumpulanMahasiswa[infoMahasiswaInput].GolonganDarah}");
        System.Console.WriteLine($"ID Card: {kumpulanMahasiswa[infoMahasiswaInput].NomorKTP}");
        System.Console.WriteLine($"Entry Date (Duration): {kumpulanMahasiswa[infoMahasiswaInput].TanggalMasuk.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("id-ID"))} {kumpulanMahasiswa[infoMahasiswaInput].LamaStudi}");
        System.Console.WriteLine();
        
        System.Console.WriteLine("Enrollment Information:");
        foreach(Pendaftaran pendaftaran in kumpulanMahasiswa[infoMahasiswaInput].KumpulanPendaftaran)
        {
            if(pendaftaran.TanggalSelesai != DateTime.MinValue)
            {
                System.Console.WriteLine
                ($"{pendaftaran.MataKuliah.NamaMatkul} in {pendaftaran.MataKuliah.NamaPenjurusan}, ({pendaftaran.TanggalMulai.ToString("dd MMMM yyyy")} - {pendaftaran.TanggalSelesai.ToString("dd MMMM yyyy")}), {CariJarakDetailTanggal(pendaftaran.TanggalMulai, pendaftaran.TanggalSelesai)} +{pendaftaran.MataKuliah.Poin} poin");
            }
            else
            {
                System.Console.WriteLine($"{pendaftaran.MataKuliah.NamaMatkul} in {pendaftaran.MataKuliah.NamaPenjurusan}, ({pendaftaran.TanggalMulai.ToString("dd MMMM yyyy")} - N/A), (N/A tahun, N/A bulan, N/A hari) +{pendaftaran.MataKuliah.Poin} poin");
            }
        }
    }
    public static void CetakTutor(Dictionary<string, Dosen> kumpulanDosen)
    {
        System.Console.WriteLine("Masukan nomor NIK yang anda ingin lihat informasinya:");
        string infoDosenInput = NIKDosenValidasi(kumpulanDosen);
        System.Console.WriteLine();

        System.Console.WriteLine($"First Name: {kumpulanDosen[infoDosenInput].NamaDepan}");
        System.Console.WriteLine($"Last Name: {kumpulanDosen[infoDosenInput].NamaBelakang}");
        System.Console.WriteLine($"Gender {kumpulanDosen[infoDosenInput].JenisKelamin}");
        System.Console.WriteLine($"Birth Information: {kumpulanDosen[infoDosenInput].KotaLahir}, {kumpulanDosen[infoDosenInput].TanggalLahir.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("id-ID"))} ({kumpulanDosen[infoDosenInput].Umur} tahun)");
        System.Console.WriteLine($"Religion: {kumpulanDosen[infoDosenInput].Agama}");
        System.Console.WriteLine($"Blood Type: {kumpulanDosen[infoDosenInput].GolonganDarah}");
        System.Console.WriteLine($"ID Card: {kumpulanDosen[infoDosenInput].NomorKTP}");
        System.Console.WriteLine($"Hire Date (Duration): {kumpulanDosen[infoDosenInput].TanggalBekerja.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("id-ID"))} {CariJarakDetailTanggal(kumpulanDosen[infoDosenInput].TanggalBekerja, DateTime.Now)}");
        System.Console.WriteLine();

        System.Console.WriteLine("Competency Information:");
        foreach(MataKuliah matkul in kumpulanDosen[infoDosenInput].KumpulanMatkul)
        {
            if(matkul.NamaMatkul == null)
            {
                System.Console.WriteLine($"N/A in {matkul.NamaPenjurusan}");
            }
            else
            {
                System.Console.WriteLine($"{matkul.NamaMatkul} in {matkul.NamaPenjurusan}");
            }
        }
    }
    public static void CetakBerdasarkanGender(Dictionary<string, Dosen> kumpulanDosen, Dictionary<string, Mahasiswa> kumpulanMahasiswa)
    {
        System.Console.WriteLine("Pilih gender yang mau ditampilkan");
        string masukanGender = Console.ReadLine();

        foreach(KeyValuePair<string, Mahasiswa> kumpulan in kumpulanMahasiswa)
        {
            System.Console.WriteLine(kumpulan.Value.NamaDepan);
        }
    }
}
