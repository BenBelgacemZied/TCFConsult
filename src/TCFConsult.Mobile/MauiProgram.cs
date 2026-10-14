using Plugin.Maui.DocumentScanner;
using TCFConsult.Application.UseCases;
using TCFConsult.Infrastructure.Pdf;
using TCFConsult.Infrastructure.Storage;

namespace TCFConsult.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseDocumentScanner();
        builder.Services.AddSingleton<TCFConsult.Application.Ports.IDocumentScanner, Platforms.Android.AndroidDocumentScanner>();
        builder.Services.AddSingleton<TCFConsult.Application.Ports.IPdfDocumentWriter, PdfDocumentWriter>();
        builder.Services.AddSingleton<TCFConsult.Application.Ports.IDocumentStorage>(_ => new LocalDocumentStorage(FileSystem.AppDataDirectory));
        builder.Services.AddTransient<ScanAndSaveDocument>();
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
    }
}