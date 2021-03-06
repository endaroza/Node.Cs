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


using System;
using System.Collections.Generic;
using System.Linq;

namespace Node.Cs.Lib.Controllers
{
	public class ModelStateDictionary
	{
		private readonly Dictionary<string, List<string>> _messages;

		public ModelStateDictionary()
		{
			_messages = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
		}

		// ReSharper disable once UseMethodAny.2
		public bool IsValid { get { return _messages.Count() == 0; } }

		public void AddModelError(string field, string message)
		{
			if (!_messages.ContainsKey(field))
			{
				_messages.Add(field, new List<string>());
			}
			_messages[field].Add(message);
		}

		public List<string> GetErrors(string forFields = "")
		{
			if (!_messages.ContainsKey(forFields)) return new List<string>();
			return _messages[forFields];
		}
	}
}
