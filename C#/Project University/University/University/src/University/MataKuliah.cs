namespace University;

public class MataKuliah
{

    public string NamaMatkul { get; set; }
    public string? NamaPenjurusan { get; set; }
    public int Poin { get; set; }
    public MataKuliah(string? namaMatkul, string? namaPenjurusan, int poin)
    {
        NamaMatkul = namaMatkul;
        NamaPenjurusan = namaPenjurusan;
        Poin = poin;
    }
}
