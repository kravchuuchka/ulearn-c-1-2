using System.Security.Cryptography.X509Certificates;

namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			var amountUnits = count % 10;
			var amountDozens = (count / 10) % 10;
			if (amountUnits == 1 && (amountDozens > 1 || amountDozens == 0))
				return "рубль";
			else if ((amountUnits > 1 && amountUnits < 5) && (amountDozens > 1 || amountDozens == 0))
				return "рубля";
			return "рублей";
		}
	}
}
