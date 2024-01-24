namespace University.Helper;

public class CariTanggal
{
    public static int CariBulan(DateTime tanggalMulai, DateTime tanggalAkhir)
    {
        int tahun = CariTahun(tanggalMulai, tanggalAkhir);
        int jarakBulan =  tanggalMulai.Month - tanggalAkhir.Month - 12 * tahun;
        jarakBulan %= 12;
        return Math.Abs(jarakBulan);
    }
    public static int CariTahun(DateTime tanggalMulai, DateTime tanggalAkhir)
    {
        TimeSpan cariTahun = tanggalAkhir - tanggalMulai;
        int jarakTahun = Convert.ToInt32(cariTahun.Days) / 365;
        return jarakTahun;
    }
    public static string CariJarakDetailTanggal(DateTime tanggalMulai, DateTime tanggalAkhir)
    {
        int tahun = CariTahun(tanggalMulai, tanggalAkhir);
        int bulan = CariBulan(tanggalMulai, tanggalAkhir);
        int hari = Math.Abs(tanggalAkhir.Day - tanggalMulai.Day);

        return $"({tahun} tahun {bulan} bulan {hari} hari)";
    }
    
}
