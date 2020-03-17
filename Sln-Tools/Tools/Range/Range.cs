using System;
using System.Collections.Generic;

namespace Tools.Range
{
	public static class Range
	{
		#region Public Methods

		public static IEnumerable<DateTime> DateTime(DateTime start,DateTime stop,TimeSpan step)
		{
			if(step == TimeSpan.Zero)
			{
				throw new Exception($"Please set the value of {nameof(step)}");
			}

			if(start == stop)
			{
				throw new Exception($"{nameof(start)} and {nameof(start)} are equals");
			}
			if((stop - start) > TimeSpan.Zero ^ step > TimeSpan.Zero)
			{
				throw new Exception($"Infinite loop");
			}
			return LocalDateTime();

			IEnumerable<DateTime> LocalDateTime()
			{
				while(start <= stop)
				{
					yield return start;
					start += step;
				}
			}
		}

		public static IEnumerable<DateTime> DateTimeCount(DateTime start,int count,TimeSpan step = default)
		{
			for(var i = 0;i < count;i++)
			{
				yield return start;
				start += step;
			}
		}

		public static IEnumerable<DateTime> DateTimeDayesDOWN(DateTime start,int count)
		{
			var step = TimeSpan.FromDays(1);
			for(var i = 0;i < count;i++)
			{
				yield return start;
				start -= step;
			}
		}

		public static IEnumerable<DateTime> DateTimeDayesUP(DateTime start,int count)
		{
			var step = TimeSpan.FromDays(1);
			for(var i = 0;i < count;i++)
			{
				yield return start;
				start += step;
			}
		}

		public static IEnumerable<DateTime> DateTimeMonthsDOWN(DateTime start,int count)
		{
			for(var i = 0;i < count;i++)
			{
				yield return start;
				start = start.AddMonths(-1);
			}
		}

		public static IEnumerable<DateTime> DateTimeMonthsUP(DateTime start,int count)
		{
			for(var i = 0;i < count;i++)
			{
				yield return start;
				start = start.AddMonths(1);
			}
		}

		public static IEnumerable<double> Dboule(double start,double stop,double step = 1)
		{
			if(step == 0)
			{
				throw new Exception($"Please set the value of {nameof(step)}");
			}
			if(start == stop)
			{
				throw new Exception($"{nameof(start)} and {nameof(start)} are equals");
			}
			if(Math.Sign(start - stop) == Math.Sign(step))
			{
				throw new Exception("Infinite loop");
			}
			return LocalDouble();

			IEnumerable<double> LocalDouble()
			{
				while(start <= stop)
				{
					yield return start;
					start += step;
				}
			}
		}

		public static IEnumerable<double> DbouleCount(double start,int count,double step = 0)
		{
			for(var i = 0;i < count;i++)
			{
				yield return start;
				start += step;
			}
		}

		public static IEnumerable<int> Int(int start,int stop,int step = 1)
		{
			if(step == 0)
			{
				throw new Exception($"Please set the value of {nameof(step)}");
			}
			if(start == stop)
			{
				throw new Exception($"{nameof(start)} and {nameof(start)} are equals");
			}
			if(Math.Sign(start - stop) == Math.Sign(step))
			{
				throw new Exception("Infinite loop");
			}
			return LocalInt();

			IEnumerable<int> LocalInt()
			{
				while(start <= stop)
				{
					yield return start;
					start += step;
				}
			}
		}

		public static IEnumerable<int> IntCount(int start,int count,int step = 0)
		{
			for(var i = 0;i < count;i++)
			{
				yield return start;
				start += step;
			}
		}

		#endregion Public Methods
	}
}
