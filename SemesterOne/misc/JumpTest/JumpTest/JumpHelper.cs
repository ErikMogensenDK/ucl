

namespace JumpTest
{
    public class JumpHelper
    {
        public static string CalculateMeetingPoint(int TigerStart, int TigerJump, int WinnieStart, int WinnieJump)
        {
            for (int i = 0; i * TigerJump < 10000 && i * WinnieJump < 10000; i++)
            {
                int tigerPoint = TigerStart + TigerJump * i;
                int winniePoint = WinnieStart + WinnieJump * i;
                if (tigerPoint == winniePoint)
                    return $"{tigerPoint},{winniePoint}";
            }
            return "NO";
        }
    }
}