namespace Asuransi;

public class Vehicle
{

    public VehicleType VehicleType { get; set; }
    public string PoliceNumber { get; set; }
    public string VehicleRegistration { get; set; }
    public string VehicleBrand { get; set; }
    public string VehicleModel { get; set; }
    public string VehicleColor { get; set; }
    public Vehicle(VehicleType vehicleType, string policeNumber, string vehicleRegistration, string vehicleBrand, string vehicleModel, string vehicleColor)
    {
        VehicleType = vehicleType;
        PoliceNumber = policeNumber;
        VehicleRegistration = vehicleRegistration;
        VehicleBrand = vehicleBrand;
        VehicleModel = vehicleModel;
        VehicleColor = vehicleColor;
    }
}
