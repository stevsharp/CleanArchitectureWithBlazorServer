global using System.ComponentModel;
global using System.Data;
global using System.Globalization;
global using System.Linq.Expressions;
global using System.Reflection;
global using System.Text.Json;
global using Ardalis.Specification;
global using AutoMapper;
global using AutoMapper.QueryableExtensions;
global using CleanArchitecture.Blazor.Application.Common.ExceptionHandlers;
global using CleanArchitecture.Blazor.Application.Common.Interfaces.Serialization;
global using CleanArchitecture.Blazor.Application.Common.Extensions;
global using CleanArchitecture.Blazor.Application.Common.Interfaces;
global using CleanArchitecture.Blazor.Application.Common.Interfaces.Caching;
global using CleanArchitecture.Blazor.Application.Common.Models;
global using CleanArchitecture.Blazor.Application.Common.Security;
global using CleanArchitecture.Blazor.Application.Common.FusionCache;
global using CleanArchitecture.Blazor.Domain.Common.Enums;
global using CleanArchitecture.Blazor.Domain.Common.Events;
global using CleanArchitecture.Blazor.Domain.Events;
global using CleanArchitecture.Blazor.Domain.Entities;
global using FluentValidation;
global using MediatR;
global using MediatR.Pipeline;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;

global using CleanArchitecture.Blazor.Application.Features.Employees.Caching;
global using CleanArchitecture.Blazor.Application.Features.Employees.Commands;
global using CleanArchitecture.Blazor.Application.Features.Employees.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Employees.Queries;
global using CleanArchitecture.Blazor.Application.Features.Employees.Specifications;

global using CleanArchitecture.Blazor.Application.Features.Assignments.Caching;
global using CleanArchitecture.Blazor.Application.Features.Assignments.Commands;
global using CleanArchitecture.Blazor.Application.Features.Assignments.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Assignments.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.Assignments.Queries;
global using CleanArchitecture.Blazor.Application.Features.Assignments.Specifications;


global using CleanArchitecture.Blazor.Application.Features.Projects.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Projects.Caching;
global using CleanArchitecture.Blazor.Application.Features.Projects.Commands;
global using CleanArchitecture.Blazor.Application.Features.Projects.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.Projects.Queries;

global using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Tenants.Caching;
global using CleanArchitecture.Blazor.Application.Features.Tenants.Commands;
global using CleanArchitecture.Blazor.Application.Features.Tenants.Queries;

global using CleanArchitecture.Blazor.Application.Features.Vendors.Caching;
global using CleanArchitecture.Blazor.Application.Features.Vendors.Commands;
global using CleanArchitecture.Blazor.Application.Features.Vendors.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Vendors.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.Vendors.Queries;
global using CleanArchitecture.Blazor.Application.Features.Vendors.Specifications;

global using CleanArchitecture.Blazor.Application.Features.Quotes.Caching;
global using CleanArchitecture.Blazor.Application.Features.Quotes.Commands;
global using CleanArchitecture.Blazor.Application.Features.Quotes.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Quotes.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.Quotes.Queries;
global using CleanArchitecture.Blazor.Application.Features.Quotes.Specifications;

global using CleanArchitecture.Blazor.Application.Features.QuoteLines.Caching;
global using CleanArchitecture.Blazor.Application.Features.QuoteLines.Commands;
global using CleanArchitecture.Blazor.Application.Features.QuoteLines.DTOs;
global using CleanArchitecture.Blazor.Application.Features.QuoteLines.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.QuoteLines.Queries;
global using CleanArchitecture.Blazor.Application.Features.QuoteLines.Specifications;

global using CleanArchitecture.Blazor.Application.Features.QuoteVersions.Caching;
global using CleanArchitecture.Blazor.Application.Features.QuoteVersions.Commands;
global using CleanArchitecture.Blazor.Application.Features.QuoteVersions.DTOs;
global using CleanArchitecture.Blazor.Application.Features.QuoteVersions.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.QuoteVersions.Queries;
global using CleanArchitecture.Blazor.Application.Features.QuoteVersions.Specifications;

global using CleanArchitecture.Blazor.Application.Features.QuoteApprovals.Caching;
global using CleanArchitecture.Blazor.Application.Features.QuoteApprovals.Commands;
global using CleanArchitecture.Blazor.Application.Features.QuoteApprovals.DTOs;
global using CleanArchitecture.Blazor.Application.Features.QuoteApprovals.Queries;
global using CleanArchitecture.Blazor.Application.Features.QuoteApprovals.Specifications;
global using CleanArchitecture.Blazor.Application.Features.QuoteApprovals.EventHandlers;

global using CleanArchitecture.Blazor.Application.Features.ProjectTasks.Caching;
global using CleanArchitecture.Blazor.Application.Features.ProjectTasks.Commands;
global using CleanArchitecture.Blazor.Application.Features.ProjectTasks.DTOs;
global using CleanArchitecture.Blazor.Application.Features.ProjectTasks.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.ProjectTasks.Queries;
global using CleanArchitecture.Blazor.Application.Features.ProjectTasks.Specifications;

global using CleanArchitecture.Blazor.Application.Features.Contacts.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Contacts.Caching;
global using CleanArchitecture.Blazor.Application.Features.Contacts.Commands;
global using CleanArchitecture.Blazor.Application.Features.Contacts.Queries;
global using CleanArchitecture.Blazor.Application.Features.Contacts.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.Contacts.Specifications;

global using CleanArchitecture.Blazor.Application.Features.Documents.Commands;
global using CleanArchitecture.Blazor.Application.Features.Documents.Caching;
global using CleanArchitecture.Blazor.Application.Features.Documents.DTOs;
global using CleanArchitecture.Blazor.Application.Features.Documents.EventHandlers;
global using CleanArchitecture.Blazor.Application.Features.Documents.Queries;
global using CleanArchitecture.Blazor.Application.Features.Documents.Specifications;