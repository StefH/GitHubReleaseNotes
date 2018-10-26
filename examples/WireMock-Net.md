1.0.4.17
2018-09-22 10:01:52 +02:00
  - [#203](https://github.com/WireMock-Net/WireMock.Net/pull/203) - Set up CI with Azure Pipelines PR contributed by [azure-pipelines[bot]](https://github.com/apps/azure-pipelines)
  - [#204](https://github.com/WireMock-Net/WireMock.Net/pull/204) - Lower priority from Proxy mappings in favor of Admin Mappings PR contributed by [StefH](https://github.com/StefH)
  - [#115](https://github.com/WireMock-Net/WireMock.Net/issues/115) - Question : Do we have provision to read the Response data from a file?
  - [#200](https://github.com/WireMock-Net/WireMock.Net/issues/200) - Issue: Incorrect port matching
  - [#205](https://github.com/WireMock-Net/WireMock.Net/issues/205) - Issue: DELETE method is proxied as lowercase

1.0.4.16
2018-09-11 08:51:13 +02:00
  - [#202](https://github.com/WireMock-Net/WireMock.Net/pull/202) - Update handlebars code to support Regex.Match (#201) PR contributed by [StefH](https://github.com/StefH)
  - [#201](https://github.com/WireMock-Net/WireMock.Net/issues/201) - Question : Extracting text from a request.body that is not json

1.0.4.15
2018-09-04 22:53:23 +02:00
  - [#199](https://github.com/WireMock-Net/WireMock.Net/pull/199) - Fix for .WithBody(Func<RequestMessage, string>...) PR contributed by [StefH](https://github.com/StefH)
  - [#198](https://github.com/WireMock-Net/WireMock.Net/issues/198) - Issue : creating response using .WithBody(Func<RequestMessage, string>...) and .WithStatusCode

1.0.4.14
2018-09-02 21:48:31 +02:00
  - [#197](https://github.com/WireMock-Net/WireMock.Net/pull/197) - Set IsStarted = true in a IApplicationLifetime.ApplicationStarted listener PR contributed by [davide-romanini](https://github.com/davide-romanini)
  - [#196](https://github.com/WireMock-Net/WireMock.Net/issues/196) - Issue: AspNetCoreSelfHost.IsStarted set before the server actually started for real

1.0.4.13
2018-08-31 20:46:08 +02:00
  - [#195](https://github.com/WireMock-Net/WireMock.Net/pull/195) - Add LinqMatcher PR contributed by [StefH](https://github.com/StefH)
  - [#192](https://github.com/WireMock-Net/WireMock.Net/issues/192) - Cannot upgrade from 1.0.4.10 to 1.0.4.12 without upgrading to .net core 2.1
  - [#193](https://github.com/WireMock-Net/WireMock.Net/issues/193) - Question: WireMock in Azure

1.0.4.12
2018-08-23 18:48:42 +02:00
  - [#190](https://github.com/WireMock-Net/WireMock.Net/pull/190) - Fix ResponseMessageTransformer (#188) PR contributed by [StefH](https://github.com/StefH)
  - [#191](https://github.com/WireMock-Net/WireMock.Net/pull/191) - Fix ignore case logic for header-name and cookie-name  PR contributed by [StefH](https://github.com/StefH)
  - [#188](https://github.com/WireMock-Net/WireMock.Net/issues/188) - Bug: ResponseMessageTransformer :
  - [#189](https://github.com/WireMock-Net/WireMock.Net/issues/189) - Issue: Case of header key/name not ignored in RequestBuilder when ignoreCase == true

1.0.4.11
2018-08-20 22:17:14 +02:00
  - [#183](https://github.com/WireMock-Net/WireMock.Net/pull/183) - Set Content-Type header for PutMappingAsync in the client PR contributed by [seanamosw](https://github.com/seanamosw)
  - [#185](https://github.com/WireMock-Net/WireMock.Net/pull/185) - Support Microsoft.AspNetCore for net 4.6.1 and up PR contributed by [StefH](https://github.com/StefH)
  - [#186](https://github.com/WireMock-Net/WireMock.Net/pull/186) - ContentType "application/vnd.api+json" is not recognized as json PR contributed by [steveland83](https://github.com/steveland83)
  - [#182](https://github.com/WireMock-Net/WireMock.Net/issues/182) - Bug: IFluentMockServerAdmin::PutMappingAsync does not set Content-Type
  - [#184](https://github.com/WireMock-Net/WireMock.Net/issues/184) - Bug: Fix AppVeyor PR build process
  - [#187](https://github.com/WireMock-Net/WireMock.Net/issues/187) - Bug: Admin GetRequestAsync does not populate request body for JsonApi ("application/vnd.api+json") content

1.0.4.10
2018-08-14 18:58:06 +02:00
  - [#180](https://github.com/WireMock-Net/WireMock.Net/pull/180) - Add IFileSystemHandler to support Azure for StaticMapping location PR contributed by [StefH](https://github.com/StefH)
  - [#173](https://github.com/WireMock-Net/WireMock.Net/issues/173) - Feature: Mapping files lost when restarting an Azure app service

1.0.4.9
2018-08-08 11:42:24 +02:00
  - [#172](https://github.com/WireMock-Net/WireMock.Net/issues/172) - Question: Same/similar fluent interface for in process and admin client API
  - [#174](https://github.com/WireMock-Net/WireMock.Net/issues/174) - Bug: JsonMatcher and JsonPathMatcher throws when posting byte[]
  - [#175](https://github.com/WireMock-Net/WireMock.Net/issues/175) - Bug: Don't allow adding a mapping with no URL or PATH
  - [#176](https://github.com/WireMock-Net/WireMock.Net/issues/176) - Question: Saving mapping with relative (not found) file fails
  - [#177](https://github.com/WireMock-Net/WireMock.Net/issues/177) - Feature: Skip invalid static mapping files

1.0.4.8
2018-07-23 17:34:25 +02:00
  - [#170](https://github.com/WireMock-Net/WireMock.Net/pull/170) - Support json path in the response PR contributed by [StefH](https://github.com/StefH)
  - [#167](https://github.com/WireMock-Net/WireMock.Net/issues/167) - Feature: Support for JsonPath in the response (with HandleBars)

1.0.4.7
2018-07-19 22:25:40 +02:00
  - [#169](https://github.com/WireMock-Net/WireMock.Net/pull/169) - Fix for Restricted Response headers PR contributed by [StefH](https://github.com/StefH)
  - [#148](https://github.com/WireMock-Net/WireMock.Net/issues/148) - Question: proxy passthrough when no match?

1.0.4.6
2018-07-18 21:37:26 +02:00
  - [#168](https://github.com/WireMock-Net/WireMock.Net/pull/168) - Expose scenario states PR contributed by [StefH](https://github.com/StefH)
  - [#163](https://github.com/WireMock-Net/WireMock.Net/issues/163) - Feature: Expose scenario states

1.0.4.5
2018-07-17 08:30:49 +02:00
  - [#164](https://github.com/WireMock-Net/WireMock.Net/pull/164) - Support running WireMock.Net as a sub-app in IIS PR contributed by [StefH](https://github.com/StefH)
  - [#165](https://github.com/WireMock-Net/WireMock.Net/pull/165) - Add SonarCloud PR contributed by [StefH](https://github.com/StefH)
  - [#166](https://github.com/WireMock-Net/WireMock.Net/pull/166) - Fix Sonar issues PR contributed by [StefH](https://github.com/StefH)
  - [#105](https://github.com/WireMock-Net/WireMock.Net/issues/105) - Question: URL binding issues
  - [#120](https://github.com/WireMock-Net/WireMock.Net/issues/120) - Question: JsonPathMatcher - not matching? Correct syntax?
  - [#123](https://github.com/WireMock-Net/WireMock.Net/issues/123) - Fix for DateTime Header causing null value in ResponseBuilder
  - [#158](https://github.com/WireMock-Net/WireMock.Net/issues/158) - Feature: Support running WireMock.Net as a sub-app in IIS

1.0.4.4
2018-07-01 11:10:34 +02:00
  - [#156](https://github.com/WireMock-Net/WireMock.Net/issues/156) - Feature: when adding / updating a mapping : return more details

1.0.4.3
2018-06-30 00:06:53 +02:00
  - [#159](https://github.com/WireMock-Net/WireMock.Net/issues/159) - Bug: IRequestBuilder.WithParam broken for key-only matching

1.0.4.2
2018-06-26 23:24:32 +02:00
  - [#155](https://github.com/WireMock-Net/WireMock.Net/pull/155) - Replace JsonMatcher with JsonObjectMatcher and directly support JSON body matching. PR contributed by [DavidKorn](https://github.com/DavidKorn)
  - [#157](https://github.com/WireMock-Net/WireMock.Net/pull/157) - Support for string and object in JsonMatcher. PR contributed by [StefH](https://github.com/StefH)
  - [#150](https://github.com/WireMock-Net/WireMock.Net/issues/150) - Add support for .NET Core 2.1 (.NET Core 2.0 will reach end of life on september 2018)
  - [#154](https://github.com/WireMock-Net/WireMock.Net/issues/154) - Feature: support BodyAsJson for Request in static mapping files.

1.0.4.1
2018-06-25 19:57:42 +02:00
  - [#152](https://github.com/WireMock-Net/WireMock.Net/pull/152) - Adds logging of incoming requests PR contributed by [MartijnSips](https://github.com/MartijnSips)
  - [#153](https://github.com/WireMock-Net/WireMock.Net/issues/153) - Feature: Add JsonMatcher to support Json mapping

1.0.4.0
2018-06-23 11:26:24 +02:00
  - [#131](https://github.com/WireMock-Net/WireMock.Net/issues/131) - Bug: CurlException Couldn't connect to Server when running multiple tests
  - [#149](https://github.com/WireMock-Net/WireMock.Net/issues/149) - Question: Transformer and Delay in Static Mappings?
  - [#151](https://github.com/WireMock-Net/WireMock.Net/issues/151) - Feature: Add logging of incoming request and body for tracability

1.0.3.20
2018-05-29 22:01:15 +02:00
  - [#147](https://github.com/WireMock-Net/WireMock.Net/pull/147) - Revert PortUtil.cs changes PR contributed by [StefH](https://github.com/StefH)
  - [#129](https://github.com/WireMock-Net/WireMock.Net/issues/129) - Random test failures between WireMock.Net 1.0.3.1 and 1.0.3.2
  - [#146](https://github.com/WireMock-Net/WireMock.Net/issues/146) - Hang possibly due to Windows firewall prompt

1.0.3.19
2018-05-28 08:43:41 +02:00
  - [#144](https://github.com/WireMock-Net/WireMock.Net/pull/144) - Fix ConcurrentDictionary (#129) PR contributed by [StefH](https://github.com/StefH)
  - [#145](https://github.com/WireMock-Net/WireMock.Net/pull/145) - Cancellation token not passed to server instance in .NET Core 2 PR contributed by [Bob11327](https://github.com/Bob11327)

1.0.3.18
2018-05-25 21:43:29 +02:00
  - [#142](https://github.com/WireMock-Net/WireMock.Net/pull/142) - Allow all headers to be set as Response headers PR contributed by [StefH](https://github.com/StefH)
  - [#97](https://github.com/WireMock-Net/WireMock.Net/issues/97) - Request matching logic is not practical
  - [#122](https://github.com/WireMock-Net/WireMock.Net/issues/122) - WireMock.Net not responding in unit tests - same works in console application
  - [#126](https://github.com/WireMock-Net/WireMock.Net/issues/126) - Question: UsingHead always returns 0 for Content-Length header even when explicitly specified
  - [#127](https://github.com/WireMock-Net/WireMock.Net/issues/127) - Question: Stub priority - Most recent stub is not always used
  - [#132](https://github.com/WireMock-Net/WireMock.Net/issues/132) - LogEntries not being recorded on subsequent tests
  - [#136](https://github.com/WireMock-Net/WireMock.Net/issues/136) - Question: Does the WireMock send Content-Length response header
  - [#137](https://github.com/WireMock-Net/WireMock.Net/issues/137) - Question: How to specify Transfer-Encoding response header?
  - [#139](https://github.com/WireMock-Net/WireMock.Net/issues/139) - Wiki link https://github.com/StefH/WireMock.Net/wiki/Record-(via-proxy)-and-Save is dead
  - [#140](https://github.com/WireMock-Net/WireMock.Net/issues/140) - Question: Why the Microsoft.Owin.Host.HttpListener is not referenced in the dll, which uses WireMock?

1.0.3.17
2018-05-16 22:30:25 +02:00
  - [#134](https://github.com/WireMock-Net/WireMock.Net/pull/134) - Stef negate matcher PR contributed by [alastairtree](https://github.com/alastairtree)
  - [#135](https://github.com/WireMock-Net/WireMock.Net/pull/135) - Merge into the stef_negate_matcher branch (solves issue #133) PR contributed by [StefH](https://github.com/StefH)
  - [#138](https://github.com/WireMock-Net/WireMock.Net/pull/138) - Added Negate matcher logic PR contributed by [StefH](https://github.com/StefH)
  - [#103](https://github.com/WireMock-Net/WireMock.Net/issues/103) - Support for Faults
  - [#128](https://github.com/WireMock-Net/WireMock.Net/issues/128) - Feature: Negate a matcher
  - [#130](https://github.com/WireMock-Net/WireMock.Net/issues/130) - ...
  - [#133](https://github.com/WireMock-Net/WireMock.Net/issues/133) - Issue: Wildcard matching a json body does not work?

1.0.3.16
2018-04-17 19:43:38 +02:00
  - [#119](https://github.com/WireMock-Net/WireMock.Net/pull/119) - Fix for issue 118 PR contributed by [raghavendrabankapur](https://github.com/raghavendrabankapur)
  - [#121](https://github.com/WireMock-Net/WireMock.Net/pull/121) - Fix for issue #118 PR contributed by [raghavendrabankapur](https://github.com/raghavendrabankapur)
  - [#125](https://github.com/WireMock-Net/WireMock.Net/pull/125) - Change listen from loopback to any ip address for dotnetcore2.0 apps PR contributed by [SubjectiveReality](https://github.com/SubjectiveReality)
  - [#118](https://github.com/WireMock-Net/WireMock.Net/issues/118) - Not reading the response from a file when mappings are placed in json file
  - [#124](https://github.com/WireMock-Net/WireMock.Net/issues/124) - Issue: Unable to get host to listen on ips other than 127.0.0.1 using StandAloneApp

1.0.3.15
2018-04-05 21:07:05 +02:00
  - [#117](https://github.com/WireMock-Net/WireMock.Net/pull/117) - Respect start timeout setting and expose exception from server startup PR contributed by [msft-eliang](https://github.com/msft-eliang)

1.0.3.14
2018-04-01 11:54:20 +02:00
  - [#111](https://github.com/WireMock-Net/WireMock.Net/issues/111) - Question: Adding wiki documentation on how to use WireMock.Net.WebApplication project
  - [#112](https://github.com/WireMock-Net/WireMock.Net/issues/112) - Question: Request.Create().WithBody() not able to match with custom class which implements IMatcher
  - [#113](https://github.com/WireMock-Net/WireMock.Net/issues/113) - Feature: Add BodyAsJsonIndented for response message
  - [#114](https://github.com/WireMock-Net/WireMock.Net/issues/114) - Feature: Add PathSegments in Transform

1.0.3.13
2018-03-24 19:02:21 +01:00

1.0.3.12
2018-03-24 18:40:59 +01:00
  - [#100](https://github.com/WireMock-Net/WireMock.Net/issues/100) - Issue: JsonPathMatcher - not working for rootless jsons?

1.0.3.11
2018-03-20 21:06:58 +01:00
  - [#110](https://github.com/WireMock-Net/WireMock.Net/issues/110) - Fix: remove `Func[]` from MappingModel

1.0.3.10
2018-03-17 13:20:18 +01:00
  - [#109](https://github.com/WireMock-Net/WireMock.Net/issues/109) - Issue: When proxying, MimeType is wrong for StringContent

1.0.3.9
2018-03-15 22:18:35 +01:00
  - [#108](https://github.com/WireMock-Net/WireMock.Net/issues/108) - Issue: provide correct contentTypeHeader value for the bodyparser

1.0.3.8
2018-03-10 15:50:34 +01:00
  - [#106](https://github.com/WireMock-Net/WireMock.Net/issues/106) - Issue: Params does not work, when there are multiple values for a key

1.0.3.7
2018-03-09 08:20:48 +01:00
  - [#104](https://github.com/WireMock-Net/WireMock.Net/issues/104) - Issue: PlatformNotSupportedException

1.0.3.6
2018-03-08 18:28:53 +01:00

1.0.3.5
2018-03-08 11:00:19 +01:00

1.0.3.4
2018-03-04 10:50:30 +01:00
  - [#95](https://github.com/WireMock-Net/WireMock.Net/pull/95) - Unittest fix PR contributed by [StefH](https://github.com/StefH)
  - [#96](https://github.com/WireMock-Net/WireMock.Net/pull/96) - Replace log4net by custom logger (#94) PR contributed by [StefH](https://github.com/StefH)
  - [#99](https://github.com/WireMock-Net/WireMock.Net/pull/99) - feat: simple implementation/spike of dynamic responses using callbacks PR contributed by [StefH](https://github.com/StefH)
  - [#101](https://github.com/WireMock-Net/WireMock.Net/pull/101) - ICallbackResponseBuilder + added more unit-tests PR contributed by [StefH](https://github.com/StefH)
  - [#102](https://github.com/WireMock-Net/WireMock.Net/pull/102) - Feature: add WithBody(req => dostuff) style callback PR contributed by [alastairtree](https://github.com/alastairtree)
  - [#66](https://github.com/WireMock-Net/WireMock.Net/issues/66) - Interested in callbacks?
  - [#93](https://github.com/WireMock-Net/WireMock.Net/issues/93) - Bug: FluentMockServer IsStarted after calling Start()
  - [#94](https://github.com/WireMock-Net/WireMock.Net/issues/94) - Issue: Introduced dependency on log4net
  - [#98](https://github.com/WireMock-Net/WireMock.Net/issues/98) - IBodyResponseBuilder.WithBody* should receive the request as a parameter

1.0.3.3
2018-02-24 09:04:21 +01:00
  - [#92](https://github.com/WireMock-Net/WireMock.Net/pull/92) - Json fixes (#91) PR contributed by [StefH](https://github.com/StefH)
  - [#91](https://github.com/WireMock-Net/WireMock.Net/issues/91) - Bug: WireMock.Net is not matching application/json http requests using JSONPathMatcher

1.0.3.2
2018-02-14 19:35:51 +01:00
  - [#89](https://github.com/WireMock-Net/WireMock.Net/pull/89) - Add log4net logging PR contributed by [StefH](https://github.com/StefH)
  - [#90](https://github.com/WireMock-Net/WireMock.Net/pull/90) - Concurrent issue (#88) PR contributed by [StefH](https://github.com/StefH)
  - [#87](https://github.com/WireMock-Net/WireMock.Net/issues/87) - Feature: Add logging
  - [#88](https://github.com/WireMock-Net/WireMock.Net/issues/88) - Bug: Standalone server throws 500 error when receiving concurrent requests

1.0.3.1
2018-02-14 16:13:05 +00:00

1.0.3.0
2018-02-05 08:36:50 +01:00
  - [#80](https://github.com/WireMock-Net/WireMock.Net/issues/80) - Feature: When using proxy, in case Content-Type is JSON, use BodyAsJson in Response
  - [#81](https://github.com/WireMock-Net/WireMock.Net/issues/81) - Feature: When using proxy, only BodyAsBytes in case of binary data?
  - [#82](https://github.com/WireMock-Net/WireMock.Net/issues/82) - Feature: make it possible to ignore some headers when proxying
  - [#83](https://github.com/WireMock-Net/WireMock.Net/issues/83) - Feature : Add also a method in IProxyResponseBuilder to provide proxy-settings
  - [#85](https://github.com/WireMock-Net/WireMock.Net/issues/85) - Bug: https for netstandard does not work ?
  - [#86](https://github.com/WireMock-Net/WireMock.Net/issues/86) - Feature : Add FileSystemWatcher logic for watching static mapping files

1.0.2.13
2018-01-23 08:09:19 +01:00
  - [#79](https://github.com/WireMock-Net/WireMock.Net/pull/79) - Fix missed content headers PR contributed by [vladimir-fed](https://github.com/vladimir-fed)
  - [#57](https://github.com/WireMock-Net/WireMock.Net/issues/57) - ProxyAndRecord does not save query-parameters, headers and body
  - [#78](https://github.com/WireMock-Net/WireMock.Net/issues/78) - WireMock not working when attempting to access from anything other than localhost.

1.0.2.12
2018-01-16 20:46:43 +01:00
  - [#74](https://github.com/WireMock-Net/WireMock.Net/pull/74) - Capturing the index of the existing mapping before removing and insert the updated mapping at the same index of the list PR contributed by [raghavendrabankapur](https://github.com/raghavendrabankapur)
  - [#75](https://github.com/WireMock-Net/WireMock.Net/pull/75) - Add WireMock.Net.WebApplication example PR contributed by [StefH](https://github.com/StefH)
  - [#77](https://github.com/WireMock-Net/WireMock.Net/pull/77) - Fixed issue #76 PR contributed by [StefH](https://github.com/StefH)
  - [#73](https://github.com/WireMock-Net/WireMock.Net/issues/73) - Updated mapping is not being picked and responded with the response
  - [#76](https://github.com/WireMock-Net/WireMock.Net/issues/76) - Bug: IFluentMockServerAdmin is missing content-type for some POST/PUT calls

1.0.2.11
2017-12-20 20:51:51 +01:00
  - [#72](https://github.com/WireMock-Net/WireMock.Net/issues/72) - Matching WithParam on OData End Points

1.0.2.10
2017-12-12 18:18:04 +01:00
  - [#70](https://github.com/WireMock-Net/WireMock.Net/issues/70) - Proxy/Intercept pattern is throwing a keep alive header error with net461

1.0.2.9
2017-12-07 22:17:35 +01:00
  - [#71](https://github.com/WireMock-Net/WireMock.Net/pull/71) - Fixed restricted headers on response PR contributed by [StefH](https://github.com/StefH)
  - [#69](https://github.com/WireMock-Net/WireMock.Net/issues/69) - Instructions are incorrect (?)

1.0.2.8
2017-11-23 20:54:30 +01:00
  - [#65](https://github.com/WireMock-Net/WireMock.Net/pull/65) - bug: Fix admin api client definition returning the wrong types PR contributed by [alastairtree](https://github.com/alastairtree)
  - [#67](https://github.com/WireMock-Net/WireMock.Net/pull/67) - bug: fix supporting the Patch method and logging the body PR contributed by [alastairtree](https://github.com/alastairtree)
  - [#64](https://github.com/WireMock-Net/WireMock.Net/issues/64) - Pull Requests do not trigger test + codecoverage ?
  - [#68](https://github.com/WireMock-Net/WireMock.Net/issues/68) - Full path required in Stub

1.0.2.7
2017-11-18 13:49:22 +01:00
  - [#62](https://github.com/WireMock-Net/WireMock.Net/pull/62) - Add the Host, Protocol, Port and Origin to the Request message so they can be used in templating PR contributed by [alastairtree](https://github.com/alastairtree)
  - [#63](https://github.com/WireMock-Net/WireMock.Net/pull/63) - Fix issue with concurrent logging PR contributed by [vladimir-fed](https://github.com/vladimir-fed)
  - [#27](https://github.com/WireMock-Net/WireMock.Net/issues/27) - New feature: Record and Save
  - [#42](https://github.com/WireMock-Net/WireMock.Net/issues/42) - Enhancement - Save/load request logs to/from disk
  - [#53](https://github.com/WireMock-Net/WireMock.Net/issues/53) - New feature request: Access to Owin pipeline
  - [#61](https://github.com/WireMock-Net/WireMock.Net/issues/61) - Partial mapping

1.0.2.6
2017-10-30 08:49:16 +01:00
  - [#59](https://github.com/WireMock-Net/WireMock.Net/pull/59) - Add ability to provide multiple values for headers in response PR contributed by [Dreamescaper](https://github.com/Dreamescaper)
  - [#60](https://github.com/WireMock-Net/WireMock.Net/pull/60) - Fix proxy headers handling PR contributed by [Dreamescaper](https://github.com/Dreamescaper)
  - [#54](https://github.com/WireMock-Net/WireMock.Net/issues/54) - Proxy for AWS: Error unmarshalling response back from AWS
  - [#56](https://github.com/WireMock-Net/WireMock.Net/issues/56) - WithBodyFromFile Support
  - [#58](https://github.com/WireMock-Net/WireMock.Net/issues/58) - Multiple headers with same name

1.0.2.5
2017-10-24 18:12:39 +02:00
  - [#55](https://github.com/WireMock-Net/WireMock.Net/pull/55) - Fix the problem with headers passthrough PR contributed by [deeptowncitizen](https://github.com/deeptowncitizen)
  - [#44](https://github.com/WireMock-Net/WireMock.Net/issues/44) - Bug: Server not listening after Start() returns (on macOS)
  - [#48](https://github.com/WireMock-Net/WireMock.Net/issues/48) - Stateful support
  - [#52](https://github.com/WireMock-Net/WireMock.Net/issues/52) - SimMetrics.NET error when trying to install NuGet Package

1.0.2.4
2017-10-10 18:12:45 +02:00
  - [#32](https://github.com/WireMock-Net/WireMock.Net/pull/32) - [Feature] Add support for client certificate password and test with real services that require client certificate auth PR contributed by [phillee007](https://github.com/phillee007)
  - [#35](https://github.com/WireMock-Net/WireMock.Net/pull/35) - Revert changes that were made by mistake in prior PR PR contributed by [phillee007](https://github.com/phillee007)
  - [#39](https://github.com/WireMock-Net/WireMock.Net/pull/39) - Listen on http://*:9090 PR contributed by [StefH](https://github.com/StefH)
  - [#40](https://github.com/WireMock-Net/WireMock.Net/pull/40) - Expose more settings to stand-alone app PR contributed by [StefH](https://github.com/StefH)
  - [#41](https://github.com/WireMock-Net/WireMock.Net/pull/41) - Dotnet 20 preview final PR contributed by [StefH](https://github.com/StefH)
  - [#45](https://github.com/WireMock-Net/WireMock.Net/pull/45) - Add RequestLogExpirationDuration and MaxRequestLogCount (#43) PR contributed by [StefH](https://github.com/StefH)
  - [#49](https://github.com/WireMock-Net/WireMock.Net/pull/49) - stateful behavior PR contributed by [deeptowncitizen](https://github.com/deeptowncitizen)
  - [#51](https://github.com/WireMock-Net/WireMock.Net/pull/51) - Observable logs PR contributed by [deeptowncitizen](https://github.com/deeptowncitizen)
  - [#15](https://github.com/WireMock-Net/WireMock.Net/issues/15) - New feature: Proxying
  - [#19](https://github.com/WireMock-Net/WireMock.Net/issues/19) - Is this the same as Mock4Net?
  - [#20](https://github.com/WireMock-Net/WireMock.Net/issues/20) - Add client certificate authentication
  - [#31](https://github.com/WireMock-Net/WireMock.Net/issues/31) - Feature request: Nuget package for standalone version
  - [#33](https://github.com/WireMock-Net/WireMock.Net/issues/33) - Issue with launching sample code (StandAlone server)
  - [#34](https://github.com/WireMock-Net/WireMock.Net/issues/34) - Where is SearchLogsFor method?
  - [#36](https://github.com/WireMock-Net/WireMock.Net/issues/36) - How to implement a request body-dependent response?
  - [#37](https://github.com/WireMock-Net/WireMock.Net/issues/37) - Wrong Request Match result is returning
  - [#38](https://github.com/WireMock-Net/WireMock.Net/issues/38) - Bug: support also listening on *:{port}
  - [#43](https://github.com/WireMock-Net/WireMock.Net/issues/43) - Feature: Add RequestLogExpirationDuration and MaxRequestLogCount
  - [#46](https://github.com/WireMock-Net/WireMock.Net/issues/46) - Log the ip-address from the client/caller also in the RequestLog
  - [#47](https://github.com/WireMock-Net/WireMock.Net/issues/47) - Feature: add matcher details to Request to see which matchers match/not match
  - [#50](https://github.com/WireMock-Net/WireMock.Net/issues/50) - New Feature: Callbacks

1.0.2.1
2017-06-14 12:59:01 +02:00
  - [#28](https://github.com/WireMock-Net/WireMock.Net/issues/28) - Facing issue with WildcardMatcher and '?'
  - [#29](https://github.com/WireMock-Net/WireMock.Net/issues/29) - Support of .Net 4.0
  - [#30](https://github.com/WireMock-Net/WireMock.Net/issues/30) - [Feature] Disable partial mappings by default in standalone version

1.0.2.0
2017-05-05 20:22:55 +02:00
  - [#26](https://github.com/WireMock-Net/WireMock.Net/pull/26) - merge netstandard into main PR contributed by [StefH](https://github.com/StefH)
  - [#21](https://github.com/WireMock-Net/WireMock.Net/issues/21) - Admin static json mappings
  - [#23](https://github.com/WireMock-Net/WireMock.Net/issues/23) - Consider port to .Net Core
  - [#25](https://github.com/WireMock-Net/WireMock.Net/issues/25) - Upgrade to vs2017

1.0.1.5
2017-03-21 14:49:45 +01:00

1.0.1.3
2017-03-20 18:03:54 +01:00

1.0.1.2
2017-02-27 11:16:34 +01:00
  - [#24](https://github.com/WireMock-Net/WireMock.Net/pull/24) - Body Encoding PR contributed by [sbebrys](https://github.com/sbebrys)
  - [#8](https://github.com/WireMock-Net/WireMock.Net/issues/8) - admin rest api
  - [#22](https://github.com/WireMock-Net/WireMock.Net/issues/22) - Add basic-authentication for accessing admin-interface

1.0.1.1
2017-02-10 11:09:28 +01:00
  - [#1](https://github.com/WireMock-Net/WireMock.Net/issues/1) - Replace WildcardPatternMatcher by RegEx
  - [#2](https://github.com/WireMock-Net/WireMock.Net/issues/2) - Func<string> matching
  - [#3](https://github.com/WireMock-Net/WireMock.Net/issues/3) - WithUrls and WithHeaders and ...
  - [#4](https://github.com/WireMock-Net/WireMock.Net/issues/4) - Handlebar support
  - [#5](https://github.com/WireMock-Net/WireMock.Net/issues/5) - Xml(2)Path matching
  - [#6](https://github.com/WireMock-Net/WireMock.Net/issues/6) - JsonPath support matching
  - [#7](https://github.com/WireMock-Net/WireMock.Net/issues/7) - Add WithStatusCodeRange matching
  - [#9](https://github.com/WireMock-Net/WireMock.Net/issues/9) - Cookie matching
  - [#10](https://github.com/WireMock-Net/WireMock.Net/issues/10) - Add usingDelete
  - [#11](https://github.com/WireMock-Net/WireMock.Net/issues/11) - Add response body in binary format
  - [#12](https://github.com/WireMock-Net/WireMock.Net/issues/12) - Getting all currently registered stub mappings
  - [#13](https://github.com/WireMock-Net/WireMock.Net/issues/13) - Handle Exception
  - [#14](https://github.com/WireMock-Net/WireMock.Net/issues/14) - Allow Body as Base64
  - [#16](https://github.com/WireMock-Net/WireMock.Net/issues/16) - Stub priority
  - [#17](https://github.com/WireMock-Net/WireMock.Net/issues/17) - Add JsonBody to response
  - [#18](https://github.com/WireMock-Net/WireMock.Net/issues/18) - Listen on more ip-address/ports

1.0.0.0
2017-01-17 20:44:53 +01:00