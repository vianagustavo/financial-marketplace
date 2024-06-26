namespace FinancialMarketplace.Application.Tools;

public static class ShortCircuitXor
{
    public static int? BetweenInt(int? on, int? off)
    {
        if (on.HasValue || off.HasValue)
        {
            int onOrDefault = on ?? 0;
            int offOrDefault = off ?? 0;

            if (onOrDefault == 1 && offOrDefault == 0)
            {
                return 1;
            }
            else if (onOrDefault == 0 && offOrDefault == 1)
            {
                return 0;
            }
        }

        return null;
    }
}