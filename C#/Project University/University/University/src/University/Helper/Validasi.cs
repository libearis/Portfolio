namespace University.Helper;

public class Validasi
{
    public static string NIKSiswaValidasi(Dictionary<string, Mahasiswa> kumpulanMahasiswa)
    {
        string infoMahasiswa = Console.ReadLine();
        Mahasiswa mahasiswa = new Mahasiswa();
        while(!kumpulanMahasiswa.ContainsKey(infoMahasiswa))
        {
            System.Console.WriteLine("Mohon Input lagi sesuai NIK yang ada:");
            infoMahasiswa = NIKSiswaValidasi(kumpulanMahasiswa);
        }
        return infoMahasiswa;
    }
    public static string NIKDosenValidasi(Dictionary<string, Dosen> kumpulanDosen)
    {
        string infoDosen = Console.ReadLine();
        foreach(MataKuliah mataKuliah in kumpulanDosen[infoDosen].KumpulanMatkul)
        {

        }
        Dosen dosen = new Dosen();
        while(!kumpulanDosen.TryGetValue(infoDosen, out dosen))
        {
            System.Console.WriteLine("Mohon Input lagi sesuai NIK yang ada:");
            infoDosen = NIKDosenValidasi(kumpulanDosen);
        }
        return infoDosen;
    }
    public static string MenuValidasi(int jarakIndeks)
    {
        string masukanMenu = Console.ReadLine();
        int number = 0;
        while(!Int32.TryParse(masukanMenu, out number) || Convert.ToInt32(masukanMenu) > jarakIndeks || Convert.ToInt32(masukanMenu) < 1)
        {
            System.Console.WriteLine("Mohon input angka sesuai dengan menu yang ingin dituju");
            masukanMenu = MenuValidasi(jarakIndeks);
        }
        return masukanMenu;
    }
}
