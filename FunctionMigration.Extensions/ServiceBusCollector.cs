﻿using Azure.Messaging.ServiceBus;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using System.Text.Json;

namespace FunctionMigration.Extensions;

public class ServiceBusCollector<T> : IAsyncCollector<T> {

	private ServiceBusClient _Client;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public ServiceBusCollector(string connectionString, ServiceBusEntityType type, string queueOrTopicName)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{

		ConnectionString = connectionString;
		_Type = type;
		QueueName = queueOrTopicName;

	}

	private string _ConnectionString;
	public string ConnectionString {
		get => _ConnectionString;
		set {
			_ConnectionString = value;
			_Client = new ServiceBusClient(value);
		}
	}

	private readonly ServiceBusEntityType _Type;
	private string _QueueName;
	public string QueueName {
		get => _QueueName;
		set {
			_QueueName = value;
		}
	}

	public Task AddAsync(T item, CancellationToken cancellationToken = default) {

		if (item == null) return Task.CompletedTask;
		if (_Client == null) _Client = new ServiceBusClient(ConnectionString);

		if (typeof(T).IsValueType || item is string) {
			return _Client.CreateSender(QueueName).SendMessageAsync(new ServiceBusMessage(item.ToString()), cancellationToken);
		}
		else {
			return _Client.CreateSender(QueueName).SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(item)), cancellationToken);
		}

	}

	public Task FlushAsync(CancellationToken cancellationToken = default) {

		return Task.CompletedTask;

	}
}