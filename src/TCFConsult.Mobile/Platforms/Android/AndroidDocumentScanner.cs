using CommunityToolkit.Maui.Camera;
using TCFConsult.Application.Ports;

namespace TCFConsult.Mobile.Platforms.Android;

public sealed class AndroidDocumentScanner : IDocumentScanner
{
    public async Task<IReadOnlyList<Stream>> ScanPagesAsync(CancellationToken cancellationToken = default)
    {
        var camera = new CameraView();
        var tcs = new TaskCompletionSource<IReadOnlyList<Stream>>(TaskCreationOptions.RunContinuationsAsynchronously);
        var page = new ContentPage { Title = "Scanner", Content = new VerticalStackLayout { Padding = 20, Spacing = 12, Children = {
            new Label { Text = "Placez le document dans le cadre puis prenez une photo. Vous pouvez ajouter plusieurs pages." },
            camera,
            new HorizontalStackLayout { Spacing = 10, Children = {
                new Button { Text = "Prendre la page", Command = new Command(async () => { try { var shot = await camera.TakePictureAsync(); if (shot is not null) ((List<Stream>)tcs.Task.AsyncState!).Add(shot); } catch (Exception ex) { tcs.TrySetException(ex); } }) },
                new Button { Text = "Terminer", Command = new Command(() => tcs.TrySetResult((List<Stream>)tcs.Task.AsyncState!)) }
            }}
        }}};
        var pages = new List<Stream>();
        // AsyncState is not user-settable on TaskCompletionSource; capture the page list in button closures.
        page.Content = BuildContent(camera, pages, tcs);
        await MainThread.InvokeOnMainThreadAsync(async () => await Application.Current!.MainPage!.Navigation.PushModalAsync(new NavigationPage(page)));
        using var registration = cancellationToken.Register(() => tcs.TrySetCanceled(cancellationToken));
        return await tcs.Task;
    }

    private static View BuildContent(CameraView camera, List<Stream> pages, TaskCompletionSource<IReadOnlyList<Stream>> tcs) =>
        new VerticalStackLayout { Padding = 20, Spacing = 12, Children = {
            new Label { Text = "Placez le document dans le cadre puis prenez une photo. Vous pouvez ajouter plusieurs pages." },
            camera,
            new HorizontalStackLayout { Spacing = 10, Children = {
                new Button { Text = "Prendre la page", Command = new Command(async () => { try { var shot = await camera.TakePictureAsync(); if (shot is not null) pages.Add(shot); } catch (Exception ex) { tcs.TrySetException(ex); } }) },
                new Button { Text = "Terminer", Command = new Command(() => tcs.TrySetResult(pages)) }
            }}
        }};
}