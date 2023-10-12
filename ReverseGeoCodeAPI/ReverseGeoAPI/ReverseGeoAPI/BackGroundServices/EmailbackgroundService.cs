using System.ComponentModel;

namespace ReverseGeoAPI.BackGroundServices
{
	public class EmailbackgroundService:BackgroundService
	{
		public EmailbackgroundService()
		{

		}
		
		protected override Task ExecuteAsync(CancellationToken cancellationToken)
		{
			if(cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}

			while(true)
			{
				//SendEmail();
			}

			return Task.CompletedTask;
		}
		//int n = 111112811113;
		public static int Contigousone(int num)
		{
			int m = num;
			int max = 0;
			int revmax = 0;
			int temp = 0;


			while (m > 0)
			{
				int r = m % 10;
				m = m / 10;
				if (r == 1)
				{
					revmax++;
				}
				else
				{
					if (revmax >= max)
					{
						max = revmax;
					}
					revmax = 0;

				}

			}
			if (revmax >= max)
			{
				max = revmax;
			}
			return max;
		}


	}
}
