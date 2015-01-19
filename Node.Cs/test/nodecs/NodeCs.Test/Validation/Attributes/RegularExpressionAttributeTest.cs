// ===========================================================
// Copyright (c) 2014-2015, Enrico Da Ros/kendar.org
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// ===========================================================


using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeCs.Shared.Attributes.Validation;

namespace NodeCs.Test.Validation.Attributes
{
	/// <summary>
	/// Summary description for RangeAttributesTest
	/// </summary>
	[TestClass]
	public class RegularExpressionAttributeTest
	{
		private const string EMAIL = @"^((([\w]+\.[\w]+)+)|([\w]+))@(([\w]+\.)+)([A-Za-z]{1,3})$";
		[TestMethod]
		public void ItShouldBePossibleToSetTheWithField()
		{
			var sc = new RegularExpressionAttribute(EMAIL);
			Assert.AreEqual(EMAIL, sc.Pattern);
		}

		[TestMethod]
		public void CompareValidStrings()
		{
			var sc = new RegularExpressionAttribute(EMAIL);
			Assert.IsFalse(sc.IsValid("dummy@creepy@foody", null));
			Assert.IsTrue(sc.IsValid("do@test.com", null));
		}
	}
}
