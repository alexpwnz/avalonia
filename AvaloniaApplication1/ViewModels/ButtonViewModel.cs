namespace AvaloniaApplication1.ViewModels
{
    using System;
    using System.Reactive;
    using ReactiveUI;

    public class ButtonViewModel : ViewModelBase
    {
        private readonly Random _random = new();
        private string _color = null!;

        public ButtonViewModel()
        {
            Color = GenerateRandomColor();
            ChangeColorCommand = ReactiveCommand.Create<string>(ChangeColor);
        }

        public string Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        public ReactiveCommand<string, Unit> ChangeColorCommand { get; set; }

        private void ChangeColor(string a)
        {
            Color = GenerateRandomColor();
        }

        private string GenerateRandomColor()
        {
            return $"#{_random.Next(0x1000000):X6}";
        }
    }
}