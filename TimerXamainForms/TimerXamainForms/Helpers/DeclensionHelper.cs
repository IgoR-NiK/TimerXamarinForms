namespace TimerXamainForms.Helpers
{
    static class DeclensionHelper
    {
		public static string GetDeclension(int number, string nominativ, string genetiv, string plural)
		{
			number %= 100;
			if (number >= 11 && number <= 19)
			{
				return plural;
			}

			number %= 10;
			switch (number)
			{
				case 1:
					return nominativ;
				case 2:
				case 3:
				case 4:
					return genetiv;
				default:
					return plural;
			}
		}
	}
}