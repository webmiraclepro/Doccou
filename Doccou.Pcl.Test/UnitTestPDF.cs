using NUnit.Framework;

namespace Doccou.Pcl.Test
{
	[TestFixture]
	public class UnitTestPdf
	{
		//[Test]
		//public void TestEmptyPdf()
		//{

		//}

		[Test]
		public void TestOnePagePdf()
		{
			const string path = "../../Example/Pdf/OnePage.pdf";
			Assert.AreEqual(1, Helper.ReadCount(path));
		}

		//[Test]
		//public void TestBigPdf()
		//{
		//}
	}
}
