# Crawler

## Task description
Implement a simple web crawler in C# (as a console application), that would crawl given page from starting URL provided as a parameter (only simple HTML parsing, no need to decode URLs from JS / Ajax) and write the results to MongoDB (URL, worker thread, timestamp, page title). The application should be multithreaded, number of parser working threads configurable as a parameter. 

## My solution
To make it more fun for me I was trying to do not to obvious version of Crawler using Reactive Extensions. Using more functional approach seems better for this kind of problem. This is only proof of concept so there are many thing which could be done better. Like more tests, better exceptions handling, better page parser, more efficient html downloader, more control when process should be stopped and so on. This took me about 6 hours so I can't afford to work more with that.

For real solution I think I would use simplest basic Threads to have most efficient way to manage process.


 