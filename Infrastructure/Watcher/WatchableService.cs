using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Watcher
{
    public abstract class WatchableService : IWatchableService
    {
        readonly IEmailSender _emailService;
        readonly string _adminEmails;
        readonly string _emailErrorSubject;
        readonly string _emailWarningSubject;
        readonly string _emailRestoreSubject;
        readonly bool _sendErrorEmail;
        readonly bool _sendWarningEmail;
        readonly bool _sendRestoreEmail;
        protected readonly ILogger _logger;

        TimeSpan _lastElapsedTime = TimeSpan.Zero;
        TimeSpan _minElapsedTime = TimeSpan.MaxValue;
        TimeSpan _maxElapsedTime = TimeSpan.MinValue;
        TimeSpan _avgElapsedTime = TimeSpan.Zero;
        TimeSpan _totalElapsedTime = TimeSpan.Zero;

        public DateTime LastOperationTime { get; protected set; }
        public WatchableServiceState State { get; private set; }
        public string Name { get; }
        public string DisplayName { get; }

        public int TotalRequestCount { get; protected set; }
        public int SuccessfulRequestCount { get; protected set; }
        public int FailedRequestCount { get; protected set; }

        public Dictionary<string, string> Metrics { get; private set; } = new Dictionary<string, string>();
        public List<StateChangeItem> StateChangeHistory { get; private set; } = new List<StateChangeItem>();
        public Dictionary<string, string> Parameters { get; private set; } = new Dictionary<string, string>();

        public WatchableService(string name, 
            string displayName,
            ILoggerFactory loggerFactory,
            IEmailSender emailService, 
            IConfiguration configuration)
        {
            Name = name;
            DisplayName = displayName;

            Metrics["Total Requests"] = TotalRequestCount.ToString();
            Metrics["Successful Requests"] = SuccessfulRequestCount.ToString();
            Metrics["Failed Requests"] = FailedRequestCount.ToString();
            Metrics["Last Request Time"] = "";
            Metrics["Min Request Time"] = "";
            Metrics["Max Request Time"] = "";
            Metrics["Avg Request Time"] = "";
            Metrics["Total Request Time"] = "";

            _logger = loggerFactory.CreateLogger(Name);
            _emailService = emailService;

            _adminEmails = configuration["Watcher:AdminEmails"];

            _emailErrorSubject = configuration["Watcher:ErrorEmailSubject"] ?? "GRAFT Service Error (_service_name_)";
            _emailWarningSubject = configuration["Watcher:WarningEmailSubject"] ?? "GRAFT Service Warninig (_service_name_)";
            _emailRestoreSubject = configuration["Watcher:RestoreEmailSubject"] ?? "GRAFT Service Restore (_service_name_)";

            _sendErrorEmail = Convert.ToBoolean(configuration[$"{Name}:SendErrorEmail"] ?? "true");
            _sendWarningEmail = Convert.ToBoolean(configuration[$"{Name}:SendWarningEmail"] ?? "true");
            _sendRestoreEmail = Convert.ToBoolean(configuration[$"{Name}:SendRestoreEmail"] ?? "true");

            if (_emailErrorSubject != null)
                _emailErrorSubject = _emailErrorSubject.Replace("_service_name_", DisplayName);

            if (_emailWarningSubject != null)
                _emailWarningSubject = _emailWarningSubject.Replace("_service_name_", DisplayName);

            if (_emailRestoreSubject != null)
                _emailRestoreSubject = _emailRestoreSubject.Replace("_service_name_", DisplayName);

            _logger.LogInformation("Service started");
        }

        public abstract Task Ping();

        protected void SetState(WatchableServiceState newState, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            SetState(newState, ex.InnerException?.Message ?? ex.Message);
        }

        protected void SetState(WatchableServiceState newState, string message = null)
        {
            bool sendError = false;
            bool sendWarning = false;
            bool sendRestore = false;

            lock (StateChangeHistory)
            {
                if (State != newState)
                {
                    StateChangeHistory.Add(new StateChangeItem
                    {
                        Time = DateTime.UtcNow,
                        OldState = State,
                        NewState = newState,
                        Message = message
                    });


                    if (newState == WatchableServiceState.Error)
                        sendError = _sendErrorEmail;
                    else if (newState == WatchableServiceState.Warning)
                        sendWarning = _sendWarningEmail;
                    else if (State != WatchableServiceState.Undefined)
                        sendRestore = _sendRestoreEmail;

                    State = newState;
                }
            }

            if (sendError)
            {
                _logger.LogError($"{message}");
                if (_emailService != null)
                    _emailService.SendEmail(_adminEmails, _emailErrorSubject, $"Service '{DisplayName}' error: {message}");
            }
            else if (sendWarning)
            {
                _logger.LogWarning($"{message}");
                if (_emailService != null)
                    _emailService.SendEmail(_adminEmails, _emailWarningSubject, $"Service '{DisplayName}' warning: {message}");
            }
            else if (sendRestore)
            {
                _logger.LogInformation($"Service '{Name}' restored");
                if (_emailService != null)
                    _emailService.SendEmail(_adminEmails, _emailRestoreSubject, $"Service '{DisplayName}' restored");
            }
        }

        protected void UpdateStopwatchMetrics(Stopwatch sw, bool success)
        {
            TotalRequestCount++;

            if (success)
                SuccessfulRequestCount++;
            else
                FailedRequestCount++;

            _lastElapsedTime = sw.Elapsed;

            if (sw.Elapsed < _minElapsedTime)
                _minElapsedTime = sw.Elapsed;

            if (sw.Elapsed > _maxElapsedTime)
                _maxElapsedTime = sw.Elapsed;

            _totalElapsedTime += sw.Elapsed;

            _avgElapsedTime = _totalElapsedTime / TotalRequestCount;

            Metrics["Total Requests"] = TotalRequestCount.ToString();
            Metrics["Successful Requests"] = SuccessfulRequestCount.ToString();
            Metrics["Failed Requests"] = FailedRequestCount.ToString();
            Metrics["Last Request Time"] = _lastElapsedTime.ToString();
            Metrics["Min Request Time"] = _minElapsedTime.ToString();
            Metrics["Max Request Time"] = _maxElapsedTime.ToString();
            Metrics["Avg Request Time"] = _avgElapsedTime.ToString();
            Metrics["Total Request Time"] = _totalElapsedTime.ToString();
        }

    }
}
