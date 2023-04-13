using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using System.Text.Json;

namespace FunctionMigration.Extensions;

public class QueueCollector<T> : ICollector<T>, IAsyncCollector<T>
{

	private QueueClient _Client;
	private static readonly QueueClientOptions DefaultOptions = new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 };

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public QueueCollector(string connectionString, string queueName)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{

		ConnectionString = connectionString;
		QueueName = queueName;

	}

	private string _ConnectionString;
	public string ConnectionString
	{
		get => _ConnectionString;
		set
		{
			_ConnectionString = value;
			_Client = new QueueClient(value, QueueName, DefaultOptions);
		}
	}

	private string _QueueName;
	public string QueueName
	{
		get => _QueueName;
		set
		{
			_QueueName = value;
			_Client = new QueueClient(ConnectionString, value, DefaultOptions);
		}
	}

	public void Add(T item)
	{

		if (item == null) return;
		if (_Client == null) _Client = new QueueClient(ConnectionString, QueueName, DefaultOptions);
		_Client.CreateIfNotExists();

		if (typeof(T).IsValueType || item is string)
		{
			_Client.SendMessage(item.ToString());
		}
		else
		{
			_Client.SendMessage(JsonSerializer.Serialize(item));
		}

	}

	public Task AddAsync(T item, CancellationToken cancellationToken = default)
	{

		if (item == null) return Task.CompletedTask;
		if (_Client == null) _Client = new QueueClient(ConnectionString, QueueName, DefaultOptions);
		_Client.CreateIfNotExists();

		if (typeof(T).IsValueType || item is string)
		{
			return _Client.SendMessageAsync(item.ToString(), cancellationToken);
		}
		else
		{
			return _Client.SendMessageAsync(JsonSerializer.Serialize(item), cancellationToken);
		}

	}

	public Task FlushAsync(CancellationToken cancellationToken = default)
	{

		return Task.CompletedTask;

	}
}
