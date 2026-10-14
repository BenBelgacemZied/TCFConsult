using TCFConsult.Application.Ports;

namespace TCFConsult.Mobile.Platforms.Android;

/// <summary>First safe increment: import one or more scans from the Android system picker.</summary>
public sealed class AndroidDocumentScanner : IDocumentScanner
{
    public async Task<IReadOnlyList<Stream>> ScanPagesAsync(CancellationToken cancellationToken = default)
    {
        var files = await FilePicker.Default.PickMultipleAsync(new PickOptions
        {
            PickerTitle = "Choisir les pages scannées",
            FileTypes = FilePickerFileType.Images
        });
        if (files is null) return [];

        var pages = new List<Stream>();
        foreach (var file in files)
        {
            cancellationToken.ThrowIfCancellationRequested();
            pages.Add(await file.OpenReadAsync());
        }
        return pages;
    }
}