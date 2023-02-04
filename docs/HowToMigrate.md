# How-To Migrate your Azure Functions to .NET 7 Isolated Mode

Azure Functions for .NET with .NET 7 only run in _isolated mode_ and require a change in structure when migrating from .NET 6 and _in-process mode._  This document will help you migrate your Azure Functions project to .NET 7 with the help of this library, **FunctionMigration.Extensions** 

## Migrate Any Functions

### 1. Add Packages for the Isolated Mode project model

### 2. Change the project framework version to net7.0

### 3. Set configuration for dotnet_isolated

Add a configuration setting to the `values` node in your `local.settings.json` file, and eventually to production application, to instruct the application to run in isolated mode.

```json
"FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
```

### 4. Add the FunctionMigrations package

Execute the following at the command-line to add the FunctionMigrations package to your functions project:

```console
dotnet add package FunctionMigrations.Extensions
```

### Results

None of your functions should be able to compile at this point.  We need to adapt your functions so that they can be processed by the functions runtime.  

## Migrate HttpTrigger functions

### 1. Global Replace the typical HttpTrigger parameters

### 2. Clean up interactions with Headers, Query, and Form

### 3. Fix return statements

