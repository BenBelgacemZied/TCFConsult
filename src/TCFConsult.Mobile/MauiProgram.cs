using CommunityToolkit.Maui;
using TCFConsult.Application.Ports;
using TCFConsult.Application.UseCases;
using TCFConsult.Infrastructure.Pdf;
using TCFConsult.Infrastructure.Storage;

namespace TCFConsult.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddSingleton<IDocumentScanner, Platforms.Android.AndroidDocumentScanner>();
        builder.Services.AddSingleton<IPdfDocumentWriter, PdfDocumentWriter>();
        builder.Services.AddSingleton<IDocumentStorage>(_ => new LocalDocumentStorage(FileSystem.AppDataDirectory));
        builder.Services.AddTransient<ScanAndSaveDocument>();
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
    }
}