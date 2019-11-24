using System.Collections.Generic;

namespace Domain
{
	public interface ISeed<T>
	{
		List<T> GetSeedData();
	}
}
