namespace Asuransi;

public static class Data
{
    public static Dictionary<string, Product> InstantiateProduct()
    {
        Product healthyTogether = new Product("Sehat Bersama", ProductType.Kesehatan, PaymentFrequency.Bulanan, "Claim perawatan kelas 1.");
        Product extraHealth = new Product("Sehat Extra", ProductType.Kesehatan, PaymentFrequency.Bulanan, "Claim perawatan kelas VIP.");
        Product familyLife = new Product("Life Keluarga", ProductType.Jiwa, PaymentFrequency.Bulanan, "Mendapatkan 500.000.000 apabila terjadi sesuatu pada tertanggung.");
        Product lifeSpecial = new Product("Life Special", ProductType.Jiwa, PaymentFrequency.Tahunan, "Mendapatkan 800.000.000 apabila terjadi sesuatu pada tertanggung.");
        Product protection = new Product("Protection", ProductType.Kendaraan, PaymentFrequency.Tahunan, "Mendapat ganti rugi sampai 100.000.000 bila terjadi sesuatu.");
        Product protectionPlus = new Product("Protection Plus", ProductType.Kendaraan, PaymentFrequency.Tahunan, "Mendapat ganti rugi total lost apa bila terjadi sesuatu.");

        Dictionary<string, Product> products = new Dictionary<string, Product>()
        {
            {healthyTogether.ProductName, healthyTogether},
            {extraHealth.ProductName, extraHealth},
            {familyLife.ProductName, familyLife},
            {lifeSpecial.ProductName, lifeSpecial},
            {protection.ProductName, protection},
            {protectionPlus.ProductName, protectionPlus},
        };
        return products;
    }
}
