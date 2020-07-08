namespace NUnitTest.Interface
{
	public class Student
	{
		#region Public Properties
		public int Age { get; set; }
		public string Name { get; set; }
		#endregion Public Properties

		#region Public Methods

		public int GetAge() => Age;

		public string GetName() => Name;

		#endregion Public Methods
	}
}
