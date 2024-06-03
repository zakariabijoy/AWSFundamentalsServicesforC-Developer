﻿namespace Customers.Api.Contracts.Messages;

public class CustomerCreated
{
    public required Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string GitHubUsername { get; set; }
    public required DateTime DateOfBirth { get; set; }
}

public class CustomerUpdated
{
    public required Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string GitHubUsername { get; set; }
    public required DateTime DateOfBirth { get; set; }
}

public class CustomerDeleted
{
    public required Guid Id { get; set; }
}