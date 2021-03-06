// ===========================================================
// Copyright (C) 2014-2015 Kendar.org
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
// files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, 
// modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
// is furnished to do so, subject to the following conditions:
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS 
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF 
// OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ===========================================================


using Node.Cs.Lib.Filters;

namespace Node.Cs.Lib.Test.Mocks
{
	public class MockFilter : FilterBase
	{
		public int CountPreExecute = 0;
		public int CountPostExecute = 0;
		public MockFilter()
		{

		}

		public override bool OnPreExecute(System.Web.HttpContextBase context)
		{
			CountPreExecute++;
			return true;
		}

		public override void OnPostExecute(System.Web.HttpContextBase context, Controllers.IResponse result)
		{
			CountPostExecute++;
		}

	}

	public class MockFilterAtt:FilterBase
	{
		public static int CountPreExecute = 0;
		public static int CountPostExecute = 0;
		public MockFilterAtt()
		{
			CountPreExecute = 0;
			CountPostExecute = 0;
		}

		public override bool OnPreExecute(System.Web.HttpContextBase context)
		{
			CountPreExecute++;
			return true;
		}

		public override void OnPostExecute(System.Web.HttpContextBase context, Controllers.IResponse result)
		{
			CountPostExecute++;
		}
	}
}