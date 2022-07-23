# elev-api.net

A POC for login (and maybe more) to the Skolplattformen Student portal written in C#.

The end goal is to create an app for students. This repo is used to find out the inner workings of all things Skolplattformen. It may or may not be a part of the final product.

## Current status
Not much is wroking yet. Code is a mess. Don't bother unless you are here to try to make it work. 

### Working parts
* Login to the Sharepoint part, AzureApi (calendar, user info), absence and timetable is working. Others untested.
* Getting a list of news items and a single news item is partly done.
* Getting absence list
* Getting timetable
* Getting calendar entries

## Contribute / Try
### Prerequisites
You will need to have access to a **student account** in Skolplattformen.

### Issues
Please file issues and requests. 

### Help with coding

* Clone repo
* Copy *elev-template.json* to **elev.json** and fill in the login details (elev.json is .gitignored)

#### Visual Studio 2022 (17.x) for PC/Mac
* Get Visual Studio (free Community edition is enough) from [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/)
* Open Solution and run/debug SkolplattformenElevDemo

#### Other ways
* Get and install .NET 6 from [https://dotnet.microsoft.com/en-us/download/dotnet/6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Install tooling for your editor for debugging

```bash
dotnet restore
cd src/SkolplattformenElevDemo
dotnet run
```
