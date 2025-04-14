# Currency Rate Fetcher

This is a project designed to fetch and store exchange rates using an external API. The rates are fetched periodically and stored in a JSON file for later use.

## Features

- Fetch exchange rates from an external API (CurrencyFreaks API).
- Store the exchange rates in a local JSON file.
- Periodically update the rates every 10 seconds.
- Supports multiple currencies, including USD, EUR, GBP, and others.
- API for retrieving stored rates.

## Prerequisites

- .NET 7.0

## Solution Overview
The solution consists of a single project with two independent services:

RateFetcher: This service is responsible for querying the API every 10 seconds to fetch exchange rates.

RatePrinter: This service is an API itself that provides information about the exchange rates. It includes endpoints such as:

GET /all – Returns all exchange rates.


GET /rates – Returns exchange rates for a given date range, using the query parameters fromRate and toRate.

The database is a JSON file.

## Technical Notes
The JSON file is not secured for read/write operations on shared resources. If this were a production environment, I would implement a more secure solution.

The API I used is somehow restricted to only allow USD exchange rates, and I noticed this limitation later in the process.



