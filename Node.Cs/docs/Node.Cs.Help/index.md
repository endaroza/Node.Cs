﻿<!--settings(
title=Node.Cs 2.0
description=Node.Js like C# implementation.
filters=children;includefile
children_required=
)-->

<!--include(shared/breadcrumb.php)-->

## {Title}

<!--include(shared/childrensmall.php)-->

### Introduction

This is a project developed to simulate the behaviour of Node.Js. It's not a direct port since simply I prefer
the approach proposed here :) . Nothing against Node.Js developers, i love their work!

The idea is to leverage on the usage of "Coroutines" with the simple implementation offered by the C# IEnumerable
and IEnumerator. I could have made something based on the new await and async, but I like to loose myself in the
darkest and deepest forrest, for the sake of knowledge... or masochism sometimes.

NOTE THAT ALL THIS IS STILL IN ALPHA

### Why

Well, firstly I was curious to understand if the whole thing was feasible without using the new async and await
C# keywords. Having seen node.js and loving it's way of using resources, i thought to do something like that in
C#, adding a plugin based infrastructure and the support for the already defined .NET MVC structure.

I thought to reuse part of the Cassini project, but there were too much refactoring involved. And i started something
new, thanks to the HttpListener and the RazorEngine

After this (being really interested in LockFree data structures and, in general, to thread safe data structures,
i thought that it would be nice to have a system where, for example:

* Function x ask to download a web page, http://foo/bar
	* It asks for the page on a local cache, and pass to the cache a Lambda to get the data
	* Function x is stopped until the Lambda is completed, with the data
* Function y ask http://foo/bar, but the previous Lambda is not yet completed.
	* It asks for the page on the local cache, but the cache realizes that it is doing a request for the page
	* Function y is stopped until the Function y lambda is completed
* When the lambda completes, both function x and y takes a copy of the retrieved data. BUT
	* __Only on http request had been made__
	* __Thanks to the lock free data structures used (Queues, essentially) no context switch was needed__
	* __No CPU had been wasted started stopping tasks and threads__


All i did was something like what is made by the async/await keywords, but extending it beyond the tasks, like 
node,js does( [Decompiling Async/Await](http://community.sharpdevelop.net/blogs/danielgrunwald/archive/2012/04/16/decompiling-async-await.aspx)
written by the author of IlSpy).

### Features

#### Release 2.0.0




<!--

* Core
	* Easy porting from existing MVC controllers
	* Coroutine based operations, no context switches
	* Heavy usage of CAS atomic operations
	* Easy Async and task operations
	* Predisposition for usage with IOC containers like Castle Windsor
	* File upload
	* Sessions with support for custom session storage
	* Cookies
	* Redirects
	* Automatic binding on controller parameters
	* Support for all major verbs
	* Automatic caching of static files
	* Multple overlappable sources for files
	* MVC like routing system
	* Form and Basic authentication method with custom data provider for authentication
	* Embedded performance monitor (eventually connectable with the Windows native monitor)
	* MVC like action and global filters
	* Use of embedded resources as source of static and dynamic resources
	* Small memory/CPU footprint
	* Capable of thousands of concurrent connections
	* Automatic recycle when reaching too high resources usage (configurable)
	* Logging
* EntityFramework Module
	* Embeddable as nuget plugin
	* Offering a custom code first data provider for authentication
* Admin Module
	* Embeddable as nuget plugin
	* Simple administration console
	* Visualization of all statistics
* Razor Module
	* Embeddable as nuget plugin
	* Support for all the major helpers (UrlHelper and HtmlHelper) for model and form binding
	* Support for _ViewStart and _Layut
	* Support for RenderPartial and RenderAction
	* Support for ChildOnly actions

#### Release 1.2.0

* Bugs
	* Css mime not set
	* XmlResponse accepted only strings
* Quality
	* Improved testability (splitting everything)
	* Totally changed the network-related section to support websocket
* General
	* Visual Studio Template now has all jquery/css included..

### Milestones

Feel free to ask (with an explanation) to change my priorities! All these are more or less indipendents between them

* Release 2.0
	* Logging level by configuration
	* Routing through controllers attributes
	* WebSocket
	* Custom Castle Windsor module
	* Binaries cache with precompiled pages
	* NTLM1 Authentication with custom auth provider
	* Reduce recycle times
	* Enhance administration console
	* Porting to Mono
	* Optimize razor module loading

* Release 3.0
	* NTLM2 Authentication
	* Enhance administration console
	* ... more to come

Enjoy!-->

### Credits

* Thorsten Bruning for its [Universal Type Converter](http://www.codeproject.com/Articles/248440/Universal-Type-Converter).
* Jon Galloway for the [MVC Music Store](http://www.asp.net/mvc/tutorials/mvc-music-store) The base i used to define the minimum value.

<!--
extensibility/plugins;extensibility/controllersfactory;extensibility/pathproviders;extensibility/handlers
-->