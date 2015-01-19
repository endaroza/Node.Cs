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


using System.Collections;
using System.Collections.Generic;
using ConcurrencyHelpers.Containers;
using CoroutinesLib.Shared;
using Http.Shared;
using NodeCs.Shared;
using System;
using CoroutinesLib.Shared.Logging;

namespace Node.Caching
{
	public class CachingModule : NodeModuleBase
	{
		private readonly NodeCache _nodeCache;
		private readonly NodeCacheCoroutine _nodeCacheCoroutineThread;
		private readonly ICoroutinesManager _coroutinesManager;

		public CachingModule()
		{
			Log = ServiceLocator.Locator.Resolve<ILogger>();
			_coroutinesManager = ServiceLocator.Locator.Resolve<ICoroutinesManager>();
			var main = ServiceLocator.Locator.Resolve<IMain>();
			var timeout = TimeSpan.FromHours(1);
			_nodeCache = new NodeCache(_coroutinesManager, timeout);
			_nodeCacheCoroutineThread = new NodeCacheCoroutine(_nodeCache);
			_coroutinesManager.StartCoroutine(_nodeCacheCoroutineThread);
			SetParameter(HttpParameters.CacheInstance, _nodeCache);
			main.RegisterCommand("nodecache.groups", new CommandDefinition((Action)ShowGroups));
			main.RegisterCommand("nodecache.items", new CommandDefinition((Action<string>)ShowItems,typeof(string)));
		}

		public override void Initialize()
		{
			var timeout = GetParameter<TimeSpan>(HttpParameters.CacheTimeout);
			_nodeCache.Log = Log;
			if (timeout == null)
			{
				throw new NotImplementedException();
			}
			
		}

		protected override void Dispose(bool disposing)
		{
			_nodeCacheCoroutineThread.Stop();
			var main = ServiceLocator.Locator.Resolve<IMain>();
			main.UnregisterCommand("nodecache.groups", 0);
			main.UnregisterCommand("nodecache.items", 1);
		}

		private void ShowGroups()
		{
			NodeRoot.CWriteLine("Current cache groups:");

			NodeRoot.CWriteLine(NodeRoot.Pad("Name", 39) + " " + NodeRoot.Pad("Expire(ms)", 39));
			NodeRoot.CWriteLine(NodeRoot.Pad("", 39, "=") + " " + NodeRoot.Pad("", 39, "="));
			foreach (var item in _nodeCache.Groups.Values)
			{
				NodeRoot.CWriteLine(NodeRoot.Pad(item.Id, 39) + " " + NodeRoot.Pad(item.ExpireAfter.TotalMilliseconds, 39));
			}
			NodeRoot.CWriteLine();
		}

		private void ShowItems(string groupId)
		{
			groupId = groupId ?? string.Empty;
			NodeRoot.CWriteLine("Current item for cache group '" + groupId + "':");

			NodeRoot.CWriteLine(NodeRoot.Pad("Name", 80) + " " + NodeRoot.Pad("Expire", 30) + " " + NodeRoot.Pad("Expire", 40));
			NodeRoot.CWriteLine(NodeRoot.Pad("", 80, "=") + " " + NodeRoot.Pad("", 30, "=") + " " + NodeRoot.Pad("", 40, "="));
			foreach (var item in _nodeCache.GetItems(groupId))
			{
				NodeRoot.CWriteLine(NodeRoot.Pad(item.Key, 80) + " " + NodeRoot.Pad(item.Value.NextExpiration, 30) + " " + NodeRoot.Pad(item.Value.Value, 40));
			}
			NodeRoot.CWriteLine();
		}
	}
}
