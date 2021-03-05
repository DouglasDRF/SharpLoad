[![License](https://img.shields.io/github/license/douglasdrf/sharpload?style=plastic)](https://github.com/DouglasDRF/SharpLoad/blob/master/LICENSE)
[![Build Status](https://travis-ci.com/DouglasDRF/SharpLoad.svg?branch=master)](https://travis-ci.org/DouglasDRF/SharpLoad)

# SharpLoad

A simple but powerful CLI tool written in .NET Core for Load Test


## Intro
This tools was inspired in [Locust.io]([https://github.com/locustio/locust](https://github.com/locustio/locust)) written in Python and [Artillery.io]([https://github.com/artilleryio/artillery](https://github.com/artilleryio/artillery)) written in Node.js and I like them due its ease to use.

This is an attempt to explore .NET Core capabilities and see how better the memory efficiency and speed compared its predecessor .NET Core framework and try to achieve higher request count per single node.

## Requirements
This CLI tools is so far written using the latest version of .NET Core, the 3.1.100. Such framework is multiplataform being able to work fine and Windows, Linux, Linux ARMs such Raspberries and Mac OSX, but the MAC OSX I couldn't test it.
Having the .NET Core SDK installed, only a text editor like VS Code and `dotnet` CLI are enough to build, test and run the project

## Building
In the root folder of this repository there's the `build-all-platforms.sh`to build to all platforms to a Release folder
if you want to build-it by your own you can use de `dotnet build` or `dotnet publish` command to achieve it

 - Example
`cd src/SharpLoad.Application `	
`dotnet build SharpLoad.Application.csproj -o <output-path> -c Release -r <plataform-id> `
or
`dotnet publish SharpLoad.Application.csproj -o <output-path> -c Release -r <plataform-id> `

## Getting Started
To first run some load test you can write a json file with the params of the test. There's a sample in Example folder that you can run it calling:
	`sharloader -f ../<pathToFolder>/loadTestExamplleOnWish.json `

