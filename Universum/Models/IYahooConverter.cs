namespace Universum.Models
{
    public interface IYahooConverter
    {
        double RemoveCommaivideBy1000000Round2(string lastEquityString);
        double Roe(double income, double equity);
    }
}