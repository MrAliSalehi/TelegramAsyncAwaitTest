using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using static TelegramDelayTestDemo.BotLogic;

namespace TelegramDelayTestDemo;
public class Worker : BackgroundService
{
    private static TelegramBotClient? _bot;
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("started");

        _bot = new TelegramBotClient("1468805234:AAEm-QRYUP_WTRaiol5VyvixPgdoHhRNbfY");
        ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
        _bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (update.Message is not null)
                await BotOnMessageReceived(botClient, update.Message, cancellationToken);
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(botClient, exception, cancellationToken);
        }
    }
    public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}