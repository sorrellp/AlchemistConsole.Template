using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AlchemistConsole.db;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IStartupFilter, DatabaseMigrationStartupFilter>();
