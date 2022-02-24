using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Avalonia.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;
using System.ComponentModel;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace Emovision.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private enum CaptureState
        {
            Idle,
            Capturing,
        };

        private Dictionary<CaptureState, string> _capStateMap = new() {
            [CaptureState.Idle]      = "Capture",
            [CaptureState.Capturing] = "Capturing",
        };
        private CaptureState _capState = CaptureState.Idle;
        private CaptureState CapState {
            get => _capState;
            set {
                _capState = value;
                this.RaisePropertyChanged(nameof(CapStateString));
            }
        }

        public string CapStateString => _capStateMap[_capState];

        private Bitmap _bitmap;
        private VideoCapture _vc;

        private CancellationTokenSource _cts;
        
        public MainWindowViewModel()
        {
            _bitmap = new(Path.Combine(Environment.CurrentDirectory, "ai.jpg"));
            _vc = VideoCapture.FromCamera(0);
            if (!_vc.IsOpened())
            {
                Console.WriteLine($"An error occurred while opening a video capture device.");
                return;
            }
            OnStartCaptureClickCommand = ReactiveCommand.CreateFromTask(
                () => {
                    _cts = new CancellationTokenSource();
                    return Task.Run(StartCapturePreviewAsync);
                    }
                );
            OnStartCaptureClickCommand.ThrownExceptions.Subscribe(ex => {
                Console.WriteLine($"Shit happens: {ex}.");
            });
            OnStopCaptureClickCommand = ReactiveCommand.Create(
                () => {
                    if (CapState is CaptureState.Capturing)
                    {
                        _cts.Cancel();
                        _cts.Dispose();
                        CapState = CaptureState.Idle;
                    }
                }
            );
        }

        public int CapWidth => _vc.FrameWidth;
        public int CapHeight => _vc.FrameHeight;
        public Bitmap Bmp {
            get => _bitmap;
            private set => this.RaiseAndSetIfChanged(ref _bitmap, value, nameof(Bmp));
        }

        public ReactiveCommand<Unit, Unit> OnStartCaptureClickCommand { get; }
        public ReactiveCommand<Unit, Unit> OnStopCaptureClickCommand { get; }

        private Bitmap? CaptureNextFrame()
        {
            var mat = _vc.RetrieveMat();
            Console.WriteLine($"{mat}");
            
            var bmp = BitmapConverter.ToBitmap(mat);
            Bitmap? bitmap = null;
            using (MemoryStream memory = new())
            {
                bmp.Save(memory, ImageFormat.Jpeg);
                memory.Position = 0;
                bitmap = new(memory);
            }
            return bitmap;
        }
        private async Task StartCaptureAsync(CancellationToken cancellationToken = default)
        {
            using (var timer = new PeriodicTimer(TimeSpan.FromSeconds(1 / _vc.Fps)))
            {
                while (await timer.WaitForNextTickAsync(cancellationToken))
                {
                    Bmp = CaptureNextFrame()!;
                }
            }
        }
        private async Task StartCapturePreviewAsync()
        {
            if (CapState is CaptureState.Idle)
            {
                CapState = CaptureState.Capturing;
                await StartCaptureAsync(_cts.Token);
            }
        }
    }
}
