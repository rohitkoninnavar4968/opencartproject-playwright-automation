# Opencart Automation Project (Playwright)

Simple Playwright-based UI automation for the Opencart demo site.

## Overview

This repository contains Playwright-based automated tests written in C# targeting .NET 9. Tests live under `Tests`; driver and configuration helpers are under `Driver` and `Config`.

## Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or VS Code
- Node.js (required by Playwright to install browser binaries)

## Setup

1. Clone the repository:
   git clone https://github.com/rohitkoninnavar4968/opencartproject-playwright-automation.git

2. Restore packages:
   dotnet restore

3. Install Playwright browsers (run once):
   dotnet tool restore
   dotnet playwright install

## Running Tests

Run tests with the .NET test runner:
dotnet test

Test settings (such as `ApplicationUrl`, `DriverType`, `ProductName`) are read from `appsettings.json` at runtime.

## Contributing

- Follow the coding style in `.editorconfig` and `CONTRIBUTING.md`.
- Add tests for new features and open a pull request describing changes.

## License

Specify your project license here (for example: MIT).
